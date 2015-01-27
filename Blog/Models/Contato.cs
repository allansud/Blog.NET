using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Contato
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Nome Completo")]
        [Required]
        [StringLength(40)]
        [MinLength(3, ErrorMessage = "Nome deve ter no mínimo 3 caracteres!")]
        [MaxLength(40, ErrorMessage = "No máximo 40 caracteres!")]
        public String nome { get; set; }

        [Display(Name = "E-mail")]
        [Required]
        [StringLength(50)]
        [MinLength(5, ErrorMessage = "Campo e-mail pode ter no mínimo 5 caracteres!")]
        [MaxLength(50, ErrorMessage = "Campo e-mail pode ter no máximo 50 caracteres!")]
        public String email { get; set; }

        [Display(Name = "Telefone")]
        [MinLength(8, ErrorMessage = "Campo telefone nao pode ter menos de 8 caracteres!")]
        [MaxLength(14, ErrorMessage = "Campo telefone não pode ter mais de 14 digitos!")]
        [StringLength(14)]
        public String telefone { get; set; }

        [Display(Name = "Mensagem")]
        [Required]
        [StringLength(1000)]
        [MaxLength(1000, ErrorMessage = "No máximo 1000 caracteres!")]
        [DataType(DataType.MultilineText)]
        public String mensagem { get; set; }

        [Display(Name = "Assunto")]
        [Required]
        [MinLength(3, ErrorMessage = "Campo assunto deve ter no mínimo 3 caracteres!")]
        [MaxLength(30, ErrorMessage = "Campo assunto deve ter no mínimo 30 caracteres!")]
        public String assunto { get; set; }

        [Display(Name = "Cidade")]
        [MinLength(3, ErrorMessage = "Campo assunto deve ter no mínimo 3 caracteres!")]
        [MaxLength(30, ErrorMessage = "Campo assunto deve ter no mínimo 30 caracteres!")]
        public String cidade { get; set; }
    }
}