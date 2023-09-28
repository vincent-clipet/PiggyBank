namespace PiggyBankMVC.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart? ShoppingCart { get; set; }
        public decimal TotalPrice { get; set; }
        public string? LastUrl { get; set; }
    }
}
