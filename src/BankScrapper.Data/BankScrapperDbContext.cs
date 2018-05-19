using BankScrapper.Domain.Entities;
using System.Data.Entity;

namespace BankScrapper.Data
{
    public sealed class BankScrapperDbContext : DbContext
    {
        public BankScrapperDbContext(string connectionStringName) : base(connectionStringName)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}