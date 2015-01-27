using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class DownloadFileInfo
    {
        [Key]
        [Display(Name = "Código")]
        public int FileID { get; set; }

        [Display(Name = "Nome")]
        public string FileName { get; set; }

        [Display(Name = "Caminho")]
        public string FilePath { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        [MinLength(3, ErrorMessageResourceType = typeof(App_GlobalResources.Resource), ErrorMessageResourceName = "File_error")]
        public string descricao { get; set; }
    }
}