using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MobileApps.Models;

namespace MobileApps.Services
{
    public class MockDataStore : IDataStore<Boleta>
    {
        List<Boleta> Boletas;

        public MockDataStore()
        {
            Boletas = new List<Boleta>();
            var mockBoletas = new List<Boleta>
            {
                };

            foreach (var Boleta in mockBoletas)
            {
                Boletas.Add(Boleta);
            }
        }

        public async Task<bool> AddBoletaAsync(Boleta Boleta)
        {
            Boletas.Add(Boleta);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateBoletaAsync(Boleta Boleta)
        {
            var _Boleta = Boletas.Where((Boleta arg) => arg.Id == Boleta.Id).FirstOrDefault();
            Boletas.Remove(_Boleta);
            Boletas.Add(Boleta);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteBoletaAsync(Boleta Boleta)
        {
            var _Boleta = Boletas.Where((Boleta arg) => arg.Id == Boleta.Id).FirstOrDefault();
            Boletas.Remove(_Boleta);

            return await Task.FromResult(true);
        }

        public async Task<Boleta> GetBoletaAsync(string id)
        {
            return await Task.FromResult(Boletas.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Boleta>> GetBoletasAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(Boletas);
        }
    }
}