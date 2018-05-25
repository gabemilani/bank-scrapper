using BankScrapper.Domain.Entities;
using System.Data.Entity;

namespace BankScrapper.Data
{
    public sealed class BankScrapperDbContext : DbContext
    {
        public BankScrapperDbContext(string connectionStringName) : base(connectionStringName)
        {
        }

        ~BankScrapperDbContext()
        {
            Dispose(false);
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            CreateAccountModel(modelBuilder);
            CreateBillModel(modelBuilder);
            CreateCardModel(modelBuilder);
            CreateCustomerModel(modelBuilder);
            CreateTransactionModel(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void CreateAccountModel(DbModelBuilder modelBuilder)
        {
            var config = modelBuilder.Entity<Account>();
            config.HasKey(a => a.Id);
            config.Property(a => a.Bank).IsRequired();
            config.Property(a => a.Type).IsRequired();
            config.HasOptional(a => a.Customer).WithMany().HasForeignKey(a => a.CustomerId);
            config.ConfigTable();
        }

        private void CreateBillModel(DbModelBuilder modelBuilder)
        {
            var config = modelBuilder.Entity<Bill>();
            config.HasKey(b => b.Id);
            config.Property(b => b.OpenDate).IsRequired();
            config.Property(b => b.CloseDate).IsRequired();
            config.Property(b => b.State).IsRequired();
            config.Property(b => b.Total).IsRequired();
            config.HasRequired(b => b.Account).WithMany().HasForeignKey(b => b.AccountId);
            config.ConfigTable();
        }

        private void CreateCardModel(DbModelBuilder modelBuilder)
        {
            var config = modelBuilder.Entity<Card>();
            config.HasKey(c => c.Id);
            config.Property(c => c.PrintedName).IsRequired();
            config.Property(c => c.Type).IsRequired();
            config.HasRequired(c => c.Account).WithMany().HasForeignKey(c => c.AccountId);
            config.ConfigTable();
        }

        private void CreateCategoryModel(DbModelBuilder modelBuilder)
        {
            var config = modelBuilder.Entity<Category>();
            config.HasKey(c => c.Id);
            config.HasIndex(c => c.Name).IsUnique();
            config.Property(c => c.Name).IsRequired();
            config.ConfigTable();
        }

        private void CreateCustomerModel(DbModelBuilder modelBuilder)
        {
            var config = modelBuilder.Entity<Customer>();
            config.HasKey(c => c.Id);
            config.Property(c => c.Name).IsRequired();
            config.Property(c => c.Cpf).IsRequired();
            config.ConfigTable();
        }

        private void CreateTransactionModel(DbModelBuilder modelBuilder)
        {
            var config = modelBuilder.Entity<Transaction>();
            config.HasKey(t => t.Id);
            config.Property(t => t.Amount).IsRequired();
            config.Property(t => t.Date).IsRequired();
            config.HasRequired(t => t.Account).WithMany().HasForeignKey(t => t.AccountId);
            config.HasOptional(t => t.Category).WithMany().HasForeignKey(t => t.CategoryId);
            config.ConfigTable();
        }
    }
}