using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Permissao
    {

        public Permissao() 
        {
            Usuarios = new HashSet<Usuario>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int codigo { get; set; }

        public DateTime data_cadastro { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Role")]
        public String role { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}