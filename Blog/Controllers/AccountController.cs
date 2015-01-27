using Blog.Models;
using Blog.Repositories;
using Blog.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {

        private UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();

        [HttpGet]
        public ActionResult Registrar()
        {
            using (BlogContext context = new BlogContext())
            {
                ViewData["Categorias"] = context.Categoria.ToList<Categoria>();
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BlogContext db = new BlogContext())
                    {
                        ViewData["Categorias"] = db.Categoria.ToList<Categoria>();

                        if (usuario.email.Equals("freitasallan@gmail.com"))
                        {
                            if (UtilRepositorio.VerificaCadastro(db, usuario.email))
                            {
                                Permissao p = new Permissao();
                                p.data_cadastro = DateTime.Now.Date;
                                p.role = "ROLE_ADMINISTRADOR";

                                String senha_n = usuario.senha;
                                String senhaEncrypted = usuarioRepositorio.EncryptPassword(usuario.senha);
                                usuario.senha = senhaEncrypted;
                                usuario.confirmarSenha = senhaEncrypted;
                                usuario.data_cadastro = DateTime.Now.Date;
                                usuario.Permissoes.Add(p);
                                db.Usuario.Add(usuario);
                                db.SaveChanges();

                                UtilRepositorio.EnviarEmail(usuario, senha_n, "BEM VINDO AO ALLAN'S PORTFOLIO");

                                usuario = null;
                                p = null;

                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ModelState.AddModelError("", App_GlobalResources.Resource.Record);
                            }
                        }
                        else
                        {
                            if (UtilRepositorio.VerificaCadastro(db, usuario.email))
                            {
                                Permissao p = new Permissao();
                                p.data_cadastro = DateTime.Now.Date;
                                p.role = "ROLE_MEMBRO";

                                String senha_n = usuario.senha;
                                String senhaEncrypted = usuarioRepositorio.EncryptPassword(usuario.senha);
                                usuario.senha = senhaEncrypted;
                                usuario.confirmarSenha = senhaEncrypted;
                                usuario.data_cadastro = DateTime.Now.Date;
                                usuario.Permissoes.Add(p);
                                db.Usuario.Add(usuario);
                                db.SaveChanges();

                                UtilRepositorio.EnviarEmail(usuario, senha_n, "BEM VINDO AO ALLAN'S PORTFOLIO");

                                usuario = null;
                                p = null;

                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ModelState.AddModelError("", App_GlobalResources.Resource.Record);
                            }
                        }
                    }
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string mensagem = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);
                        raise = new InvalidOperationException(mensagem, raise);
                    }
                }
            }
            return View(usuario);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Logar()
        {
            using (BlogContext context = new BlogContext())
            {
                ViewData["Categorias"] = context.Categoria.ToList<Categoria>();
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Logar(UsuarioLogin model, string returnUrl)
        {
            using (BlogContext context = new BlogContext())
            {
                ViewData["Categorias"] = context.Categoria.ToList<Categoria>();
            }

            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.email, model.senha))
                {
                    FormsAuthentication.SetAuthCookie(model.email, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "O Login ou a senha não estão corretos.");
                }
            }

            // Alguma coisa deu errado...
            return View(model);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", null);
        }

        [Authorize]
        [HttpGet]
        public ActionResult MudarSenha() 
        {
            return View();
        }

        
        [HttpPost]
        [Authorize(Roles = "ROLE_MEMBRO")]
        public ActionResult MudarSenha(MudarPasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather 
                // than return false in certain failure scenarios. 
                bool changePasswordSucceeded;
                try
                {
                    using (BlogContext context = new BlogContext())
                    {
                        Usuario user = context.Usuario.Where(u => u.email == User.Identity.Name).FirstOrDefault<Usuario>();
                        CustomMembership cm = new CustomMembership();
                        changePasswordSucceeded = cm.ChangePassword(user.email, model.OldPassword, model.NewPassword);   
                    }
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", App_GlobalResources.Resource.TrocaSenha);
                }
            }

            //Se chegou até aqui é porque algo deu errado! 
            return View(model);
        }

        [HttpGet]
        public ActionResult ChangePasswordSuccess() 
        {
            return View();
        }
    }
}

