namespace ecommerceWeb_trytry.Models
{
    public class Product
    {


        public  int ProductId { get; set; } //auto generated
        public required string ProductSellerEmail { get; set; }  // seller's user id
        public required string ProductSellerName{ get; set; }  
        public required string ProductName { get; set; }
        public required double ProductPrice { get; set; }
        public required string ProductImage { get; set; }
        public required string ProductType { get; set; }
        public required string ProductDescription { get; set; }
        public required DateTime ProductProduceTime { get; set; }

        public required int Stock { get; set; }



        public int Quantity { get; set; }




    }
}
