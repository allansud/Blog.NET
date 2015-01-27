using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Usuario
    {

        public Usuario() 
        {
            Permissoes = new HashSet<Permissao>();
            Comentarios = new List<Comentario>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int codigo { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Nome é um campo requirido!")]
        [StringLength(80)]
        [MinLength(3, ErrorMessage = "No minimo 3 caracteres!")]
        [MaxLength(80, ErrorMessage = "Máximo suportado para esse campo são 80 caracteres!")]
        public String nome { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Campo e-mail é requerido!")]
        [StringLength(100)]
        [MinLength(10, ErrorMessage = "No minimo 3 caracteres!")]
        [MaxLength(100, ErrorMessage = "Máximo suportado para esse campo são 80 caracteres!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Formato de e-mail inválido!")]
        public String email { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo senha é requerido!")]
        [StringLength(100)]
        [MinLength(8, ErrorMessage = "A senha deve ter no minimo 8 caracteres!")]
        [MaxLength(100, ErrorMessage = "Senha deve ter no máximo 100 caracteres!")]
        public String senha { get; set; }

        [NotMapped]
        [Display(Name = "Confirmar Senha")]
        [Required(ErrorMessage = "Campo confirmar senha é requirido!")]
        [MinLength(8, ErrorMessage = "A senha deve ter no minimo 8 caracteres!")]
        [MaxLength(100, ErrorMessage = "Senha deve ter no máximo 100 caracteres!")]
        [Compare("senha", ErrorMessage = "As senhas digitadas são diferentes!")]
        [DataType(DataType.Password)]
        public String confirmarSenha { get; set; }

        [Display(Name = "Cidade")]
        [StringLength(50, ErrorMessage = "Campo cidade só pode ter até 50 caracteres!")]
        [MinLength(3, ErrorMessage = "Campo pode ter no minimo 3 caracteres!")]
        [MaxLength(50, ErrorMessage = "No máximo 50 caracteres!")]
        public String cidade { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(14)]
        [MinLength(8, ErrorMessage = "Campo não pode ter menos de 8 caracteres!")]
        [MaxLength(14, ErrorMessage = "No máximo 14 caracteres!")]
        public String telefone { get; set; }

        [ScaffoldColumn(false)]
        public DateTime data_cadastro { get; set; }

        [Display(Name = "Perfil")]
        public virtual ICollection<Permissao> Permissoes { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }
    }
}