using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blog.ApiControllers
{
    public class RestController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<UsuarioRest> Get()
        {
            return GetUsuarios();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        private List<UsuarioRest> GetUsuarios() 
        {
            using (BlogContext context = new BlogContext())
            {
                List<UsuarioRest> userRest = new List<UsuarioRest>();
                var lista = context.Usuario.Select(u => new { u.codigo, u.nome, u.telefone, u.email, u.cidade });
                foreach (var i in lista)
                {
                    userRest.Add(new UsuarioRest() { codigo = i.codigo, nome = i.nome, email = i.email, telefone = i.telefone, cidade = i.cidade });
                }
                return userRest;
            }
        }
    }
}