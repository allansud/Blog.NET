using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Quotes
    {
        [Key]
        public int codigo { get; set; }

        [StringLength(1500)]
        [Required]
        [Display(Name = "Quote")]
        public String quote { get; set; }
    }
}