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
    public class CustomersController : Controller
    {
        private readonly CustomerManager _repo;

        public CustomersController(CustomerManager repo)
        {
            _repo = repo;
        }

        // GET: api/Customers
        [HttpGet]
        public IEnumerable<CustomerAPI> Get()
        {
            return _repo.GetAll();
        }

        // GET api/Customers/ based on specific id
        [HttpGet("{id}")]
        public CustomerAPI Get(int id)
        {
            return _repo.Get(id);
        }

        // POST api/Customers
        [HttpPost]
        public void Post([FromBody] CustomerAPI customer)
        {
            _repo.Add(customer);
        }

        // PUT api/Customers
        [HttpPut]
        public void Put([FromBody] CustomerAPI customer)
        {
            _repo.Update(customer.CustomerID, customer);
        }

        // DELETE api/Customers/ based on specific id
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }

    }
}
