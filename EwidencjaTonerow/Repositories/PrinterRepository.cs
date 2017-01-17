using EwidencjaTonerow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EwidencjaTonerow.Models
{
    public class PrinterRepository : IPrinterRepository
    {
        private EwidencjaContext context;
        private bool disposed = false;

        public PrinterRepository(EwidencjaContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Add new printer
        /// </summary>
        /// <param name="printer">Object of Printer class</param>
        public void AddPrinter(Printer printer)
        {
            context.Printers.Add(printer);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) context.Dispose();
            }
            this.disposed = true;
        }

        /// <summary>
        /// Get all printers
        /// </summary>
        /// <returns>Return IEnumerable of all printer's objects </returns>
        public IEnumerable<Printer> GetPrinters()
        {
     
            return context.Printers.ToList().OrderBy(p=> p.Name);
        }

        /// <summary>
        /// Remove printer
        /// </summary>
        /// <param name="printerID">printerID value</param>
        public void RemovePrinter(int printerID)
        {
            Printer printer = context.Printers.Find(printerID);
            context.Printers.Remove(printer);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Save changes to database
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Return printer's class object by printerID
        /// </summary>
        /// <param name="printerID">printerID value</param>
        /// <returns>Printer's object</returns>
        public Printer GetPrinterByPrinterID(int printerID)
        {
            return context.Printers.Find(printerID);
        }

        /// <summary>
        /// Check if the printer name already exist 
        /// </summary>
        /// <param name="name">printer name</param>
        /// <returns>true if name exists, false if not</returns>
        public bool IsUniqueName(string name)
        {
            bool result = context.Printers.Any(p => p.Name == name.ToUpper());

            if (result)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}