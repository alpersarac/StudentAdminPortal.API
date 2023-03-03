using Rentiva.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentiva.DataAccess.Repository.IRepository
{
    public interface IApiCall<T> where T : class
    {
        Task<bool> PostAsync(T obj);
        Task<T> GetAsync(int? id);
        Task<bool> UpdateAsync(T obj);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> DeleteAsync(int? id);
        Task<IEnumerable<Product>> ProductListAsync(int? CategoryId);
    }
}
