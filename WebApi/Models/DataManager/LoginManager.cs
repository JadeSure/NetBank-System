using WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Repository;

namespace WebApi.Models.DataManager
{
    // connect to the database directly to manage login
    public class LoginManager : IDataRepository<LoginAPI, int>
    {
        private readonly NwbaDbContext _context;

        public LoginManager(NwbaDbContext context)
        {
            _context = context;
        }

        // add login info
        public int Add(LoginAPI item)
        {
            _context.Logins.Add(item);
            _context.SaveChanges();
            return item.CustomerID;
        }

        // delete login info based on specific id
        public int Delete(int id)
        {
            _context.Logins.Remove(_context.Logins.Find(id));
            _context.SaveChanges();
            return id;
        }

        // get login info based on specific id
        public LoginAPI Get(int id)
        {
            return _context.Logins.Find(id);
        }

        // get all login informations
        public IEnumerable<LoginAPI> GetAll()
        {
            return _context.Logins.ToList();
        }

        // update login informations based on specific id
        public int Update(int id, LoginAPI item)
        {
            _context.Update(item);
            _context.SaveChanges();
            return id;
        }
    }
}
