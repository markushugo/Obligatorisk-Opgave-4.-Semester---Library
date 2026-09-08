using System.Collections.Generic;
using Obligatorisk_Opgave_4._Semester.Models;

namespace Obligatorisk_Opgave_4._Semester.Abstractions
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        bool TryReduceStock(int id, int quantity);
        bool IncreaseStock(int id, int quantity);
    }
}
