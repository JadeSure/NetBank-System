using System;
using System.Collections.Generic;
using a2_s3736719_s3677615.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace a2_s3736719_s3677615.Models
{


    public enum AccountType
    {
        // Account Type: Checking(C), Saving(S)
        [Display(Name = "Checking account")]
        C = 1,
        [Display(Name = "Saving account")]
        S = 2
    }

    public class Account
    {
        private const decimal WithdrawFee = 0.1m;
        private const decimal TransferFee = 0.2m;
        private const decimal CheckingOpeningBalance = 500;
        private const decimal CheckingMiniBalance = 200;
        private const decimal SavingOpeningBalance = 100;
        private const decimal SavingMiniBalance = 0;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }

        [Required]
        [Display(Name = "Account Type")]
        public AccountType AccountType { get; set; }

        [Required, ForeignKey("Customer")]
        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }


        [Column(TypeName = "money"), DataType(DataType.Currency)]
        [Display(Name = "Current Balance")]
        public decimal Balance { get; set; }

        [Required, DataType(DataType.DateTime)]
        [Display(Name = "Last Modified Date")]
        public DateTime ModifyDate { get; set; }


        //Navigational Property
        public virtual Customer Customer { get; set; }
        public virtual List<BillPay> BillPays { get; set; }
        public virtual List<Transaction> Transactions { get; set; }

        [NotMapped]
        public int ServiceCount
        {
            get
            {
                int count = 0;

                foreach (var t in Transactions)
                {
                    if (t.TransactionType == TransactionType.W
                        || t.TransactionType == TransactionType.T)
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        public Account()
        {
        }


        // deposit money
        public void MakeDeposit(decimal amount, string comment = null)
        {
            CheckAmount(amount);

            var deposit = new Transaction();
            deposit.TransactionType = TransactionType.D;
            deposit.Amount = amount;
            deposit.ModifyDate = DateTime.UtcNow;


            if (comment is null)
            {
                deposit.Comment = "Deposit $" + amount;
            }
            else
            {
                deposit.Comment = comment;
            }

            Transactions.Add(deposit);
            Balance += amount;
        }

        // check the rest of money to validate it
        public void CheckAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }

            if (amount.HasMoreThanTwoDecimalPlaces())
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot have more than 2 decimal places.");
            }
        }

        // check the rest of balance
        public void CheckBalance(decimal amount, TransactionType type)
        {
            CheckAmount(amount);

            // for withdraw account
            if (type == TransactionType.W)
            {
                if (AccountType == AccountType.C)
                {
                    if ((ServiceCount >= 4 && Balance - amount - WithdrawFee < CheckingMiniBalance)
                        || Balance - amount < CheckingMiniBalance)
                    {
                        throw new InvalidOperationException("Not sufficient funds for this withdrawal. The current balance is " + Balance);
                    }
                }

                if (AccountType == AccountType.S)
                {
                    if ((ServiceCount >= 4 && Balance - amount - WithdrawFee < SavingMiniBalance)
                        || Balance - amount < SavingMiniBalance)
                    {
                        throw new InvalidOperationException("Not sufficient funds for this withdrawal. The current balance is " + Balance);
                    }
                }

            }

            // for transfer account
            if (type == TransactionType.T)
            {
                if (AccountType == AccountType.C)
                {
                    if ((ServiceCount >= 4 && Balance - amount - TransferFee < CheckingMiniBalance)
                        || Balance - amount < CheckingMiniBalance)
                    {
                        throw new InvalidOperationException("Not sufficient funds for this transfer. The current balance is " + Balance);
                    }
                }

                if (AccountType == AccountType.S)
                {
                    if ((ServiceCount >= 4 && Balance - amount - TransferFee < SavingMiniBalance)
                        || Balance - amount < SavingMiniBalance)
                    {
                        throw new InvalidOperationException("Not sufficient funds for this transfer. The current balance is " + Balance);
                    }
                }

            }


            // check for bill pay
            if (type == TransactionType.B)
            {
                if (AccountType == AccountType.C)
                {
                    if (Balance - amount < CheckingMiniBalance)
                    {
                        throw new InvalidOperationException("Not sufficient funds for this billpay. The current balance is " + Balance);
                    }
                }

                if (AccountType == AccountType.S)
                {
                    if (Balance - amount < SavingMiniBalance)
                    {
                        throw new InvalidOperationException("Not sufficient funds for this billpay. The current balance is " + Balance);
                    }
                }
            }

        }

        // add bill pay 
        public void MakeBillPay(decimal amount)
        {
            CheckBalance(amount, TransactionType.B);

            // Add new withdraw transaction
            var billPay = new Transaction
            {
                TransactionType = TransactionType.B,
                
                Amount = amount,
                ModifyDate = DateTime.UtcNow,
                Comment = "BillPay $" + amount
            };
            Transactions.Add(billPay);

            // Update balance
            Balance -= amount;


        }

        // implement withdraw method
        public void MakeWithdrawal(decimal amount, string comment = null)
        {
            CheckBalance(amount, TransactionType.W);

            // Add new withdraw transaction
            var withdrawal = new Transaction();
            withdrawal.TransactionType = TransactionType.W;
            withdrawal.Amount = amount;
            withdrawal.ModifyDate = DateTime.UtcNow;

            if (comment is null)
            {
                withdrawal.Comment = "Withdraw $" + amount;
            }
            else
            {
                withdrawal.Comment = comment;
            }

            Transactions.Add(withdrawal);

            // Update balance
            Balance -= amount;

            // Add new service charge transaction
            if (ServiceCount >= 4)
            {
                var serviceCharge = new Transaction
                {
                    TransactionType = TransactionType.S,
                    Amount = WithdrawFee,
                    ModifyDate = DateTime.UtcNow,
                    Comment = "Service charge for withdrawl of $" + amount
                };

                Transactions.Add(serviceCharge);

                Balance -= WithdrawFee;
            }

        }

        // make transfer money
        public void MakeTransfer(decimal amount, Account destinationAccount, string comment = null)
        {
            CheckBalance(amount, TransactionType.T);

            if (AccountNumber.Equals(destinationAccount.AccountNumber))
            {
                throw new InvalidOperationException("Can not transfer to the same account.");
            }

            // Add new transfer transaction
            var transfer = new Transaction();
            transfer.TransactionType = TransactionType.T;
            transfer.Amount = amount;
            transfer.ModifyDate = DateTime.UtcNow;
            transfer.DestinationAccountNumber = destinationAccount.AccountNumber;

            if (comment is null)
            {
                transfer.Comment = "Transfer $" + amount + " to " + destinationAccount.AccountNumber;
            }
            else
            {
                transfer.Comment = comment;
            }

            Transactions.Add(transfer);

            // Update balance
            Balance -= amount;

            // Update DestAccount balance
            destinationAccount.Balance += amount;

            // Add new service charge transaction
            if (ServiceCount >= 4)
            {
                var serviceCharge = new Transaction
                {
                    TransactionType = TransactionType.S,
                    Amount = WithdrawFee,
                    ModifyDate = DateTime.UtcNow,
                    Comment = "Service charge for transfer $" + amount + " to " + destinationAccount.AccountNumber
                };

                Transactions.Add(serviceCharge);

                Balance -= TransferFee;
            }

        }


    }


}