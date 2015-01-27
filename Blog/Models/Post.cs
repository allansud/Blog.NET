using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Post
    {
        public Post() 
        {
            Comentarios = new List<Comentario>();    
        }

        [Key]
        public int Id { get; set; }

        public DateTime Data_cadastro { get; set; }

        public bool Aprovado { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "Título é um campo necessário!")]
        [StringLength(30, ErrorMessage = "No máximo até 30 caracteres!")]
        public String titulo { get; set; }

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "Campo Autor é obrigatório!")]
        [StringLength(50)]
        [MinLength(3, ErrorMessage = "Campo Autor deve ter no mínimo 3 caracteres!")]
        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres para o campo Autor!")]
        public String Autor { get; set; }

        [Display(Name = "Sobre o Autor")]
        [StringLength(150)]
        [MinLength(3, ErrorMessage = "Campo Autor deve ter no mínimo 10 caracteres!")]
        [MaxLength(150, ErrorMessage = "Máximo 150 caracteres para o campo Autor!")]
        public String SobreAutor { get; set; }

        [Display(Name = "Foto Autor")]
        [StringLength(80)]
        [MaxLength(ErrorMessage = "O nome da foto é muito grande!")]
        public String FotoAutor { get; set; }

        [Display(Name = "Figura 1")]
        [StringLength(80)]
        public String Caminho_Foto_1 { get; set; }

        [Display(Name = "Primeira parte")]
        [DataType(DataType.MultilineText)]
        [StringLength(2000)]
        [MinLength(10, ErrorMessage = "Não é permitido ter menos de 10 caracteres!")]
        [MaxLength(2000, ErrorMessage = "Máximo permitido é 500 caracteres!")]
        public String Texto_1 { get; set; }

        [Display(Name = "Figura 2")]
        [StringLength(80)]
        public String Caminho_Foto_2 { get; set; }

        [Display(Name = "Segunda parte")]
        [DataType(DataType.MultilineText)]
        [StringLength(2000)]
        [MinLength(10, ErrorMessage = "Não é permitido ter menos de 10 caracteres!")]
        [MaxLength(2000, ErrorMessage = "Máximo permitido é 500 caracteres!")]
        public String Texto_2 { get; set; }

        [Display(Name = "Figura 3")]
        [StringLength(80)]
        public String Caminho_Foto_3 { get; set; }

        [Display(Name = "Terceira parte")]
        [DataType(DataType.MultilineText)]
        [StringLength(2000)]
        [MinLength(10, ErrorMessage = "Não é permitido ter menos de 10 caracteres!")]
        [MaxLength(2000, ErrorMessage = "Máximo permitido é 500 caracteres!")]
        public String Texto_3 { get; set; }

        [Display(Name = "Figura 4")]
        [StringLength(80)]
        public String Caminho_Foto_4 { get; set; }

        [Display(Name = "Quarte parte")]
        [DataType(DataType.MultilineText)]
        [StringLength(2000)]
        [MinLength(10, ErrorMessage = "Não é permitido ter menos de 10 caracteres!")]
        [MaxLength(2000, ErrorMessage = "Máximo permitido é 500 caracteres!")]
        public String Texto_4 { get; set; }

        [Display(Name = "Figura 5")]
        [StringLength(80)]
        public String Caminho_Foto_5 { get; set; }

        [Display(Name = "Quinta parte")]
        [DataType(DataType.MultilineText)]
        [StringLength(2000)]
        [MinLength(10, ErrorMessage = "Não é permitido ter menos de 10 caracteres!")]
        [MaxLength(2000, ErrorMessage = "Máximo permitido é 500 caracteres!")]
        public String Texto_5 { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}