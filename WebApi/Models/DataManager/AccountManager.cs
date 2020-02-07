using WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Repository;

namespace WebApi.Models.DataManager
{
    public class AccountManager : IDataRepository<AccountAPI, int>
    {
        private readonly NwbaDbContext _context;

        public AccountManager(NwbaDbContext context)
        {
            _context = context;
        }

        public int Add(AccountAPI item)
        {
            _context.Accounts.Add(item);
            _context.SaveChanges();
            return item.AccountNumber;
        }

        public int Delete(int id)
        {
            _context.Accounts.Remove(_context.Accounts.Find(id));
            _context.SaveChanges();
            return id;
        }

        public AccountAPI Get(int id)
        {
            return _context.Accounts.Find(id);
        }

        public IEnumerable<AccountAPI> GetAll()
        {
            return _context.Accounts.ToList();
        }

        public int Update(int id, AccountAPI item)
        {
            _context.Update(item);
            _context.SaveChanges();

            return id;
        }
    }
}
