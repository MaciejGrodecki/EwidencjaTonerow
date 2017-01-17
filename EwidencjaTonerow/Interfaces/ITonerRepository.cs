using EwidencjaTonerow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EwidencjaTonerow.Interfaces
{
    public interface ITonerRepository: IMainRepository
    {
        List<Toner> GetToners();
        Toner GetTonerByTonerID(int tonerID);
        IEnumerable<Toner> GetPrinterToners(int printerID, IEnumerable<Printer> printers);
        void AddToner(Toner toner);
        void RemoveToner(int tonerID);
        void UpdateToner(Toner toner);

    }
}