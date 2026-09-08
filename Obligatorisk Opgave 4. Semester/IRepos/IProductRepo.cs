using Obligatorisk_Opgave_4._Semester.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorisk_Opgave_4._Semester.IRepos
{
    public interface IProductRepo
    {
        List<Product> GetAllProducts();
        public void AddProduct(Product product);
        public void UpdateProduct(Product product);
        public void DeleteProduct(Product product);
        public Product GetProductById(int id);

    }
}
