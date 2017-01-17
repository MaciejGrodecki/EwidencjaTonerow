using EwidencjaTonerow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EwidencjaTonerow.Interfaces
{
    public interface IStorehouseRepository
    {
        ICollection<Storehouse> GetAllItems();
        Storehouse GetItemByStorehouseID(int storehouseID);
        Storehouse GetItemByPrinterIdWithoutRoom(int printerID);
        ICollection<Storehouse> GetItemsByPrinterIdWithUniqueRoom(int printerID);
        ICollection<Storehouse> GetItemByPrinterIdWithoutToner(int printerID);
        ICollection<Storehouse> GetItemsByPrinterIdWithToner(int printerID);
        ICollection<Storehouse> GetItemsByPrinterIdWithUniqueToner(int printerID);
        Storehouse GetStorehouseItem(int printerID, int roomID, int tonerID);
        void Fill(Storehouse storehouse);
        void Add(Storehouse storehouse);
        void AddToner(int printerID, int roomID, int tonerID);
        void SubstractToner(int printerId, int roomID, int tonerID);
        int GetStorehouseID(int printerId, int roomID, int tonerID);
        void Remove(int storehouseID);
        void Save();
    }
}