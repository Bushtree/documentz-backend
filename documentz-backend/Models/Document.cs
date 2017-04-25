using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace documentz_backend.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        [Required]
        public string Category { get; set; }
        public List<Tag> Tags { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
