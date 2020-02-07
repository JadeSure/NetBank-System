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
    public class LoginsController : Controller
    {
        private readonly LoginManager _repo;

        public LoginsController(LoginManager repo)
        {
            _repo = repo;
        }

        // GET: api/Logins
        [HttpGet]
        public IEnumerable<LoginAPI> Get()
        {
            return _repo.GetAll();
        }

        // GET api/Logins/1
        [HttpGet("{id}")]
        public LoginAPI Get(int id)
        {
            return _repo.Get(id);
        }

        // POST api/Logins
        [HttpPost]
        public void Post([FromBody] LoginAPI login)
        {
            _repo.Add(login);
        }

        // PUT api/Customers
        [HttpPut]
        public void Put([FromBody] LoginAPI login)
        {
            _repo.Update(login.CustomerID, login);
        }

        // DELETE api/Customers/1
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }

    }
}
