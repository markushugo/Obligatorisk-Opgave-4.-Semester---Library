using System.Collections.Generic;
using System.Linq;
using Obligatorisk_Opgave_4._Semester.Models;
using Obligatorisk_Opgave_4._Semester.Abstractions;

namespace Obligatorisk_Opgave_4._Semester.Services
{
    // In-memory implementation used for development/testing only.
    public class InMemoryProductService : Obligatorisk_Opgave_4._Semester.Abstractions.IProductService
    {
        private readonly List<Product> _products;

        public InMemoryProductService()
        {
            _products = new List<Product>
            {
                new Product{ Id=1, Name="Product A", Description="Description A", Price=9.99, Stock=10, ImageUrl=null},
                new Product{ Id=2, Name="Product B", Description="Description B", Price=19.99, Stock=5, ImageUrl=null},
                new Product{ Id=3, Name="Product C", Description="Description C", Price=29.99, Stock=2, ImageUrl=null},
                new Product{ Id=4, Name="Product D", Description="Description A", Price=9.99, Stock=10, ImageUrl=null},
                new Product{ Id=5, Name="Product E", Description="Description B", Price=19.99, Stock=5, ImageUrl=null},
                new Product{ Id=6, Name="Product F", Description="Description C", Price=29.99, Stock=2, ImageUrl=null},
            };
        }

        public IEnumerable<Product> GetAll() => _products;

        public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public bool TryReduceStock(int id, int quantity)
        {
            var p = GetById(id);
            if (p == null) return false;
            if (p.Stock < quantity) return false;
            p.Stock -= quantity;
            return true;
        }

        public bool IncreaseStock(int id, int quantity)
        {
            var p = GetById(id);
            if (p == null) return false;
            p.Stock += quantity;
            return true;
        }
    }
}
