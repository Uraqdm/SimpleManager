using Microsoft.EntityFrameworkCore;
using SimpleManager.Models;

namespace SimpleManager.Context
{
    class SimpleManagerContext: DbContext
    {
        #region Constructors

        public SimpleManagerContext()
        {
            Database.EnsureCreated();
        }

        static SimpleManagerContext()
        {
            DataBase = new SimpleManagerContext();
        }

        #endregion

        #region Properties

        public static SimpleManagerContext DataBase { get; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Record> Journal { get; set; }

        #endregion

        #region overrided methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=URAQDMPC\\SQLEXPRESS;Database=MyManagerDB;Trusted_Connection=True;");
        }

        #endregion
    }
}
