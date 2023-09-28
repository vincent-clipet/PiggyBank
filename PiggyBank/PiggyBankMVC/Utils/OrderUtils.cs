using PiggyBankMVC.DataAccessLayer;
using PiggyBankMVC.Models;

namespace PiggyBankMVC.Utils
{
    public static class OrderUtils
    {
        public static decimal getTotalPrice(List<OrderDetail> od)
        {
            return od.Aggregate(0.0M, (acc, orderDetail) => (acc + orderDetail.Price));
        }

        public static int? getTotalProducts(List<OrderDetail> od)
        {
            return od.Aggregate(0, (acc, orderDetail) => acc + orderDetail.Quantity);
        }

        public static int? getUniqueProducts(List<OrderDetail> od)
        {
            return od.Count;
        }
    }
}