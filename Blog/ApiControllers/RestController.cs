using Blog.Models;
using Blog.Repositories;
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
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (BlogContext context = new BlogContext())
                {
                    UsuarioRepositorio userRepos = new UsuarioRepositorio();
                    var user = context.Usuario.Find(id);
                    if (user == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                    if (userRepos.RemoveUsuario(id))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
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