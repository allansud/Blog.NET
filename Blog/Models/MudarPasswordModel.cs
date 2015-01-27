using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class MudarPasswordModel
    {
        [Required]
        [StringLength(12)]
        [MinLength(8, ErrorMessageResourceType = typeof(App_GlobalResources.Resource), ErrorMessageResourceName = "Password_min")]
        [MaxLength(12, ErrorMessageResourceType = typeof(App_GlobalResources.Resource), ErrorMessageResourceName = "Password_max")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessageResourceType = typeof(App_GlobalResources.Resource), ErrorMessageResourceName = "Password_min")]
        [MaxLength(12, ErrorMessageResourceType = typeof(App_GlobalResources.Resource), ErrorMessageResourceName = "Password_max")]
        [Display(Name = "Nova Senha")]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(12)]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessageResourceType = typeof(App_GlobalResources.Resource), ErrorMessageResourceName = "Password_min")]
        [MaxLength(12, ErrorMessageResourceType = typeof(App_GlobalResources.Resource), ErrorMessageResourceName = "Password_max")]
        [Display(Name = "Confirmar Senha")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(App_GlobalResources.Resource), ErrorMessageResourceName = "TrocaSenha")]
        public string ConfirmPassword { get; set; }
    }
}