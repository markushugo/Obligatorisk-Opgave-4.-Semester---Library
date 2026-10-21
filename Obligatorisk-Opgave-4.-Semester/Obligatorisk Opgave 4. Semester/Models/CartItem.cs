namespace Obligatorisk_Opgave_4._Semester.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public double LineTotal => Price * Quantity;
    }
}
