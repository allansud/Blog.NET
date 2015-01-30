using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;

namespace Blog.WCFService
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class RestService
    {
        [WebGet(UriTemplate = "Usuarios")]
        public List<Usuario> GetUsuarios() 
        {
            using (BlogContext context = new BlogContext())
            {
                var usuarios = context.Usuario.ToList<Usuario>();
                return usuarios;
            }
        }
    }
}