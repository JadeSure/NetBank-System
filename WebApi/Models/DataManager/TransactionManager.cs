using WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Repository;

namespace WebApi.Models.DataManager
{
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

        public int Update(int id, TransactionAPI item)
        {
            _context.Update(item);
            _context.SaveChanges();
            return id;
        }
    }
}
