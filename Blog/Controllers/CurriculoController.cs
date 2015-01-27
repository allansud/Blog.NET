using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class CurriculoController : Controller
    {
        //
        // GET: /Curriculo/

        [HttpGet]
        public ActionResult Index()
        {
            List<Categoria> categoria = null;

            using (BlogContext context = new BlogContext())
            {
                ViewData["Categorias"] = context.Categoria.ToList<Categoria>();
                categoria = context.Categoria.ToList<Categoria>();
            }

            ViewBag.Title = "Curriculum";
            ViewBag.Title2 = "Formação Escolar";
            ViewBag.Title3 = "Experiências Profissionais";
            ViewBag.Title4 = "Idiomas";
            ViewBag.Title5 = "Cursos Extracurriculares";

            return View(categoria);
        }
    }
}
