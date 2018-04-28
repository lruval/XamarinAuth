using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MobileApps.Models;
using MobileApps.Views;

namespace MobileApps.ViewModels
{
    public class BoletasViewModel : BaseViewModel
    {
        public ObservableCollection<Boleta> Boletas { get; set; }
        public Command LoadItemsCommand { get; set; }

        public BoletasViewModel()
        {
            Title = "Browse";
            Boletas = new ObservableCollection<Boleta>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Boleta>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Boleta;
                Boletas.Add(_item);
                await DataStore.AddBoletaAsync(_item);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Boletas.Clear();
                var boletas = await DataStore.GetBoletasAsync(true);
                foreach (var boleta in boletas)
                {
                    Boletas.Add(boleta);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}