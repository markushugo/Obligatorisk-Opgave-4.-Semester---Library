using Obligatorisk_Opgave_4._Semester.IRepos;
using Obligatorisk_Opgave_4._Semester.Models;
using Obligatorisk_Opgave_4._Semester.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorisk_Opgave_4._Semester.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;

        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        public IEnumerable<Product> GetAll() => _productRepo.GetAllProducts();

        public Product? GetById(int id) => _productRepo.GetProductById(id);

        public bool TryReduceStock(int id, int quantity)
        {
            var p = _productRepo.GetProductById(id);
            if (p == null) return false;
            if (p.Stock < quantity) return false;
            p.Stock -= quantity;
            _productRepo.UpdateProduct(p);
            return true;
        }

        public bool IncreaseStock(int id, int quantity)
        {
            var p = _productRepo.GetProductById(id);
            if (p == null) return false;
            p.Stock += quantity;
            _productRepo.UpdateProduct(p);
            return true;
        }
    }
}
