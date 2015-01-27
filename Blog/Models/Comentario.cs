using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Comentario
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Comentário")]
        [MinLength(10, ErrorMessage = "Comentário não pode ter menos de 10 caracteres!")]
        [MaxLength(500, ErrorMessage = "No máximo 500 caracteres!")]
        [StringLength(500)]
        public String Comentarios { get; set; }

        public bool Aprovado { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Data_cadastro { get; set; }

        public int PostID { get; set; }

        public int UsuarioID { get; set; }

        [ForeignKey("PostID")]
        public virtual Post Post { get; set; }

        [ForeignKey("UsuarioID")]
        public virtual Usuario Usuario { get; set; }
    }
}