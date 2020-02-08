using WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Repository;

namespace WebApi.Models.DataManager
{

    public class TransactionRequest
    {
        public int? CustomerID { get; set; }
        public int? AccountNumber { get; set; }
        public TransactionType? TransactionType { get; set; }
        public int? DestinationAccountNumber { get; set; }
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

        public int Add(TransactionAPI item)
        {
            _context.Transactions.Add(item);
            _context.SaveChanges();
            return item.TransactionID;
        }

        public int Delete(int id)
        {
            _context.Transactions.Remove(_context.Transactions.Find(id));
            _context.SaveChanges();
            return id;
        }

        public TransactionAPI Get(int id)
        {
            return _context.Transactions.Find(id);
        }

        public IEnumerable<TransactionAPI> GetAll()
        {
            return _context.Transactions.ToList();
        }

        public IEnumerable<TransactionAPI> GetAllByAccountNumber(int accountNumber)
        {
            return _context.Transactions.Where(x => x.AccountNumber == accountNumber).ToList();
        }

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

            if (searchRequest.CustomerID != null)
            {
                result = result.Where(x => x.Account.CustomerID == searchRequest.CustomerID);
            }

            if (searchRequest.AccountNumber != null)
            {
                result = result.Where(x => x.AccountNumber == searchRequest.AccountNumber);
            }

            if (searchRequest.TransactionType != null)
            {
                result = result.Where(x => x.TransactionType == searchRequest.TransactionType);
            }

            if (searchRequest.DestinationAccountNumber != null)
            {
                result = result.Where(x => x.DestinationAccountNumber == searchRequest.DestinationAccountNumber);
            }

            if (searchRequest.MinAmount != null)
            {
                result = result.Where(x => x.Amount >= searchRequest.MinAmount);
            }

            if (searchRequest.MaxAmount != null)
            {
                result = result.Where(x => x.Amount <= searchRequest.MaxAmount);
            }

            if (searchRequest.StartTime != null)
            {
                result = result.Where(x => DateTime.Compare(x.ModifyDate, (DateTime)searchRequest.StartTime) >= 0);
            }

            if (searchRequest.EndTime != null)
            {
                result = result.Where(x => DateTime.Compare(x.ModifyDate, (DateTime)searchRequest.EndTime) <= 0);
            }

            if (searchRequest.Comment != null)
            {
                result = result.Where(x => x.Comment.Contains(searchRequest.Comment));
            }

            return result;
        }

    public int Update(int id, TransactionAPI item)
    {
        _context.Update(item);
        _context.SaveChanges();
        return id;
    }


}
}
