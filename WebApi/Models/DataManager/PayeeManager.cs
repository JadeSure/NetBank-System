using WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Repository;

namespace WebApi.Models.DataManager
{
    public class PayeeManager : IDataRepository<PayeeAPI, int>
    {
        private readonly NwbaDbContext _context;

        public PayeeManager(NwbaDbContext context)
        {
            _context = context;
        }

        public int Add(PayeeAPI item)
        {
            _context.Payees.Add(item);
            _context.SaveChanges();
            return item.PayeeID;
        }

        public int Delete(int id)
        {
            _context.Payees.Remove(_context.Payees.Find(id));
            _context.SaveChanges();
            return id;
        }

        public PayeeAPI Get(int id)
        {
            return _context.Payees.Find(id);
        }

        public IEnumerable<PayeeAPI> GetAll()
        {
            return _context.Payees.ToList();
        }

        public int Update(int id, PayeeAPI item)
        {
            _context.Update(item);
            _context.SaveChanges();
            return id;
        }
    }
}
