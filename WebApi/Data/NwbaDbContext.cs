using System;
using WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Data
{
    public class NwbaDbContext : DbContext
    {

        // connect to database
        public NwbaDbContext(DbContextOptions<NwbaDbContext> options) : base(options)
        { }

        public DbSet<CustomerAPI> Customers { get; set; }
        public DbSet<LoginAPI> Logins { get; set; }
        public DbSet<AccountAPI> Accounts { get; set; }
        public DbSet<TransactionAPI> Transactions { get; set; }
        public DbSet<PayeeAPI> Payees { get; set; }
        public DbSet<BillPayAPI> BillPays { get; set; }

        // fluent API to set further constraint
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.HasSequence<int>("AccountSequence", schema: "shared")
            //    .StartsAt(1000)
            //    .IncrementsBy(1);


            base.OnModelCreating(builder);
            builder.Entity<AccountAPI>()
                .Property(o => o.AccountNumber)
                .HasDefaultValueSql("NEXT VALUE FOR shared.AccountSequence");

            builder.Entity<PayeeAPI>()
                .Property(o => o.PayeeID)
                .HasDefaultValueSql("NEXT VALUE FOR shared.PayeeSequence");

            // composite keys
            // tedious?
            builder.Entity<TransactionAPI>()
                .HasKey(c => new { c.TransactionID, c.TransactionType });

            builder.Entity<TransactionAPI>()
               .Property(o => o.TransactionID)
               .HasDefaultValueSql("NEXT VALUE FOR shared.TransactionSequence");

            builder.Entity<BillPayAPI>()
               .Property(o => o.BillPayID)
               .HasDefaultValueSql("NEXT VALUE FOR shared.BillPaySequence");


            builder.Entity<AccountAPI>()
                .HasCheckConstraint("CH_Account_Balance", "Balance >= 0");

            builder.Entity<TransactionAPI>()
                .HasOne(x => x.Account)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.AccountNumber);

            builder.Entity<TransactionAPI>()
                .HasCheckConstraint("CH_Transaction_Amount", "Amount > 0");
        }

    }
}
