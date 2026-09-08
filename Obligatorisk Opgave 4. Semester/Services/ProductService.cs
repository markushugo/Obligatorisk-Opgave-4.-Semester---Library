using Obligatorisk_Opgave_4._Semester.IRepos;
using Obligatorisk_Opgave_4._Semester.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorisk_Opgave_4._Semester.Services
{
    public class ProductService
    {
        private readonly IProductRepo _productRepo;

        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        public List<Product> GetAllProducts()
        {
            return _productRepo.GetAllProducts();
        }

        public Product GetProductById(int id) 
        {
            return _productRepo.GetProductById(id);
        }

        public void UpdateProduct(Product product)
        {
            _productRepo.UpdateProduct(product);
        }

        public void DeleteProductById(int id)
        {
            _productRepo.DeleteProduct(_productRepo.GetProductById(id));
        }
    }
}
