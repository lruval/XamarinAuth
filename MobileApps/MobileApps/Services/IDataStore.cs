using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileApps.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddBoletaAsync(T item);
        Task<bool> UpdateBoletaAsync(T item);
        Task<bool> DeleteBoletaAsync(T item);
        Task<T> GetBoletaAsync(string id);
        Task<IEnumerable<T>> GetBoletasAsync(bool forceRefresh = false);
    }
}
