using EwidencjaTonerow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EwidencjaTonerow.Models
{
    public class OrderRepository : IOrderRepository
    {
        private EwidencjaContext context;
        private bool disposed = false;

        public OrderRepository(EwidencjaContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Add new order
        /// </summary>
        /// <param name="order">Object of Order class</param>
        public void AddOrder(Order order)
        {
            context.Orders.Add(order);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Return Order's class object by orderID
        /// </summary>
        /// <param name="orderID">orderID value</param>
        /// <returns>Order's object</returns>
        public Order GetOrderByOrderID(int orderID)
        {
            return context.Orders.Find(orderID);
        }

        /// <summary>
        /// Get All Orders
        /// </summary>
        /// <returns>Return ICollection of all order's objects</returns>
        public ICollection<Order> GetOrders()
        {
            return context.Orders.ToList();
        }

        /// <summary>
        /// Remove order
        /// </summary>
        /// <param name="orderID">orderID value</param>
        public void RemoveOrder(int orderID)
        {
            Order order = context.Orders.Find(orderID);
            context.Orders.Remove(order);
        }

        /// <summary>
        /// Save changes to database
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Get all orders without delivery date
        /// </summary>
        /// <param name="descending">true if descending return values, false if not</param>
        /// <returns>Returns ICollection of order's objects without delivery date</returns>
        public ICollection<Order> GetOrdersWithoutDeliveryDate(bool descending)
        {
            var orders = new List<Order>();
            if (descending)
            {
                orders = (from o in context.Orders
                          where o.DeliveryDate == null
                          select o).OrderByDescending(x => x.OrderID).ToList();
            }
            else
            {
                var order = (from o in context.Orders
                             where o.DeliveryDate == null
                             select o);
            }
            return orders;
        }

        /// <summary>
        /// Get all orders with delivery date
        /// </summary>
        /// <param name="descending">true if descending return values, false if not</param>
        /// <returns>Returns ICollection of order's objects with delivery date</returns>
        public ICollection<Order> GetOrdersWithDeliveryDate(bool descending)
        {
            var orders = new List<Order>();
            if (descending)
            {
                orders = (from o in context.Orders
                          where o.DeliveryDate != null
                          select o).OrderByDescending(x => x.OrderID).ToList();
            }
            else
            {
                orders = (from o in context.Orders
                          where o.DeliveryDate != null
                          select o).ToList();
            }
            return orders;
        }

        /// <summary>
        /// Get all orders without change date
        /// </summary>
        /// <param name="descendig">true if descending return values, false if not</param>
        /// <returns>Returns ICollection of order's objects without change date</returns>
        public ICollection<Order> GetOrdersWithoutChangeDate(bool descendig)
        {
            var orders = new List<Order>();
            if (descendig)
            {
                orders = (from o in context.Orders
                          where o.ChangeDate == null
                          select o).OrderByDescending(x => x.OrderID).OrderBy(o => o.SendDate).ToList();
            }
            else
            {
                orders = (from o in context.Orders
                          where o.ChangeDate == null
                          select o).OrderBy(o => o.SendDate).ToList();
            }
            return orders;
        }

        /// <summary>
        /// Get all orders with change date
        /// </summary>
        /// <param name="descendig">true if descending return values, false if not</param>
        /// <returns>Returns ICollection of order's objects wit change date</returns>
        public ICollection<Order> GetOrderWithChangeDate(bool descendig)
        {
            var orders = new List<Order>();
            if (descendig)
            {
                orders = (from o in context.Orders
                          where o.ChangeDate != null
                          select o).OrderByDescending(x => x.OrderID).OrderBy(o => o.SendDate).ToList();
            }
            else
            {
                orders = (from o in context.Orders
                          where o.ChangeDate != null
                          select o).OrderBy(o => o.SendDate).ToList();
            }
            return orders;
        }
    }
}