using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.DataManager;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly AccountManager _repo;

        public AccountsController(AccountManager repo)
        {
            _repo = repo;
        }

        // GET: api/movies
        [HttpGet]
        public IEnumerable<AccountAPI> Get()
        {
            return _repo.GetAll();
        }

        // GET api/movies/1
        [HttpGet("{id}")]
        public AccountAPI Get(int id)
        {
            return _repo.Get(id);
        }

        // POST api/movies
        [HttpPost]
        public void Post([FromBody] AccountAPI account)
        {
            _repo.Add(account);
        }

        // PUT api/movies
        [HttpPut]
        public void Put([FromBody] AccountAPI account)
        {
            _repo.Update(account.AccountNumber, account);
        }

        // DELETE api/movies/1
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }

    }
}
