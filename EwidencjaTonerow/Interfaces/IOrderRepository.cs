using EwidencjaTonerow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwidencjaTonerow.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrders();
        ICollection<Order> GetOrdersWithoutDeliveryDate(bool descending);
        ICollection<Order> GetOrdersWithDeliveryDate(bool descending);
        ICollection<Order> GetOrdersWithoutChangeDate(bool descendig);
        ICollection<Order> GetOrderWithChangeDate(bool descendig);
        Order GetOrderByOrderID(int orderID);
        void AddOrder(Order order);
        void RemoveOrder(int orderID);
        void Save();
    }
}
