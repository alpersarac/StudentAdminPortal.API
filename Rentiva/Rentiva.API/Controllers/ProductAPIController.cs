using Microsoft.AspNetCore.Mvc;
using Rentiva.DataAccess.Repository.IRepository;
using Rentiva.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AcsightAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        IUnitOfWork _unitOfWork;
        public ProductAPIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Product> GetAll()
        {
            IEnumerable<Product> objProducts = _unitOfWork.Product.GetAll();
            return objProducts;
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create([FromBody] Product obj)
        {
            _unitOfWork.Product.Add(obj);
            _unitOfWork.Save();
        }
        [HttpGet("GetProduct/{id}")]
        public Product GetProduct(int? id)
        {
            var categoryFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            return categoryFromDb;
        }
        [HttpPut]
        [Route("Update")]
        public async Task Update([FromBody] Product obj)
        {
            _unitOfWork.Product.Update(obj);
            _unitOfWork.Save();
        }

        [HttpDelete("Delete/{id}")]
        public void Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
        }

        [HttpPost("UpdateProductDynamically/{productId}/{categoryId}/{productName}")]
        public void UpdateProductDynamically(int? productId,int categoryId, string? productName)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == productId);
            obj.CategoryId = categoryId;
            obj.Name = productName;
            _unitOfWork.Save();
        }

    }
}
