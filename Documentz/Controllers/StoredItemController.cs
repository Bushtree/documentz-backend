using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Documentz.Repositories;
using Documentz.Models;
using Documentz.Services;

namespace Documentz.Controllers
{
    [Produces("application/json")]
    [Route("api/stored-item")]
    public class StoredItemController : Controller
    {
        private IStoredItemService StoredItemService { get; }

        public StoredItemController(IStoredItemService storedItemService)
        {
            StoredItemService = storedItemService;
        }

        // GET: api/StoredItem
        [HttpGet]
        [ActionName("Get")]
        public async Task<IEnumerable<IStoredItem>> GetAsync()
        {
            return await StoredItemService.GetItemsAsync(a => true);
        }

        // GET: api/StoredItem/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/StoredItem
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/StoredItem/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
