using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class ReuniaoFamiliar
    {
        [Key]
        public int Id { get; set; }
        public String Titulo { get; set; }
        public String Palestrante { get; set; }
        public String Data { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}