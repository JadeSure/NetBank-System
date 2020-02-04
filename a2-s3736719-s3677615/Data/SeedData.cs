using System;
using System.Linq;
using a2_s3736719_s3677615.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace a2_s3736719_s3677615.Data
{

    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new NwbaDbContext(serviceProvider.GetRequiredService<DbContextOptions<NwbaDbContext>>());

            //Look for customers.
            if (context.Customers.Any())
                return; // DB has already been seeded.

            // set default data to database by data seed
            //context.Customers.AddRange(
            //    new Customer
            //    {
            //        CustomerID = 2100,
            //        CustomerName = "Matthew Bolger",
            //        Address = "123 Fake Street",
            //        City = "Melbourne",
            //        State = State.VIC,
            //        PostCode = "3000",
            //        Phone = "(61)- 1234 5678"
            //    },
            //    new Customer
            //    {
            //        CustomerID = 2200,
            //        CustomerName = "Rodney Cocker",
            //        Address = "456 Real Road",
            //        City = "Melbourne",
            //        State = State.VIC,
            //        PostCode = "3005",
            //        Phone = "(61)- 2345 6789"
            //    },
            //    new Customer
            //    {
            //        CustomerID = 2300,
            //        CustomerName = "Shekhar Kalra",
            //        Phone = "(61)- 3456 7890"
            //    });

            const string format = "dd/MM/yyyy hh:mm:ss tt";

            //context.Logins.AddRange(
            //    new Login
            //    {
            //        UserID = "12345678",
            //        CustomerID = 2100,
            //        PasswordHash = "YBNbEL4Lk8yMEWxiKkGBeoILHTU7WZ9n8jJSy8TNx0DAzNEFVsIVNRktiQV+I8d2",
            //        ModifyDate = DateTime.ParseExact("01/01/2019 08:00:00 PM", format, null)
            //    },
            //    new Login
            //    {
            //        UserID = "38074569",
            //        CustomerID = 2200,
            //        PasswordHash = "EehwB3qMkWImf/fQPlhcka6pBMZBLlPWyiDW6NLkAh4ZFu2KNDQKONxElNsg7V04",
            //        ModifyDate = DateTime.ParseExact("01/01/2019 08:00:00 PM", format, null)
            //    },
            //    new Login
            //    {
            //        UserID = "17963428",
            //        CustomerID = 2300,
            //        PasswordHash = "LuiVJWbY4A3y1SilhMU5P00K54cGEvClx5Y+xWHq7VpyIUe5fe7m+WeI0iwid7GE",
            //        ModifyDate = DateTime.ParseExact("01/01/2019 08:00:00 PM", format, null)
            //    });



            //context.Accounts.AddRange(
            //    new Account
            //    {
            //        AccountType = AccountType.S,
            //        CustomerID = 2100,
            //        Balance = 100,
            //        ModifyDate = DateTime.ParseExact("01/01/2019 08:00:00 PM", format, null)
            //    },
            //    new Account
            //    {
            //        AccountType = AccountType.C,
            //        CustomerID = 2100,
            //        Balance = 500,
            //        ModifyDate = DateTime.ParseExact("01/01/2019 08:00:00 PM", format, null)

            //    },
            //    new Account
            //    {
            //        AccountType = AccountType.S,
            //        CustomerID = 2200,
            //        ModifyDate = DateTime.ParseExact("01/01/2019 08:00:00 PM", format, null)
            //    },
            //    new Account
            //    {
            //        AccountType = AccountType.C,
            //        CustomerID = 2300,
            //        ModifyDate = DateTime.ParseExact("01/01/2019 08:00:00 PM", format, null)
            //    });

            //context.Payees.AddRange(
            //new Payee
            //{
            //    PayeeName = "Jin",
            //    Address = "La la land",
            //    City = "Melbourne",
            //    State = "VIC",
            //    PostCode = "3000",
            //    Phone = "(61)- 3456 7890"
            //});


            const string openingBalance = "Opening balance";

            context.Transactions.AddRange(
                new Transaction
                {
                    TransactionType = TransactionType.D,
                    AccountNumber = 1008,
                    Amount = 100,
                    Comment = openingBalance,
                    ModifyDate = DateTime.ParseExact("19/12/2019 08:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.D,
                    AccountNumber = 1009,
                    Amount = 500,
                    Comment = openingBalance,
                    ModifyDate = DateTime.ParseExact("19/12/2019 08:30:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.D,
                    AccountNumber = 1010,
                    Amount = 500.95m,
                    Comment = openingBalance,
                    ModifyDate = DateTime.ParseExact("19/12/2019 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.D,
                    AccountNumber = 1011,
                    Amount = 1250.50m,
                    Comment = openingBalance,
                    ModifyDate = DateTime.ParseExact("19/12/2019 10:00:00 PM", format, null)
                });



            //context.BillPays.AddRange(
            //    new BillPay
            //    {
            //        AccountNumber = ,
            //        PayeeID = ,
            //        Amount = ,
            //        ScheduleDate = DateTime.ParseExact("19/12/2019 10:00:00 PM", format, null),
            //        Period = Period.S,
            //        ModifyDate = DateTime.ParseExact("19/12/2019 10:00:00 PM", format, null)
            //    });

            context.SaveChanges();
        }
    }
}