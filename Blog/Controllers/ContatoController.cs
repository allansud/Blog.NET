using Blog.Models;
using Blog.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class ContatoController : Controller
    {
        //
        // GET: /Contato/

        public ActionResult Index()
        {
            ViewBag.Title = "Deixe seu contato!";

            using (BlogContext context = new BlogContext())
            {
                ViewData["Categorias"] = context.Categoria.ToList<Categoria>();
            }
            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Contato contato) 
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    UtilRepositorio.EnviarEmailContato(contato);

                    return RedirectToAction("Index", "Contato");
                }                
            }
            catch (Exception)
            {                
                throw;
            }
            return View();
        }
    }
}
