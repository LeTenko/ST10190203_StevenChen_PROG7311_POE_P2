namespace ecommerceWeb_trytry.Models
{
    public class Item
    {

        public Product product { get; set; }
        public int Quantity { get; set; }
        public double Cost
        {
            get {return product.ProductPrice * Quantity; }
        }



    }
}
