using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using WebApi.Models.DataManager;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillPaysController : Controller
    {
        private readonly BillPayManager _repo;

        public BillPaysController(BillPayManager repo)
        {
            _repo = repo;
        }

        // GET: api/BillPays
        [HttpGet]
        public IEnumerable<BillPayAPI> Get()
        {
            return _repo.GetAll();
        }

        // GET api/BillPay/1
        [HttpGet("{id}")]
        public BillPayAPI Get(int id)
        {
            return _repo.Get(id);
        }

        // POST api/BillPay
        [HttpPost]
        public void Post([FromBody] BillPayAPI billPay)
        {
            _repo.Add(billPay);
        }

        // PUT api/billPay
        [HttpPut]
        public void Put([FromBody] BillPayAPI billPay)
        {
            _repo.Update(billPay.BillPayID, billPay);
        }

        // DELETE api/billPay/1
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }

    }
}
