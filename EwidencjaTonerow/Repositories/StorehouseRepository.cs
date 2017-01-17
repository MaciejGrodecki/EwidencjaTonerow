using EwidencjaTonerow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EwidencjaTonerow.Models
{
    public class StorehouseRepository : IStorehouseRepository
    {
        private EwidencjaContext context;
        private bool disposed = false;

        public StorehouseRepository(EwidencjaContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Add new storehouse item
        /// </summary>
        /// <param name="storehouse">Object of storehouse class</param>
        public void Add(Storehouse storehouse)
        {
            context.Storehouses.Add(storehouse); 
        }

        /// <summary>
        /// Get all storehouse items
        /// </summary>
        /// <returns>Return ICollection of all storehouse items</returns>
        public ICollection<Storehouse> GetAllItems()
        {
            return context.Storehouses.OrderBy(x => x.Printer.Name).ToList();
        }

        /// <summary>
        /// Return storehouse item by storehouseID
        /// </summary>
        /// <param name="storehouseID">storehouseID value</param>
        /// <returns>Storehouse's object</returns>
        public Storehouse GetItemByStorehouseID(int storehouseID)
        {
            return context.Storehouses.Find(storehouseID);
        }

        /// <summary>
        /// Remove storehouse item
        /// </summary>
        /// <param name="storehouseID">storehouseID value</param>
        public void Remove(int storehouseID)
        {
            Storehouse storehouse = GetItemByStorehouseID(storehouseID);
            context.Storehouses.Remove(storehouse);
        }

        /// <summary>
        /// Save changes to database
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) context.Dispose();
            }
            this.disposed = true;
        }

        /// <summary>
        /// Get storehouse item without room
        /// </summary>
        /// <param name="printerID">printerID value</param>
        /// <returns>Object of storehouse class</returns>
        public Storehouse GetItemByPrinterIdWithoutRoom(int printerID)
        {
            var storehouse = (from s in context.Storehouses
                              where s.PrinterID == printerID
                              && s.RoomID == null
                              select s).SingleOrDefault();
            return storehouse;
        }

        /// <summary>
        /// Get all storehouse items with unique room
        /// </summary>
        /// <param name="printerID">printerID value</param>
        /// <returns>Returns ICollection of storehouse objects with unique room</returns>
        public ICollection<Storehouse> GetItemsByPrinterIdWithUniqueRoom(int printerID)
        {
            var storehouseItems = from s in context.Storehouses
                                  where s.PrinterID == printerID
                                  group s by s.RoomID into groups
                                  select groups.FirstOrDefault();
            return storehouseItems.ToList();
        }

        /// <summary>
        /// Get all storehouse items without toner
        /// </summary>
        /// <param name="printerID">printerID value</param>
        /// <returns>Returns ICollection of storehouse objects without toner</returns>
        public ICollection<Storehouse> GetItemByPrinterIdWithoutToner(int printerID)
        {
            var storehouse = (from s in context.Storehouses
                              where s.PrinterID == printerID
                              && s.TonerID == null
                              select s);
            return storehouse.ToList();
        }

        /// <summary>
        /// Get all storehouse items with toner
        /// </summary>
        /// <param name="printerID">printerID value</param>
        /// <returns>Returns ICollection of storehouse objecs with toner</returns>
        public ICollection<Storehouse> GetItemsByPrinterIdWithToner(int printerID)
        {
            var storehouseItems = (from s in context.Storehouses
                                   where s.PrinterID == printerID
                                   && s.Toner != null
                                   select s).ToList();
            return storehouseItems;
        }

        /// <summary>
        /// Get all storehouse items with unique toner
        /// </summary>
        /// <param name="printerID">printerID value</param>
        /// <returns>Returns ICollection of storehouse objects with unique toner</returns>
        public ICollection<Storehouse> GetItemsByPrinterIdWithUniqueToner(int printerID)
        {
            var storehouseItems = from s in context.Storehouses
                                  where s.PrinterID == printerID
                                  group s by s.TonerID into groups
                                  select groups.FirstOrDefault();
            return storehouseItems.ToList();
        }

        public void Fill(Storehouse storehouse)
        {
            if (storehouse.TonerID != null)
            {
                ICollection<Storehouse> storehouseItems = GetItemsByPrinterIdWithUniqueRoom(storehouse.PrinterID);
                foreach (var s in storehouseItems)
                {
                    Storehouse store = new Storehouse();
                    store.PrinterID = s.PrinterID;
                    store.RoomID = s.RoomID;
                    store.TonerID = storehouse.TonerID;
                    context.Storehouses.Add(store);
                }
            }

            if (storehouse.RoomID != null)
            {
                ICollection<Storehouse> storehouseItems = GetItemsByPrinterIdWithUniqueToner(storehouse.PrinterID);
                if (storehouseItems.Count > 0)
                {
                    foreach (var s in storehouseItems)
                    {
                        Storehouse store = new Storehouse();
                        store.PrinterID = s.PrinterID;
                        store.RoomID = storehouse.RoomID;
                        store.TonerID = s.TonerID;
                        context.Storehouses.Add(store);
                    }
                }
                else
                {
                    context.Storehouses.Add(storehouse);
                }
            }
        }

        /// <summary>
        /// Add toner to specify storehouse item
        /// </summary>
        /// <param name="printerID">printerID value</param>
        /// <param name="roomID">roomID value</param>
        /// <param name="tonerID">tonerID value</param>
        public void AddToner(int printerID, int roomID, int tonerID)
        {
            var storehouseitem = GetStorehouseItem(printerID, roomID, tonerID);

            storehouseitem.Count++;
            context.SaveChanges();
        }

        /// <summary>
        /// Reduce the amount of toners
        /// </summary>
        /// <param name="printerID">printerID value</param>
        /// <param name="roomID">roomID value</param>
        /// <param name="tonerID">tonerID value</param>
        public void SubstractToner(int printerID, int roomID, int tonerID)
        {
            var storehouseitem = GetStorehouseItem(printerID, roomID, tonerID);

            storehouseitem.Count--;
            context.SaveChanges();
        }

        /// <summary>
        /// Get storehouseitem
        /// </summary>
        /// <param name="printerID">printerID value</param>
        /// <param name="roomID">roomID value</param>
        /// <param name="tonerID">tonerID value</param>
        /// <returns>Returns object of storehouse class</returns>
        public Storehouse GetStorehouseItem(int printerID, int roomID, int tonerID)
        {
            var storehouseitem = (from s in context.Storehouses
                                  where s.PrinterID == printerID
                                  && s.RoomID == roomID
                                  && s.TonerID == tonerID
                                  select s).SingleOrDefault();

            return storehouseitem;
        }

        /// <summary>
        /// Get storehouseID value
        /// </summary>
        /// <param name="printerID">printerID value</param>
        /// <param name="roomID">roomID value</param>
        /// <param name="tonerID">tonerID value</param>
        /// <returns></returns>
        public int GetStorehouseID(int printerID, int roomID, int tonerID)
        {
            var storehouseitem = GetStorehouseItem(printerID, roomID, tonerID);
            return storehouseitem.StorehouseID;
        }
    }
}