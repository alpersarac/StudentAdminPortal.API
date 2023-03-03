using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentiva.DataAccess.Repository.IRepository
{
    public interface IApiCallForProduct
    {
        Task<bool> UpdateProductDynamically(int? productId, int categoryId, string? productName);
    }
}
