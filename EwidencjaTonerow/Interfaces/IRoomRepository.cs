using EwidencjaTonerow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwidencjaTonerow.Interfaces
{
    public interface IRoomRepository : IMainRepository
    {
        List<Room> GetRooms();
        Room GetRoomByRoomID(int roomID);
        IEnumerable<Room> GetPrinterRooms(int printerID, IEnumerable<Printer> printers);
        void AddRoom(Room room);
        void RemoveRoom(int roomID);
    }
}
