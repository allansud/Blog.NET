using Blog.Models;
using Blog.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;


namespace Blog.ApiControllers
{
    public class RestController : ApiController
    {
        Blog.Repositorios.UsuarioRepositorio userRepo = new Blog.Repositorios.UsuarioRepositorio();
        Blog.Repositorios.ReuniaoFamiliarRepositorio reuniaoRepositorio = new Blog.Repositorios.ReuniaoFamiliarRepositorio();


        // GET api/<controller>
        public IEnumerable<UsuarioRest> Get()
        {
            return GetUsuarios();
        }

        [HttpGet]
        [Route("ListaReunioes")]
        public IEnumerable<ReuniaoFamiliarRest> GetReunioes(String reuniao) 
        {
            List<ReuniaoFamiliarRest> listaReunioes = new List<ReuniaoFamiliarRest>();
            var reunioesFamiliares = reuniaoRepositorio.GetAll();

            foreach (var r in reunioesFamiliares) 
            {
                listaReunioes.Add(new ReuniaoFamiliarRest{ Codigo = r.Id, Titulo = r.Titulo, Palestrante = r.Palestrante, Data = r.Data});
            }
            return listaReunioes;
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
        [Route("usuario")]
        [HttpPost]
        public HttpResponseMessage Post(String nome, String email, String senha, String confirmarsenha, String cidade, String telefone)
        {
            try
            {
                if (nome == String.Empty || email == String.Empty || senha == String.Empty
                    && cidade == String.Empty || telefone == String.Empty)
                {
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read subject/tutor from body");
                }
                if (nome != String.Empty && email != String.Empty && senha != String.Empty
                    && cidade != String.Empty && telefone != String.Empty)
                {
                    Permissao p = new Permissao();
                    p.data_cadastro = DateTime.Now.Date;
                    p.role = "ROLE_MEMBRO";

                    String senhaEncrypted = BCrypt.Net.BCrypt.HashPassword(senha);

                    Usuario usuario = new Usuario();

                    usuario.nome = nome;
                    usuario.email = email;
                    usuario.senha = senhaEncrypted;
                    usuario.confirmarSenha = senhaEncrypted;
                    usuario.cidade = cidade;
                    usuario.telefone = telefone;
                    usuario.data_cadastro = DateTime.Now.ToShortDateString();

                    usuario.Permissoes.Add(p);
                    userRepo.Adicionar(usuario);
                    userRepo.SalvarTodos();

                    UtilRepositorio.EnviarEmail(usuario, senha, "Bem vindo ao meu BLOG onde você podera encontrar algumas informações úteis, apesar de ainda não ter terminado.");
                    return Request.CreateResponse(HttpStatusCode.Created, usuario);
                }
                else 
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Não foi possível salvar no banco de dados.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Route("reunioes")]
        [HttpPost]
        public HttpResponseMessage PostReuniao(String titulo, String palestrante, String data) 
        {
            try
            {
                if (titulo == String.Empty || palestrante == String.Empty || data == String.Empty)
                {
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, "Algum dos campos está em branco?");
                }
                if (titulo != String.Empty && palestrante != String.Empty && data != String.Empty)
                {
                    ReuniaoFamiliar rf = new ReuniaoFamiliar();

                    rf.Titulo = titulo;
                    rf.Palestrante = palestrante;
                    rf.Data = data;

                    reuniaoRepositorio.Adicionar(rf);
                    reuniaoRepositorio.SalvarTodos();

                    return Request.CreateResponse(HttpStatusCode.Created, rf);
                }
                else 
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Não foi possível salvar no banco de dados.");
                }
            }
            catch (Exception)
            {                
                throw;
            }
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