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
    public class PayeesController : Controller
    {
        private readonly PayeeManager _repo;

        public PayeesController(PayeeManager repo)
        {
            _repo = repo;
        }

        // GET: api/Payees
        [HttpGet]
        public IEnumerable<PayeeAPI> Get()
        {
            return _repo.GetAll();
        }

        // GET api/Payees/based on specific id
        [HttpGet("{id}")]
        public PayeeAPI Get(int id)
        {
            return _repo.Get(id);
        }

        // POST api/Payees
        [HttpPost]
        public void Post([FromBody] PayeeAPI payee)
        {
            _repo.Add(payee);
        }

        // PUT api/Payees
        [HttpPut]
        public void Put([FromBody] PayeeAPI payee)
        {
            _repo.Update(payee.PayeeID, payee);
        }

        // DELETE api/Payees/based on specific id
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }

    }
}
