using WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Repository;

namespace WebApi.Models.DataManager
{
    // connect to the database directly to manage bill pya
    public class BillPayManager : IDataRepository<BillPayAPI, int>
    {
        private readonly NwbaDbContext _context;

        // connect to database
        public BillPayManager(NwbaDbContext context)
        {
            _context = context;
        }

        // add bill pay
        public int Add(BillPayAPI item)
        {
            _context.BillPays.Add(item);
            _context.SaveChanges();
            return item.BillPayID;
        }

        // delete bill pay by specific id
        public int Delete(int id)
        {
            _context.BillPays.Remove(_context.BillPays.Find(id));
            _context.SaveChanges();
            return id;
        }

        // get bill pay by specific id
        public BillPayAPI Get(int id)
        {
            return _context.BillPays.Find(id);
        }

        // get all bill pays
        public IEnumerable<BillPayAPI> GetAll()
        {
            return _context.BillPays.ToList();
        }

        // update bill info by specific item
        public int Update(int id, BillPayAPI item)
        {
            _context.Update(item);
            _context.SaveChanges();

            return id;
        }
    }
}
