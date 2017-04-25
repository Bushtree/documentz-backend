using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace documentz_backend.Models
{
    public class Tag
    {
        [Key]
        [Required]
        public string Value { get; set; }
    }
}
