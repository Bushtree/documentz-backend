using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using documentz_backend.Data;
using documentz_backend.Errors;
using documentz_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace documentz_backend.Controllers
{
    [Route("api/[controller]")]
    public class DocumentsController : Controller
    {
        private readonly IDbContext db;

        public DocumentsController(IDbContext db)
        {
            this.db = db;
        }
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Document>> Get()
        {
            return await db.GetDocumentsAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Document> Get(int id)
        {
            return await db.GetDocumentAsync(id);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Document document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newId = await db.AddDocumentAsync(document);
            return Created($"/api/documents/{newId}", document);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]Document document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await db.UpdateDocumentAsync(document);
            }
            catch (ItemNotFoundException ex)
            {
                return NotFound(document);
            }

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await db.DeleteDocumentAsync(id);
            }
            catch (ItemNotFoundException ex)
            {
                return NotFound(id);
            }

            return Ok();
        }
    }
}
