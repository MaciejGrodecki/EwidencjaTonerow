using EwidencjaTonerow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EwidencjaTonerow.Models
{
    public class RoomRepository : IRoomRepository
    {
        private EwidencjaContext context;
        private bool disposed = false;

        public RoomRepository(EwidencjaContext context)
        {
            this.context = context;
        }

        public void AddRoom(Room room)
        {
            context.Rooms.Add(room);
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
        /// Get all rooms where is specify printer
        /// </summary>
        /// <param name="printerID">printerID value</param>
        /// <param name="printers">IEnumerable of printer's objects</param>
        /// <returns>Return IEnumerable of room's objects</returns>
        public IEnumerable<Room> GetPrinterRooms(int printerID, IEnumerable<Printer> printers)
        {
            return printers.Where(p => p.PrinterID == printerID).Single().Rooms;
        }

        /// <summary>
        /// Get room's object
        /// </summary>
        /// <param name="roomID">roomID value</param>
        /// <returns>Return object of room</returns>
        public Room GetRoomByRoomID(int roomID)
        {
            return context.Rooms.Find(roomID);
        }

        /// <summary>
        /// Get all rooms
        /// </summary>
        /// <returns>Return List of room's objects</returns>
        public List<Room> GetRooms()
        {
            return context.Rooms.ToList().OrderBy(r => r.Name).ToList();
        }

        /// <summary>
        /// Check if the room name already exist 
        /// </summary>
        /// <param name="name">room name</param>
        /// <returns>true if name exists, false if not</returns>
        public bool IsUniqueName(string name)
        {
            bool result = context.Rooms.Any(r => r.Name == name.ToUpper());

            if (result)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Remove room
        /// </summary>
        /// <param name="roomID">roomID value</param>
        public void RemoveRoom(int roomID)
        {
            Room room = context.Rooms.Find(roomID);
            context.Rooms.Remove(room);
        }

        /// <summary>
        /// Save changes to database
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }
    }
}