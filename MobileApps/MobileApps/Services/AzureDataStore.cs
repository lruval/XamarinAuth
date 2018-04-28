using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

using MobileApps.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace MobileApps.Services
{
	public class AzureDataStore : IDataStore<Boleta>
	{
        bool isInitialized;
		IMobileServiceSyncTable<Boleta> BoletasTable;

		public MobileServiceClient MobileService { get; set; }

		public async Task<IEnumerable<Boleta>> GetBoletasAsync(bool forceRefresh = false)
		{
			await InitializeAsync();
			if (forceRefresh)
				await PullLatestAsync();

			return await BoletasTable.ToEnumerableAsync();
		}

		public async Task<Boleta> GetBoletaAsync(string id)
		{
			await InitializeAsync();
			await PullLatestAsync();
			var Boletas = await BoletasTable.Where(s => s.Id == id).ToListAsync();

			if (Boletas == null || Boletas.Count == 0)
				return null;

			return Boletas[0];
		}

		public async Task<bool> AddBoletaAsync(Boleta Boleta)
		{
			await InitializeAsync();
			await PullLatestAsync();
			await BoletasTable.InsertAsync(Boleta);
			await SyncAsync();

			return true;
		}

		public async Task<bool> UpdateBoletaAsync(Boleta Boleta)
		{
			await InitializeAsync();
			await BoletasTable.UpdateAsync(Boleta);
			await SyncAsync();

			return true;
		}

		public async Task<bool> DeleteBoletaAsync(Boleta Boleta)
		{
			await InitializeAsync();
			await PullLatestAsync();
			await BoletasTable.DeleteAsync(Boleta);
			await SyncAsync();

			return true;
		}

		public async Task InitializeAsync()
		{
			if (isInitialized)
				return;

			MobileService = new MobileServiceClient(App.AzureMobileAppUrl)
			{
				SerializerSettings = new MobileServiceJsonSerializerSettings
				{
					CamelCasePropertyNames = true
				}
			};

            var path = "syncstore.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);
            var store = new MobileServiceSQLiteStore(path);

			store.DefineTable<Boleta>();
			await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
			BoletasTable = MobileService.GetSyncTable<Boleta>();

			isInitialized = true;
		}

		public async Task<bool> PullLatestAsync()
		{
			try
			{
				await BoletasTable.PullAsync($"all{typeof(Boleta).Name}", BoletasTable.CreateQuery());
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Unable to pull Boletas: {ex.Message}");

				return false;
			}
			return true;
		}

		public async Task<bool> SyncAsync()
		{
			try
			{
				await MobileService.SyncContext.PushAsync();
				if (!(await PullLatestAsync().ConfigureAwait(false)))
					return false;
			}
			catch (MobileServicePushFailedException exc)
			{
				if (exc.PushResult == null)
				{
					Debug.WriteLine($"Unable to sync, that is alright as we have offline capabilities: {exc.Message}");

					return false;
				}
				foreach (var error in exc.PushResult.Errors)
				{
					if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
					{
						//Update failed, reverting to server's copy.
						await error.CancelAndUpdateItemAsync(error.Result);
					}
					else
					{
						// Discard local change.
						await error.CancelAndDiscardItemAsync();
					}

					Debug.WriteLine($"Error executing sync operation. Boleta: {error.TableName} ({error.Item["id"]}). Operation discarded.");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Unable to sync Boletas: {ex.Message}");
				return false;
			}

			return true;
		}
	}
}