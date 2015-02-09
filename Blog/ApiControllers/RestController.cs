using Blog.Models;
using Blog.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;


namespace Blog.ApiControllers
{
    public class RestController : ApiController
    {
        UsuarioRepositorio userRepo = new UsuarioRepositorio();

        // GET api/<controller>
        public IEnumerable<UsuarioRest> Get()
        {
            return GetUsuarios();
        }

        // GET api/<controller>/5
        public UsuarioRest Get(int id)
        {
            try
            {
                UsuarioRest userRest = new UsuarioRest();
                Usuario user = userRepo.Find(id);
                if (user != null)
                {                    
                    userRest.codigo = user.codigo;
                    userRest.nome = user.nome;
                    userRest.email = user.email;
                    userRest.cidade = user.cidade;
                    userRest.telefone = user.telefone;
                }
                return userRest;
            }
            catch (Exception)
            {               
                throw;
            }
        }

        [Route("login")]
        [HttpGet]
        public HttpResponseMessage Login(String email, String senha) 
        {
            try
            {                
                Usuario usuario = userRepo.Get(u => u.email.Equals(email)).FirstOrDefault<Usuario>();

                if (BCrypt.Net.BCrypt.Verify(senha, usuario.senha))
                {
                    userRepo.Dispose();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    userRepo.Dispose();
                    return Request.CreateResponse(HttpStatusCode.NotFound);   
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }            
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
                var user = userRepo.Find(id);
                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else 
                {
                    userRepo.Excluir(u => u.codigo == id);
                    userRepo.Dispose();
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
            List<UsuarioRest> userRest = new List<UsuarioRest>();
            var lista = userRepo.GetAll();

            foreach (var i in lista)
            {
                userRest.Add(new UsuarioRest() { codigo = i.codigo, nome = i.nome, email = i.email, telefone = i.telefone, cidade = i.cidade });
            }

            userRepo.Dispose();
            return userRest;
        }
    }
}