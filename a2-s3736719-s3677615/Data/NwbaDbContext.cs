using System;
using a2_s3736719_s3677615.Models;
using Microsoft.EntityFrameworkCore;

namespace a2_s3736719_s3677615.Data
{
    public class NwbaDbContext : DbContext
    {

        // connect to database
        public NwbaDbContext(DbContextOptions<NwbaDbContext> options) : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Payee> Payees { get; set; }
        public DbSet<BillPay> BillPays { get; set; }

        // fluent API to set further constraint
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Account>()
                .Property(o => o.AccountNumber)
                .HasDefaultValueSql("NEXT VALUE FOR shared.AccountSequence");

            builder.Entity<Payee>()
                .Property(o => o.PayeeID)
                .HasDefaultValueSql("NEXT VALUE FOR shared.PayeeSequence");

            //builder.Entity<Transaction>()
            //    .HasKey(c => new { c.TransactionID, c.TransactionType });

            builder.Entity<Transaction>()
               .Property(o => o.TransactionID)
               .HasDefaultValueSql("NEXT VALUE FOR shared.TransactionSequence");

            builder.Entity<BillPay>()
               .Property(o => o.BillPayID)
               .HasDefaultValueSql("NEXT VALUE FOR shared.BillPaySequence");


            builder.Entity<Account>()
                .HasCheckConstraint("CH_Account_Balance", "Balance >= 0");

            builder.Entity<Transaction>()
                .HasOne(x => x.Account)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.AccountNumber);

            builder.Entity<Transaction>()
                .HasCheckConstraint("CH_Transaction_Amount", "Amount > 0");
        }

    }
}
