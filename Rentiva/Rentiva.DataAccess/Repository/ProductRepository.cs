using Rentiva.DataAccess.Repository.IRepository;
using Rentiva.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nancy.Json;
using System.Net.Http.Json;

namespace Rentiva.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly ILogger<object> _logger;
        public ProductRepository(ApplicationDbContext db, IConfiguration configuration, ILogger<object> logger) :base(db)
        {
            _db = db;
            _configuration = configuration;
            _logger = logger;
        }

        public void Update(Product obj)
        {
            _logger.LogInformation("Product update, " + DateTime.Now);
            _db.Products.Update(obj);
        }

        public IEnumerable<Product> GetProductListById(int? id)
        {
            _logger.LogInformation("Product get by id, " + DateTime.Now);
            return _db.Products.Where(p => p.Category.Id == id).ToList();
        }
        public IEnumerable<Product> GetAll()
        {
            _logger.LogInformation("Product get all, " + DateTime.Now);
            return _db.Products.ToList();
        }

        public async Task<bool> PostAsync(Product obj)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var response = await client.PostAsJsonAsync("ProductAPI/Create", obj);
                    _logger.LogInformation("Product create, " + DateTime.Now);
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Product create exception, " + DateTime.Now + ": " + ex.Message);
                return false;
            }
        }

        public async Task<Product> GetAsync(int? id)
        {
            Product product = null;
            if (id == null)
            {
                return product;
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                var result = await client.GetAsync($"ProductAPI/GetProduct/{id}");

                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                    //deserialize to your class
                    product = JSserializer.Deserialize<Product>(data);
                    _logger.LogInformation("Product get by id, " + DateTime.Now);
                }

            }

            if (product == null)
            {
                return product;
            }
            return product;
        }

        public async Task<bool> UpdateAsync(Product obj)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var response = await client.PutAsJsonAsync("ProductAPI/Update", obj);
                    _logger.LogInformation("Product update, " + DateTime.Now);
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Product update exception, " + DateTime.Now + ": " + ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            IEnumerable<Product> Products = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var result = await client.GetAsync("ProductAPI/GetAll");

                    if (result.IsSuccessStatusCode)
                    {
                        var data = await result.Content.ReadAsStringAsync();
                        JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                        //deserialize to your class
                        Products = JSserializer.Deserialize<List<Product>>(data);
                        _logger.LogInformation("Product get all, " + DateTime.Now);
                        return Products;
                    }
                    else
                    {
                        Products = Enumerable.Empty<Product>();
                        return Products;
                        //ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Product get all exception, " + DateTime.Now + ": " + ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int? id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var response = await client.DeleteAsync($"ProductAPI/Delete/{id}");
                    _logger.LogInformation("Product delete, " + DateTime.Now);
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Product delete exception, " + DateTime.Now + ": " + ex.Message);
                return false;
            }
        }

        public Task<IEnumerable<Product>> ProductListAsync(int? CategoryId)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> UpdateProductDynamically(int? productId, int categoryId, string? productName)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var Items = new { productId = productId, categoryId = categoryId, productName= productName };
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);
                    
                    //var response = await client.PostAsJsonAsync($"ProductAPI/UpdateProductDynamically/{productId}/{categoryId}/{productName}");
                    var response = await client.PostAsJsonAsync($"ProductAPI/UpdateProductDynamically/{productId}/{categoryId}/{productName}", Items);
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Product dynamic update exception, " + DateTime.Now + ": " + ex.Message);
                return false;
            }
        }
    }
}
