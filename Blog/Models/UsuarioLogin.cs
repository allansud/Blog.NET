using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class UsuarioLogin
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessageResourceType = typeof(App_GlobalResources.Resource), ErrorMessageResourceName = "Email_req")]
        [DataType(DataType.EmailAddress, ErrorMessageResourceType = typeof(App_GlobalResources.Resource), ErrorMessageResourceName = "Email_inv")]
        public String email { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessageResourceType = typeof(App_GlobalResources.Resource), ErrorMessageResourceName = "Password_min")]
        [MaxLength(12, ErrorMessageResourceType = typeof(App_GlobalResources.Resource), ErrorMessageResourceName = "Password_max")]
        [Required(ErrorMessageResourceType = typeof(App_GlobalResources.Resource), ErrorMessageResourceName = "Password_req")]
        public String senha { get; set; }

        [Display(Name = "Manter Logado?")]
        public bool RememberMe { get; set; }
    }
}