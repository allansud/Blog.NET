using Blog.Models;
using Blog.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class RoleController : Controller
    {
        private UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();
        //
        // GET: /Role/
        [HttpGet]
        [Authorize(Roles = "ROLE_ADMINISTRADOR")]
        public ActionResult Index()
        {
            using (BlogContext context = new BlogContext())
            {
                ViewData["Categorias"] = context.Categoria.ToList<Categoria>();
            }
            return View(usuarioRepositorio.GetAllRoles());
        }

        [HttpGet]
        [Authorize(Roles = "ROLE_ADMINISTRADOR")]
        public ActionResult create() 
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "ROLE_ADMINISTRADOR")]
        public ActionResult create(Permissao p) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BlogContext context = new BlogContext())
                    {
                        context.Permissao.Add(p);
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception)
            {                
                throw;
            }
            return View(p);
        }

        [HttpGet]
        [Authorize(Roles = "ROLE_ADMINISTRADOR")]
        public ActionResult Edit(int id) 
        {
            using (BlogContext context = new BlogContext())
            {
                Permissao p = context.Permissao.Find(id);
                return View(p);
            }            
        }

        [HttpPost]
        [Authorize(Roles = "ROLE_ADMINISTRADOR")]
        public ActionResult Edit(Permissao p) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BlogContext context = new BlogContext())
                    {
                        context.Entry(p).State = EntityState.Modified;
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception)
            {                
                throw;
            }
            return View(p);
        }

        public ActionResult Delete(int id) 
        {
            using (BlogContext context = new BlogContext())
            {
                Permissao p = context.Permissao.Find(id);
                return View(p);
            }
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "ROLE_ADMINISTRADOR")]
        public ActionResult DeleteConfirmed(int id) 
        {
            using (BlogContext context = new BlogContext())
            {
                Permissao p = context.Permissao.Find(id);
                context.Permissao.Remove(p);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
