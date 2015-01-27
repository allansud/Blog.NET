using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Categoria
    {
        public Categoria() 
        {
            Posts = new List<Post>();
        }

        [Key]
        public int id { get; set; }

        [Display(Name = "Nome Categoria")]
        [StringLength(30, ErrorMessage = "No máximo até 30 caracteres!")]
        public String nome { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}