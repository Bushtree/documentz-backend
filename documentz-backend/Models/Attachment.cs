using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace documentz_backend.Models
{
    public class Attachment
    {
        public string Name { get; set; }
        [Key]
        [Required]
        public string Path { get; set; }
    }
}
