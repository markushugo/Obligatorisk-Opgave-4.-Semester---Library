using System.Collections.Generic;

namespace Obligatorisk_Opgave_4._Semester.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public double Total => Items.Sum(i => i.LineTotal);
    }
}
