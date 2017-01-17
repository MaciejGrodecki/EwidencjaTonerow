using EwidencjaTonerow.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace EwidencjaTonerow.Interfaces
{
    public interface IPrinterRepository : IMainRepository
    {
        IEnumerable<Printer> GetPrinters();
        Printer GetPrinterByPrinterID(int printerID);
        void AddPrinter(Printer printer);
        void RemovePrinter(int printerID);
    }
}
