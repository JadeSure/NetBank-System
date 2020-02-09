using WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Repository;

namespace WebApi.Models.DataManager
{

    // transaction request model
    public class TransactionRequest
    {
        public int? CustomerID { get; set; }
        public TransactionType? TransactionType { get; set; }
        public int? AccountNumber { get; set; }
        //public int? DestinationAccountNumber { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public string Comment { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    public class TransactionManager : IDataRepository<TransactionAPI, int>
    {
        private readonly NwbaDbContext _context;

        public TransactionManager(NwbaDbContext context)
        {
            _context = context;
        }

        // add new transactions inside
        public int Add(TransactionAPI item)
        {
            _context.Transactions.Add(item);
            _context.SaveChanges();
            return item.TransactionID;
        }

        // delete one transaction based on sepcific id
        public int Delete(int id)
        {
            _context.Transactions.Remove(_context.Transactions.Find(id));
            _context.SaveChanges();
            return id;
        }

        // get one transaction based on specific id
        public TransactionAPI Get(int id)
        {
            return _context.Transactions.Find(id);
        }

        // get all transactions
        public IEnumerable<TransactionAPI> GetAll()
        {
            return _context.Transactions.ToList();
        }

        // get all transactions based on account number
        public IEnumerable<TransactionAPI> GetAllByAccountNumber(int accountNumber)
        {
            return _context.Transactions.Where(x => x.AccountNumber == accountNumber).ToList();
        }

        // get transactions based on different filter conditions
        public IEnumerable<TransactionAPI> GetAllByRequest(TransactionRequest searchRequest)
        {

            //var result = _context.Transactions
            //    .Where(x =>(searchRequest.CustomerID != 0 && x.Account.CustomerID == searchRequest.CustomerID))
            //    .Where(x =>(searchRequest.AccountNumber != 0 && x.AccountNumber == searchRequest.AccountNumber))
            //.Where(x =>(searchRequest.TransactionType != 0 && x.TransactionType == searchRequest.TransactionType))
            //.Where(x =>(searchRequest.DestinationAccountNumber != 0 && x.DestinationAccountNumber == searchRequest.DestinationAccountNumber))
            //.Where(x =>(searchRequest.MinAmount > 0 && x.Amount >= searchRequest.MinAmount))
            //.Where(x =>(searchRequest.MaxAmount > 0 && x.Amount <= searchRequest.MaxAmount))
            //.Where(x =>(searchRequest.StartTime != null && DateTime.Compare(x.ModifyDate, searchRequest.StartTime) >= 0))
            //.Where(x =>(searchRequest.EndTime != null && DateTime.Compare(x.ModifyDate, searchRequest.EndTime) <= 0))
            //.ToList();

            IQueryable<TransactionAPI> result = _context.Transactions;

            // search results which depends on different filter conditions
            if (searchRequest.CustomerID != null)
            {
                result = result.Where(x => x.Account.CustomerID == searchRequest.CustomerID
                                            || x.DestAccount.CustomerID == searchRequest.CustomerID);
            }

            // based on transaction type
            if (searchRequest.TransactionType != null)
            {
                result = result.Where(x => x.TransactionType == searchRequest.TransactionType);
            }

            //if (searchRequest.DestinationAccountNumber != null)
            //{
            //    result = result.Where(x => x.DestinationAccountNumber == searchRequest.DestinationAccountNumber);
            //}

            // based on account number
            if (searchRequest.AccountNumber != null)
            {
                result = result.Where(x => x.AccountNumber == searchRequest.AccountNumber
                                            || x.DestinationAccountNumber == searchRequest.AccountNumber);
            }

            // based on the minimum amount money
            if (searchRequest.MinAmount != null)
            {
                result = result.Where(x => x.Amount >= searchRequest.MinAmount);
            }

            // based on the maximum amount money
            if (searchRequest.MaxAmount != null)
            {
                result = result.Where(x => x.Amount <= searchRequest.MaxAmount);
            }

            // based on the start of time
            if (searchRequest.StartTime != null)
            {
                result = result.Where(x => DateTime.Compare(x.ModifyDate, (DateTime)searchRequest.StartTime) >= 0);
            }

            // based on end of time
            if (searchRequest.EndTime != null)
            {
                result = result.Where(x => DateTime.Compare(x.ModifyDate, (DateTime)searchRequest.EndTime) <= 0);
            }

            //  based on comments
            if (searchRequest.Comment != null)
            {
                result = result.Where(x => x.Comment.Contains(searchRequest.Comment));
            }

            return result;
        }

        // update transaction info
    public int Update(int id, TransactionAPI item)
    {
        _context.Update(item);
        _context.SaveChanges();
        return id;
    }


}
}
