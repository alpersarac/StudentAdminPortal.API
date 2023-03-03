using Rentiva.DataAccess.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentiva.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly ILogger<object> _logger;
        public UnitOfWork(ApplicationDbContext db, IConfiguration configuration, ILogger<object> logger)
        {
            _db = db;
            _configuration = configuration;
            _logger = logger;
            Category = new CategoryRepository(db, configuration, logger);
            Product = new ProductRepository(db, configuration, logger);
        }
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
