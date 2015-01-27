using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BCrypt.Net;
using System.Data.Entity;
using System.Web.Script.Serialization;
using Blog.Security;

namespace Blog.Repositories
{
    public class UsuarioRepositorio
    {
        public Usuario GetUsuario(String email, String senha) 
        {
            using (BlogContext db = new BlogContext())
            {
                Usuario user = db.Usuario.Where(u => u.email == email).FirstOrDefault<Usuario>();

                try
                {
                    if (this.VerificaPassword(senha, user.senha)) 
                    {
                        return user;
                    }
                    else
                        return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public Usuario GetUsuario(String email) 
        {
            using (BlogContext db = new BlogContext())
            {
                return db.Usuario.Where(u => u.email == email).SingleOrDefault();
            }
        }

        //Recupera um usuário com base em seu ID
        public Usuario GetUsuario(int id) 
        {
            using (BlogContext db = new BlogContext())
            {
                return db.Usuario.Where(u => u.codigo == id).SingleOrDefault();
            }
        }

        public Usuario GetUsuarioByEmail(string email)
        {
            using (BlogContext ctx = new BlogContext())
            {
                return ctx.Usuario.Where(t => t.email == email).SingleOrDefault();
            }
        }

        //Lista todos os usuarios
        public List<Usuario> GetUsuarios()
        {
            using (BlogContext ctx = new BlogContext())
            {
                return ctx.Usuario.ToList();
            }
        }

        public void AddUsuario(Usuario usuario, string roles)
        {
            using (BlogContext ctx = new BlogContext())
            {
                usuario.Permissoes = this.CreateListRolesFromString(roles, ctx);
                ctx.Usuario.Add(usuario);
                ctx.SaveChanges();
            }
        }

        private List<Permissao> CreateListRolesFromString(string roles, BlogContext ctx)
        {
            List<Permissao> Roles = new List<Permissao>();
            if (!string.IsNullOrWhiteSpace(roles))
            {
                foreach (string roleId in roles.Split(','))
                {
                    int _roleId = Convert.ToInt32(roleId);
                    Permissao role = ctx.Permissao.Where(t => t.codigo == _roleId).SingleOrDefault();
                    Roles.Add(role);
                }
            }
            return Roles;
        }

        public void SaveUsuario(Usuario usuario)
        {
            using (BlogContext ctx = new BlogContext())
            {
                //Se a Senha ainda não está criptografada criptografo
                if (!IsPasswordEncrypted(usuario.senha))
                    usuario.senha = this.EncryptPassword(usuario.senha);

                //Alterando o usuário
                ctx.Entry(usuario).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void SaveUsuario(Usuario usuario, string roles)
        {
            using (BlogContext ctx = new BlogContext())
            {
                //Se a Senha ainda não está criptografada criptografo
                if (!IsPasswordEncrypted(usuario.senha))
                    usuario.senha = this.EncryptPassword(usuario.senha);

                //Alterando o usuário
                ctx.Entry(usuario).State = EntityState.Modified;

                // Limpo as roles existentes e incluo as novas roles no usuário
                usuario.Permissoes.Clear();
                usuario.Permissoes = this.CreateListRolesFromString(roles, ctx);

                ctx.SaveChanges();

            }
        }

        public void RemoveUsuario(int Id)
        {
            using (BlogContext ctx = new BlogContext())
            {
                Usuario usuario = ctx.Usuario.Find(Id);
                ctx.Usuario.Remove(usuario);
                ctx.SaveChanges();
            }
        }

        //Verifica se uma determinada role pertence a um determinado usuário
        public bool IsUsuarioInRole(string login, string role)
        {
            using (BlogContext ctx = new BlogContext())
            {
                Usuario user = ctx.Usuario.FirstOrDefault(u => u.email.Equals(login, StringComparison.CurrentCultureIgnoreCase)
                    || u.email.Equals(login, StringComparison.CurrentCultureIgnoreCase));
                return user.Permissoes.Where(t => t.role == role).Count() > 0;
            }
        }

        //Recupera as Roles de um usuário
        public string[] GetRoles(string login)
        {
            using (BlogContext ctx = new BlogContext())
            {
                Usuario user = ctx.Usuario.FirstOrDefault(u => u.email.Equals(login, StringComparison.CurrentCultureIgnoreCase)
                    || u.email.Equals(login, StringComparison.CurrentCultureIgnoreCase));
                List<string> roles = new List<string>();

                if (user != null)
                {
                    foreach (Permissao role in user.Permissoes)
                    {
                        roles.Add(role.role);
                    }
                }

                return roles.ToArray();
            }
        }

        //Todas as roles
        public List<Permissao> GetAllRoles()
        {
            using (var ctx = new BlogContext())
            {
                return ctx.Permissao.ToList();
            }
        }    

        public List<Permissao> GetRoles(int IdUsuario)
        {
            using (var ctx = new BlogContext())
            {
                Usuario user = ctx.Usuario.FirstOrDefault(u => u.codigo.Equals(IdUsuario));
                return user.Permissoes.ToList();
            }
        }

        private bool IsPasswordEncrypted(string password)
        {
            //Se a senha é maior ou igual a 7 caracteres e o inicio começa com $2a$10$ é uma senha criptografada.
            string senha = password.Substring(0, 7);
            return password.Length >= 7 && senha == "$2a$10$";
        }

        public string EncryptPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerificaPassword(String senhaNormal, String senhaEncrypted) 
        {
            //Verifica se uma senha encryptada confere com a senha não encryptada
            return BCrypt.Net.BCrypt.Verify(senhaNormal, senhaEncrypted);
        }       

        public static void Deslogar() 
        {
            FormsAuthentication.SignOut();
        }

        public static void Logoff(HttpSessionStateBase session, HttpResponseBase response)
        {
            // Delete the user details from cache.
            session.Abandon();

            // Delete the authentication ticket and sign out.
            FormsAuthentication.SignOut();

            // Clear authentication cookie.
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            response.Cookies.Add(cookie);
        }
    }
}