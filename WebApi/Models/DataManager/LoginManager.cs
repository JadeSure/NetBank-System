using WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Repository;

namespace WebApi.Models.DataManager
{
    public class LoginManager : IDataRepository<LoginAPI, int>
    {
        private readonly NwbaDbContext _context;

        public LoginManager(NwbaDbContext context)
        {
            _context = context;
        }

        public int Add(LoginAPI item)
        {
            _context.Logins.Add(item);
            _context.SaveChanges();
            return item.CustomerID;
        }

        public int Delete(int id)
        {
            _context.Logins.Remove(_context.Logins.Find(id));
            _context.SaveChanges();
            return id;
        }

        public LoginAPI Get(int id)
        {
            return _context.Logins.Find(id);
        }

        public IEnumerable<LoginAPI> GetAll()
        {
            return _context.Logins.ToList();
        }

        public int Update(int id, LoginAPI item)
        {
            _context.Update(item);
            _context.SaveChanges();
            return id;
        }
    }
}
