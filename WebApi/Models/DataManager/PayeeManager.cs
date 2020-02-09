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

        // add payee info
        public int Add(PayeeAPI item)
        {
            _context.Payees.Add(item);
            _context.SaveChanges();
            return item.PayeeID;
        }

        // delete payee infomation based on sepcific id
        public int Delete(int id)
        {
            _context.Payees.Remove(_context.Payees.Find(id));
            _context.SaveChanges();
            return id;
        }

        // get payee infomation based on sepcific id
        public PayeeAPI Get(int id)
        {
            return _context.Payees.Find(id);
        }

        // get all payees
        public IEnumerable<PayeeAPI> GetAll()
        {
            return _context.Payees.ToList();
        }

        // update payee infomation based on specific id
        public int Update(int id, PayeeAPI item)
        {
            _context.Update(item);
            _context.SaveChanges();
            return id;
        }
    }
}
