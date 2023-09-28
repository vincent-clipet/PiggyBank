namespace PiggyBankMVC.Models.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel(Order? o, List<OrderDetail> od)
        {
            Order = o;
            Details = od;
            TotalPrice = Order.getTotalPrice(od);
            TotalProducts = Order.getTotalProducts(od);
            UniqueProducts = Order.getUniqueProducts(od);
        }

        public virtual Order? Order { get; set; }
        public virtual List<OrderDetail>? Details { get; set; }
        public decimal TotalPrice { get; set; }
        public int? TotalProducts { get; set; }
        public int? UniqueProducts { get; set; }
    }
}
