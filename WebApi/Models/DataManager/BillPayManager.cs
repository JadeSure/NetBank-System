using WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Repository;

namespace WebApi.Models.DataManager
{
    public class BillPayManager : IDataRepository<BillPayAPI, int>
    {
        private readonly NwbaDbContext _context;

        public BillPayManager(NwbaDbContext context)
        {
            _context = context;
        }

        public int Add(BillPayAPI item)
        {
            _context.BillPays.Add(item);
            _context.SaveChanges();
            return item.BillPayID;
        }

        public int Delete(int id)
        {
            _context.BillPays.Remove(_context.BillPays.Find(id));
            _context.SaveChanges();
            return id;
        }

        public BillPayAPI Get(int id)
        {
            return _context.BillPays.Find(id);
        }

        public IEnumerable<BillPayAPI> GetAll()
        {
            return _context.BillPays.ToList();
        }

        public int Update(int id, BillPayAPI item)
        {
            _context.Update(item);
            _context.SaveChanges();

            return id;
        }
    }
}
