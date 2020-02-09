using WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Repository;

namespace WebApi.Models.DataManager
{
    // connect to the database directly to manage accounts
    public class AccountManager : IDataRepository<AccountAPI, int>
    {
        private readonly NwbaDbContext _context;

        public AccountManager(NwbaDbContext context)
        {
            _context = context;
        }

        // add account
        public int Add(AccountAPI item)
        {
            _context.Accounts.Add(item);
            _context.SaveChanges();
            return item.AccountNumber;
        }

        // delete account
        public int Delete(int id)
        {
            _context.Accounts.Remove(_context.Accounts.Find(id));
            _context.SaveChanges();
            return id;
        }

        // get account based on specific id
        public AccountAPI Get(int id)
        {
            return _context.Accounts.Find(id);
        }

        // get all accounts
        public IEnumerable<AccountAPI> GetAll()
        {
            return _context.Accounts.ToList();
        }

        // update account value based on specific id
        public int Update(int id, AccountAPI item)
        {
            _context.Update(item);
            _context.SaveChanges();

            return id;
        }
    }
}
