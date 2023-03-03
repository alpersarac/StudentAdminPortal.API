using Microsoft.AspNetCore.Mvc;
using Rentiva.DataAccess.Repository.IRepository;
using Rentiva.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AcsightAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {
        IUnitOfWork _unitOfWork;
        public CategoryAPIController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Category> GetAll()
        {
            IEnumerable<Category> objCategories = _unitOfWork.Category.GetAll();
            return objCategories;
        }
        
        [HttpGet("GetCategory/{id}")]
        public Category GetCategory(int? id)
        {
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            return categoryFromDb;
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create([FromBody] Category obj)
        {
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
        }

        [HttpPut]
        [Route("Update")]
        public async Task Update([FromBody] Category obj)
        {
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
        }

        [HttpDelete("Delete/{id}")]
        public void Delete(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
        }
        [HttpGet("GetProductList/{id}")]
        public IEnumerable<Product> GetProductList(int? id)
        {
            var categoryProduct = _unitOfWork.Product.GetProductListById(id);
            return categoryProduct;
        }
    }
}
