using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using documentz_backend.Data;
using documentz_backend.Errors;
using documentz_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace documentz_backend.Controllers
{
    [Route("api/[controller]")]
    public class DocumentsController : Controller
    {
        private readonly IDbContext db;
        private readonly ILogger logger;

        public DocumentsController(IDbContext db, ILogger<DocumentsController> logger)
        {
            this.db = db;
            this.logger = logger;
        }
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Document>> Get()
        {
            var enumerable = await db.GetDocumentsAsync();
            return enumerable;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await db.GetDocumentAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
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
                logger.LogError(LoggingEvents.ItemNotFound, ex, $"Document {document.Id} not found.");
                return NotFound(document);
            }

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await db.DeleteDocumentAsync(id);
            }
            catch (ItemNotFoundException ex)
            {
                logger.LogError(LoggingEvents.ItemNotFound, ex, $"Document {id} not found.");
                return NotFound(id);
            }

            return Ok();
        }
    }
}
