using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Documentz.Repositories;
using Documentz.Models;

namespace Documentz.Controllers
{
    [Route("api/[controller]")]
    public class AttachmentsController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(string id)
        {
            var res = await DocumentDbRepository<StoredItem>.GetAttachment(id);
            var memory = new MemoryStream();
            await res.CopyToAsync(memory);
            var reader = new StreamReader(memory);
            return Json(await reader.ReadToEndAsync());
        }

        // POST api/values
        [HttpPost]
        public async void Post(string documentId, IEnumerable<IFormFile> files)
        {
            var path = Path.GetTempFileName();

            foreach(var file in files)
            {
                var memory = new MemoryStream();
                await file.CopyToAsync(memory);
                await DocumentDbRepository<StoredItem>.AddAttachment(documentId, memory);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
