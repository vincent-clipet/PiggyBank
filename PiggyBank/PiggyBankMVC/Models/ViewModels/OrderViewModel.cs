namespace PiggyBankMVC.Models.ViewModels
{
    public class OrderViewModel
    {
        public virtual Order? Order { get; set; }
        public virtual List<OrderDetail>? Details { get; set; }
    }
}
