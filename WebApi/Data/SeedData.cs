using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Models;


namespace WebApi.Data
{

    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new NwbaDbContext(serviceProvider.GetRequiredService<DbContextOptions<NwbaDbContext>>());

            //Look for customers.
            // DB has already been seeded.
            if (context.Customers.Any())
                return;

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

            //context.Transactions.AddRange(
            //    new TransactionAPI
            //    {
            //        TransactionType = TransactionType.D,
            //        AccountNumber = 1012,
            //        Amount = 100,
            //        Comment = openingBalance,
            //        ModifyDate = DateTime.ParseExact("19/12/2019 08:00:00 PM", format, null)
            //    },
            //    new TransactionAPI
            //    {
            //        TransactionType = TransactionType.D,
            //        AccountNumber = 1013,
            //        Amount = 500,
            //        Comment = openingBalance,
            //        ModifyDate = DateTime.ParseExact("19/12/2019 08:30:00 PM", format, null)
            //    },
            //    new TransactionAPI
            //    {
            //        TransactionType = TransactionType.D,
            //        AccountNumber = 1014,
            //        Amount = 500.95m,
            //        Comment = openingBalance,
            //        ModifyDate = DateTime.ParseExact("19/12/2019 09:00:00 PM", format, null)
            //    },
            //    new TransactionAPI
            //    {
            //        TransactionType = TransactionType.D,
            //        AccountNumber = 1015,
            //        Amount = 1250.50m,
            //        Comment = openingBalance,
            //        ModifyDate = DateTime.ParseExact("19/12/2019 10:00:00 PM", format, null)
            //    });

            //context.Transactions.AddRange(
            //    // transaction for 1040 in the past 4 month for transfer
            //    new TransactionAPI
            //    {
            //        TransactionType = TransactionType.T,
            //        AccountNumber = 1040,
            //        DestinationAccountNumber = 1043,
            //        Amount = 100,
            //        Comment = "party",
            //        ModifyDate = DateTime.ParseExact("09/01/2020 08:00:00 PM", format, null)
            //    },

            //    new TransactionAPI
            //    {
            //        TransactionType = TransactionType.S,
            //        AccountNumber = 1040,

            //        Amount = 0.2m,
            //        Comment = "Servic Charge for transfer",
            //        ModifyDate = DateTime.ParseExact("09/01/2020 08:00:00 PM", format, null)
            //    },

            //      new TransactionAPI
            //      {
            //          TransactionType = TransactionType.T,
            //          AccountNumber = 1040,
            //          DestinationAccountNumber = 1042,
            //          Amount = 200,
            //          Comment = "party",
            //          ModifyDate = DateTime.ParseExact("15/01/2020 09:00:00 PM", format, null)
            //      },

            //    new TransactionAPI
            //    {
            //        TransactionType = TransactionType.S,
            //        AccountNumber = 1040,

            //        Amount = 0.2m,
            //        Comment = "Servic Charge for transfer",
            //        ModifyDate = DateTime.ParseExact("15/01/2020 09:00:00 PM", format, null)
            //    },

            //      new TransactionAPI
            //      {
            //          TransactionType = TransactionType.T,
            //          AccountNumber = 1040,
            //          DestinationAccountNumber = 1043,
            //          Amount = 666,
            //          Comment = "party",
            //          ModifyDate = DateTime.ParseExact("25/01/2020 08:00:00 PM", format, null)
            //      },

            //    new TransactionAPI
            //    {
            //        TransactionType = TransactionType.S,
            //        AccountNumber = 1040,

            //        Amount = 0.2m,
            //        Comment = "Servic Charge for transfer",
            //        ModifyDate = DateTime.ParseExact("25/01/2020 08:00:00 PM", format, null)
            //    },
            //    // end of one month

            //    // transaction for 1040 in the past 4 month for withdraw
            //    new TransactionAPI
            //    {
            //        TransactionType = TransactionType.W,
            //        AccountNumber = 1040,

            //        Amount = 250,
            //        Comment = "For living",
            //        ModifyDate = DateTime.ParseExact("11/01/2020 08:00:00 PM", format, null)
            //    },

            //    new TransactionAPI
            //    {
            //        TransactionType = TransactionType.S,
            //        AccountNumber = 1040,

            //        Amount = 0.1m,
            //        Comment = "Servic Charge for withdraw",
            //        ModifyDate = DateTime.ParseExact("11/01/2020 08:00:00 PM", format, null)
            //    },

            //      new TransactionAPI
            //      {
            //          TransactionType = TransactionType.W,
            //          AccountNumber = 1040,

            //          Amount = 150,
            //          Comment = "For living",
            //          ModifyDate = DateTime.ParseExact("16/01/2020 08:00:00 PM", format, null)
            //      },

            //    new TransactionAPI
            //    {
            //        TransactionType = TransactionType.S,
            //        AccountNumber = 1040,

            //        Amount = 0.1m,
            //        Comment = "Servic Charge for withdraw",
            //        ModifyDate = DateTime.ParseExact("16/01/2020 09:00:00 PM", format, null)
            //    },

            //      new TransactionAPI
            //      {
            //          TransactionType = TransactionType.W,
            //          AccountNumber = 1040,

            //          Amount = 100,
            //          Comment = "Buy gift to Jin",
            //          ModifyDate = DateTime.ParseExact("28/01/2020 08:00:00 PM", format, null)
            //      },

            //    new TransactionAPI
            //    {
            //        TransactionType = TransactionType.S,
            //        AccountNumber = 1040,

            //        Amount = 0.1m,
            //        Comment = "Servic Charge for transfer",
            //        ModifyDate = DateTime.ParseExact("28/01/2020 08:00:00 PM", format, null)
            //    },

            //    // extra fees for bills
            //    new TransactionAPI
            //    {
            //        TransactionType = TransactionType.B,
            //        AccountNumber = 1040,

            //        Amount = 300,
            //        Comment = "Bills",
            //        ModifyDate = DateTime.ParseExact("30/01/2020 08:00:00 PM", format, null)
            //    },
            //    // end of one month

            //    // line from lovely Jin

            //    // transaction for 1040 in the past 4 month for Deposit
            //    new TransactionAPI
            //    {
            //        TransactionType = TransactionType.D,
            //        AccountNumber = 1040,

            //        Amount = 500,
            //        Comment = "Repair computer",
            //        ModifyDate = DateTime.ParseExact("13/01/2020 08:00:00 PM", format, null)
            //    },



            //      new TransactionAPI
            //      {
            //          TransactionType = TransactionType.D,
            //          AccountNumber = 1040,

            //          Amount = 1000,
            //          Comment = "design software",
            //          ModifyDate = DateTime.ParseExact("19/01/2020 08:00:00 PM", format, null)
            //      },



            //      new TransactionAPI
            //      {
            //          TransactionType = TransactionType.D,
            //          AccountNumber = 1040,

            //          Amount = 800,
            //          Comment = "rental fees from tenants",
            //          ModifyDate = DateTime.ParseExact("29/01/2020 08:00:00 PM", format, null)
            //      },

            //        new TransactionAPI
            //        {
            //            TransactionType = TransactionType.D,
            //            AccountNumber = 1040,

            //            Amount = 500,
            //            Comment = "stock incomes",
            //            ModifyDate = DateTime.ParseExact("03/02/2020 08:00:00 PM", format, null)
            //        }

            // end of one month

            // account for 1042
            context.Transactions.AddRange(
        // transaction for 1040 in the past 4 month for transfer
        new TransactionAPI
        {
            TransactionType = TransactionType.T,
            AccountNumber = 1042,
            DestinationAccountNumber = 1043,
            Amount = 100,
            Comment = "party",
            ModifyDate = DateTime.ParseExact("11/01/2020 08:00:00 PM", format, null)
        },

        new TransactionAPI
        {
            TransactionType = TransactionType.S,
            AccountNumber = 1042,

            Amount = 0.2m,
            Comment = "Servic Charge for transfer",
            ModifyDate = DateTime.ParseExact("11/01/2020 08:00:00 PM", format, null)
        },

          new TransactionAPI
          {
              TransactionType = TransactionType.T,
              AccountNumber = 1042,
              DestinationAccountNumber = 1043,
              Amount = 300,
              Comment = "party",
              ModifyDate = DateTime.ParseExact("17/01/2020 09:00:00 PM", format, null)
          },

        new TransactionAPI
        {
            TransactionType = TransactionType.S,
            AccountNumber = 1042,

            Amount = 0.2m,
            Comment = "Servic Charge for transfer",
            ModifyDate = DateTime.ParseExact("17/01/2020 09:00:00 PM", format, null)
        },

          new TransactionAPI
          {
              TransactionType = TransactionType.T,
              AccountNumber = 1042,
              DestinationAccountNumber = 1043,
              Amount = 333,
              Comment = "party",
              ModifyDate = DateTime.ParseExact("30/01/2020 08:00:00 PM", format, null)
          },

        new TransactionAPI
        {
            TransactionType = TransactionType.S,
            AccountNumber = 1042,

            Amount = 0.2m,
            Comment = "Servic Charge for transfer",
            ModifyDate = DateTime.ParseExact("30/01/2020 08:00:00 PM", format, null)
        },
        // end of one month

        // transaction for 1040 in the past 4 month for withdraw
        new TransactionAPI
        {
            TransactionType = TransactionType.W,
            AccountNumber = 1042,

            Amount = 100,
            Comment = "For living",
            ModifyDate = DateTime.ParseExact("11/01/2020 08:00:00 PM", format, null)
        },

        new TransactionAPI
        {
            TransactionType = TransactionType.S,
            AccountNumber = 1042,

            Amount = 0.1m,
            Comment = "Servic Charge for withdraw",
            ModifyDate = DateTime.ParseExact("11/01/2020 08:00:00 PM", format, null)
        },

          new TransactionAPI
          {
              TransactionType = TransactionType.W,
              AccountNumber = 1042,

              Amount = 500,
              Comment = "For living",
              ModifyDate = DateTime.ParseExact("16/01/2020 08:00:00 PM", format, null)
          },

        new TransactionAPI
        {
            TransactionType = TransactionType.S,
            AccountNumber = 1042,

            Amount = 0.1m,
            Comment = "Servic Charge for withdraw",
            ModifyDate = DateTime.ParseExact("16/01/2020 09:00:00 PM", format, null)
        },

          new TransactionAPI
          {
              TransactionType = TransactionType.W,
              AccountNumber = 1042,

              Amount = 100,
              Comment = "Buy gift to Jin",
              ModifyDate = DateTime.ParseExact("29/01/2020 08:00:00 PM", format, null)
          },

        new TransactionAPI
        {
            TransactionType = TransactionType.S,
            AccountNumber = 1042,

            Amount = 0.1m,
            Comment = "Servic Charge for transfer",
            ModifyDate = DateTime.ParseExact("29/01/2020 08:00:00 PM", format, null)
        },

        // extra fees for bills
        new TransactionAPI
        {
            TransactionType = TransactionType.B,
            AccountNumber = 1042,

            Amount = 188,
            Comment = "Bills",
            ModifyDate = DateTime.ParseExact("02/02/2020 08:00:00 PM", format, null)
        },
        // end of one month

        // line from lovely Jin

        // transaction for 1042 in the past 4 month for Deposit
        new TransactionAPI
        {
            TransactionType = TransactionType.D,
            AccountNumber = 1042,

            Amount = 200,
            Comment = "Repair computer",
            ModifyDate = DateTime.ParseExact("15/01/2020 08:00:00 PM", format, null)
        },



          new TransactionAPI
          {
              TransactionType = TransactionType.D,
              AccountNumber = 1042,

              Amount = 300,
              Comment = "design software",
              ModifyDate = DateTime.ParseExact("26/01/2020 08:00:00 PM", format, null)
          },



          new TransactionAPI
          {
              TransactionType = TransactionType.D,
              AccountNumber = 1042,

              Amount = 50,
              Comment = "rental fees from tenants",
              ModifyDate = DateTime.ParseExact("29/01/2020 08:00:00 PM", format, null)
          },

            new TransactionAPI
            {
                TransactionType = TransactionType.D,
                AccountNumber = 1042,

                Amount = 20,
                Comment = "stock incomes",
                ModifyDate = DateTime.ParseExact("06/02/2020 08:00:00 PM", format, null)
            },

            // line from cute Jin
            // account for 1043
             new TransactionAPI
             {
                 TransactionType = TransactionType.T,
                 AccountNumber = 1043,
                 DestinationAccountNumber = 1041,
                 Amount = 500,
                 Comment = "party",
                 ModifyDate = DateTime.ParseExact("10/01/2020 08:00:00 PM", format, null)
             },

        new TransactionAPI
        {
            TransactionType = TransactionType.S,
            AccountNumber = 1043,

            Amount = 0.2m,
            Comment = "Servic Charge for transfer",
            ModifyDate = DateTime.ParseExact("10/01/2020 08:00:00 PM", format, null)
        },

          new TransactionAPI
          {
              TransactionType = TransactionType.T,
              AccountNumber = 1043,
              DestinationAccountNumber = 1041,
              Amount = 500,
              Comment = "party",
              ModifyDate = DateTime.ParseExact("19/01/2020 09:00:00 PM", format, null)
          },

        new TransactionAPI
        {
            TransactionType = TransactionType.S,
            AccountNumber = 1043,

            Amount = 0.2m,
            Comment = "Servic Charge for transfer",
            ModifyDate = DateTime.ParseExact("19/01/2020 09:00:00 PM", format, null)
        },

          new TransactionAPI
          {
              TransactionType = TransactionType.T,
              AccountNumber = 1043,
              DestinationAccountNumber = 1041,
              Amount = 333,
              Comment = "party",
              ModifyDate = DateTime.ParseExact("30/01/2020 08:00:00 PM", format, null)
          },

        new TransactionAPI
        {
            TransactionType = TransactionType.S,
            AccountNumber = 1043,

            Amount = 0.2m,
            Comment = "Servic Charge for transfer",
            ModifyDate = DateTime.ParseExact("30/01/2020 08:00:00 PM", format, null)
        },
        // end of one month

        // transaction for 1040 in the past 4 weeks for withdraw
        new TransactionAPI
        {
            TransactionType = TransactionType.W,
            AccountNumber = 1043,

            Amount = 900,
            Comment = "For living",
            ModifyDate = DateTime.ParseExact("11/01/2020 08:00:00 PM", format, null)
        },

        new TransactionAPI
        {
            TransactionType = TransactionType.S,
            AccountNumber = 1043,

            Amount = 0.1m,
            Comment = "Servic Charge for withdraw",
            ModifyDate = DateTime.ParseExact("11/01/2020 08:00:00 PM", format, null)
        },

          new TransactionAPI
          {
              TransactionType = TransactionType.W,
              AccountNumber = 1043,

              Amount = 700,
              Comment = "For living",
              ModifyDate = DateTime.ParseExact("16/01/2020 08:00:00 PM", format, null)
          },

        new TransactionAPI
        {
            TransactionType = TransactionType.S,
            AccountNumber = 1043,

            Amount = 0.1m,
            Comment = "Servic Charge for withdraw",
            ModifyDate = DateTime.ParseExact("16/01/2020 09:00:00 PM", format, null)
        },

          new TransactionAPI
          {
              TransactionType = TransactionType.W,
              AccountNumber = 1043,

              Amount = 300,
              Comment = "Buy gift to Jin",
              ModifyDate = DateTime.ParseExact("29/01/2020 08:00:00 PM", format, null)
          },

        new TransactionAPI
        {
            TransactionType = TransactionType.S,
            AccountNumber = 1043,

            Amount = 0.1m,
            Comment = "Servic Charge for transfer",
            ModifyDate = DateTime.ParseExact("29/01/2020 08:00:00 PM", format, null)
        },

        // extra fees for bills
        new TransactionAPI
        {
            TransactionType = TransactionType.B,
            AccountNumber = 1043,

            Amount = 555,
            Comment = "Bills",
            ModifyDate = DateTime.ParseExact("02/02/2020 08:00:00 PM", format, null)
        },
        // end of one month

        // line from lovely Jin

        // transaction for 1042 in the past 4 month for Deposit
        new TransactionAPI
        {
            TransactionType = TransactionType.D,
            AccountNumber = 1043,

            Amount = 100,
            Comment = "Repair computer",
            ModifyDate = DateTime.ParseExact("15/01/2020 08:00:00 PM", format, null)
        },



          new TransactionAPI
          {
              TransactionType = TransactionType.D,
              AccountNumber = 1043,

              Amount = 50,
              Comment = "design software",
              ModifyDate = DateTime.ParseExact("26/01/2020 08:00:00 PM", format, null)
          },



          new TransactionAPI
          {
              TransactionType = TransactionType.D,
              AccountNumber = 1043,

              Amount = 50,
              Comment = "rental fees from tenants",
              ModifyDate = DateTime.ParseExact("29/01/2020 08:00:00 PM", format, null)
          },

            new TransactionAPI
            {
                TransactionType = TransactionType.D,
                AccountNumber = 1043,

                Amount = 20,
                Comment = "stock incomes",
                ModifyDate = DateTime.ParseExact("06/02/2020 08:00:00 PM", format, null)
            }













        // initial account transactions

        //new TransactionAPI
        //{
        //    TransactionType = TransactionType.D,
        //    AccountNumber = 1013,
        //    Amount = 500,
        //    Comment = openingBalance,
        //    ModifyDate = DateTime.ParseExact("19/12/2019 08:30:00 PM", format, null)
        //},
        //new TransactionAPI
        //{
        //    TransactionType = TransactionType.D,
        //    AccountNumber = 1014,
        //    Amount = 500.95m,
        //    Comment = openingBalance,
        //    ModifyDate = DateTime.ParseExact("19/12/2019 09:00:00 PM", format, null)
        //},
        //new TransactionAPI
        //{
        //    TransactionType = TransactionType.D,
        //    AccountNumber = 1015,
        //    Amount = 1250.50m,
        //    Comment = openingBalance,
        //    ModifyDate = DateTime.ParseExact("19/12/2019 10:00:00 PM", format, null)
        );



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