using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorisk_Opgave_4._Semester.Models
{
    public class Product
    {

        public string _name;
        public string _description;
        public double _price;
        public int _stock;


        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }


        public Product(string name, string description, double price, int stock) 
        {
            _name = name;
            _description = description;
            _price = price;
            _stock = stock;
        }
        public Product() { }
    }
}
