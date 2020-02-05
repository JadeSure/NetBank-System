using WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Repository;

namespace WebApi.Models.DataManager
{
    public class CustomerManager : IDataRepository<CustomerAPI, int>
    {
        private readonly NwbaDbContext _context;

        public CustomerManager(NwbaDbContext context)
        {
            _context = context;
        }

        public int Add(CustomerAPI item)
        {
            _context.Customers.Add(item);
            _context.SaveChanges();
            return item.CustomerID;
        }

        public int Delete(int id)
        {
            _context.Customers.Remove(_context.Customers.Find(id));
            _context.SaveChanges();
            return id;
        }

        public CustomerAPI Get(int id)
        {
            return _context.Customers.Find(id);
        }

        public IEnumerable<CustomerAPI> GetAll()
        {
            return _context.Customers.ToList();
        }

        public int Update(int id, CustomerAPI item)
        {
            _context.Update(item);
            _context.SaveChanges();

            return id;
        }
    }
}
