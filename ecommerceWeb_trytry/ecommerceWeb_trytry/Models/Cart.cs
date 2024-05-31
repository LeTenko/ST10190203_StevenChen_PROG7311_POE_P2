namespace ecommerceWeb_trytry.Models
{
    public class Cart
    {


        public string Id { get; set; }
        public List<Item> Items { get; set; }
        public double TotalCost=> Items.Sum(item => item.Cost); //calculate total cost



        //initialize the list of items
        public Cart()
        {
            Items=new List<Item>();
        }




    }
}
