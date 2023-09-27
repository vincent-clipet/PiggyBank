using PiggyBankMVC.Utils;

namespace PiggyBankMVC.Models.ViewModels
{
    public class OrderViewModel
    {
        public virtual Order? Order { get; set; }
        public virtual List<OrderDetail>? Details { get; set; }
        public int? TotalPrice { get; set; }
        public int? TotalProducts { get; set; }
        public int? UniqueProducts { get; set; }
    }
}
