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
    public class TransactionsController : Controller
    {
        private readonly TransactionManager _repo;

        public TransactionsController(TransactionManager repo)
        {
            _repo = repo;
        }

        // GET: api/Transactions
        [HttpGet]
        public IEnumerable<TransactionAPI> Get()
        {
            return _repo.GetAll();
        }

        // GET api/Transactions/1
        [HttpGet("{id}")]
        public TransactionAPI Get(int id)
        {
            return _repo.Get(id);
        }

        // POST: api/Transactions/search
        [HttpPost("Search")]
        public IEnumerable<TransactionAPI> Search([FromBody] TransactionRequest transaction)
        {
            if (!ModelState.IsValid)
                return null;

            var transactions = _repo.GetAllByRequest(transaction);

            return transactions;
        }

        // POST api/Transactions
        [HttpPost]
        public void Post([FromBody] TransactionAPI transaction)
        {
            _repo.Add(transaction);
        }


        // PUT api/Transactions
        [HttpPut]
        public void Put([FromBody] TransactionAPI transaction)
        {
            _repo.Update(transaction.TransactionID, transaction);
        }

        // DELETE api/Transactions/1
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}
