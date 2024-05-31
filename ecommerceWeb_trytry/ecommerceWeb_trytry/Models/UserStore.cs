using ecommerceWeb_trytry.Models;

namespace ecommerceWeb_trytry_trail2.Models
{
    public class UserStore
    {


        public required string ProductSellerEmail { get; set; }  
        public List<Item> Items { get; set; }
       

        //initialize the list of items
        public UserStore ()
        {
            Items = new List<Item>();
        }





    }
}
