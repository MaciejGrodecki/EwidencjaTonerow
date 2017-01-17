using EwidencjaTonerow.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EwidencjaTonerow.Models
{
    public class TonerRepository : ITonerRepository
    {
        private EwidencjaContext context;
        private bool disposed = false;

        public TonerRepository(EwidencjaContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Add toner
        /// </summary>
        /// <param name="toner">Object of toner class</param>
        public void AddToner(Toner toner)
        {
            context.Toners.Add(toner);
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
        /// Get all toners
        /// </summary>
        /// <param name="printerID">printerID value</param>
        /// <param name="printers">IEnumerable of printer objects</param>
        /// <returns>IEnumerable of printer objects</returns>
        public IEnumerable<Toner> GetPrinterToners(int printerID, IEnumerable<Printer> printers)
        {
            return printers.Where(p => p.PrinterID == printerID).Single().Toners;
        }

        /// <summary>
        /// Get toner object
        /// </summary>
        /// <param name="tonerID">tonerID value</param>
        /// <returns>Returns object of toner class</returns>
        public Toner GetTonerByTonerID(int tonerID)
        {
            return context.Toners.Find(tonerID);
        }

        /// <summary>
        /// Get all toners
        /// </summary>
        /// <returns>List of toner object</returns>
        public List<Toner> GetToners()
        {
            return context.Toners.OrderBy(t => t.Name).ToList();
        }

        /// <summary>
        /// Check if the toner name already exist
        /// </summary>
        /// <param name="name">toner name</param>
        /// <returns>true if name exists, false if not</returns>
        public bool IsUniqueName(string name)
        {
            bool result = context.Toners.Any(t => t.Name == name.ToUpper());

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
        /// Remove toner
        /// </summary>
        /// <param name="tonerID">tonerID value</param>
        public void RemoveToner(int tonerID)
        {
            Toner toner = context.Toners.Find(tonerID);
            context.Toners.Remove(toner);
        }

        /// <summary>
        /// Save changes to database
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Update toner informations
        /// </summary>
        /// <param name="toner">Object of toner class</param>
        public void UpdateToner(Toner toner)
        {
            context.Entry(toner).State = EntityState.Modified;
        }
    }
}