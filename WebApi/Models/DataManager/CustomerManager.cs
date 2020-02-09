using WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Repository;

namespace WebApi.Models.DataManager
{// connect to the database directly to manage customers' info
    public class CustomerManager : IDataRepository<CustomerAPI, int>
    {
        private readonly NwbaDbContext _context;

        public CustomerManager(NwbaDbContext context)
        {
            _context = context;
        }

        // add customer
        public int Add(CustomerAPI item)
        {
            _context.Customers.Add(item);
            _context.SaveChanges();
            return item.CustomerID;
        }

        // delete customer based on specific id
        public int Delete(int id)
        {
            _context.Customers.Remove(_context.Customers.Find(id));
            _context.SaveChanges();
            return id;
        }

        // get customer based on specific id
        public CustomerAPI Get(int id)
        {
            return _context.Customers.Find(id);
        }

        // obtain all the customers
        public IEnumerable<CustomerAPI> GetAll()
        {
            return _context.Customers.ToList();
        }

        // update customer info for specific id
        public int Update(int id, CustomerAPI item)
        {
            _context.Update(item);
            _context.SaveChanges();

            return id;
        }
    }
}
