using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Documentz.Repositories;
using Documentz.Models;
using Documentz.Services;
using Microsoft.AspNetCore.Authorization;

namespace Documentz.Controllers
{
    //[Route("api/documents/{documentId?}/attachments")]
    [Route("api/attachments")]
    [Authorize]
    public class AttachmentsController : Controller
    {
        private readonly IStoredItemService storedItemService;
        public AttachmentsController(IStoredItemService service) => storedItemService = service;

        /// <summary>
        /// Returns all attachments for selected document
        /// </summary>
        /// <param name="documentId">Document Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<dynamic>> GetAsync(string documentId)
        {
            return await storedItemService.GetAttachmentsAsync(documentId);
        }

        // GET api/values/5
//        [HttpGet("{id}")]
//        public async Task<JsonResult> Get(string id)
//        {
//            var res = await DocumentDbRepository<StoredItem>.GetAttachment(id);
//            var memory = new MemoryStream();
//            await res.CopyToAsync(memory);
//            var reader = new StreamReader(memory);
//            return Json(await reader.ReadToEndAsync());
//        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] string documentId, [FromForm] IEnumerable<IFormFile> files)
        {
            var list = files.ToList();
            if (!list.Any())
            {
                return BadRequest("Empty file list");
            }
            foreach(var file in list)
            {
                var memory = new MemoryStream();
                await file.CopyToAsync(memory);
                memory.Seek(0, SeekOrigin.Begin);
                await storedItemService.AddAttachmentAsync(documentId, memory);
            }
            return Ok();
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
