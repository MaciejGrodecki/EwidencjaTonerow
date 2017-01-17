using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EwidencjaTonerow.Models
{
    public class EwidencjaContext : DbContext
    {
        public EwidencjaContext() : base("name=tonersDB")
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Printer> Printers { get; set; }
        public DbSet<Toner> Toners { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Storehouse> Storehouses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Printer>()
                .HasMany(p => p.Rooms).WithMany(i => i.Printers)
                .Map(t => t.MapLeftKey("PrinterID")
                .MapRightKey("RoomID")
                .ToTable("PrinterRoom"));

            modelBuilder.Entity<Printer>()
                .HasMany(p => p.Toners).WithMany(i => i.Printers)
                .Map(t => t.MapLeftKey("PrinterID")
                .MapRightKey("TonerID")
                .ToTable("PrinterToner"));
        }
    }
}