namespace PiggyBankMVC.Models.ViewModels
{
    public class OrderDetailsViewModel
    {
        public virtual Order? Order { get; set;}
        public virtual Product? Product { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int OrderDetailId { get; set;}
    }
}
