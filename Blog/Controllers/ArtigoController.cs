using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.Hosting;
using System.Net;
using Blog.Security;
using Blog.Repositorios;

namespace Blog.Controllers
{
    public class ArtigoController : Controller
    {
        PostRepositorio postRepos = new PostRepositorio();

        [HttpGet]
        [EncryptedActionParameter]
        public ActionResult Index(int id, String sortOrder, String searchString, String currentFilter, int? page)
        {
            using (BlogContext context = new BlogContext())
            {
                ViewData["Categorias"] = context.Categoria.ToList<Categoria>();

                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

                if (searchString != null)
                {
                    page = 1;
                }
                else 
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                List<Post> posts = new List<Post>();
                if (id != 0)
                {
                    posts = context.Post.Where(p => p.Id == id).ToList<Post>();
                }
                else 
                {
                    posts = context.Post.ToList<Post>();
                }                
                
                if (!String.IsNullOrEmpty(searchString))
                {
                    posts = posts.Where(p => p.Autor.Contains(searchString) || p.Texto_1.Contains(searchString)).ToList<Post>();
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        posts = posts.OrderByDescending(p => p.Autor).ToList<Post>();
                        break;
                    case "Date":
                        posts = posts.OrderBy(p => p.Data_cadastro).ToList<Post>();
                        break;
                    case "date_desc":
                        posts.OrderByDescending(p => p.Data_cadastro);
                        break;
                    default:
                        posts = posts.OrderBy(p => p.Autor).ToList<Post>();
                        break;
                }

                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(posts.ToPagedList(pageNumber, pageSize));
            }
        }

        [HttpGet]
        public ActionResult AlternateIndex(string id, String sortOrder, String searchString, String currentFilter, int? page)
        {
            using (BlogContext context = new BlogContext())
            {
                ViewData["Categorias"] = context.Categoria.ToList<Categoria>();

                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                List<Post> posts = new List<Post>();
                if (id != null)
                {
                    int idSearch = int.Parse(id);
                    posts = context.Post.Where(p => p.Id == idSearch).ToList<Post>();
                }
                else
                {
                    posts = context.Post.ToList<Post>();
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    posts = posts.Where(p => p.Autor.Contains(searchString) || p.Texto_1.Contains(searchString)).ToList<Post>();
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        posts = posts.OrderByDescending(p => p.Autor).ToList<Post>();
                        break;
                    case "Date":
                        posts = posts.OrderBy(p => p.Data_cadastro).ToList<Post>();
                        break;
                    case "date_desc":
                        posts.OrderByDescending(p => p.Data_cadastro);
                        break;
                    default:
                        posts = posts.OrderBy(p => p.Autor).ToList<Post>();
                        break;
                }

                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(posts.ToPagedList(pageNumber, pageSize));
            }
        }

        [HttpGet]
        [Authorize(Roles = "ROLE_ADMINISTRADOR")]
        public ActionResult Delete(int? id) 
        {
            using (BlogContext context = new BlogContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DownloadFileInfo info = context.DownloadFileInfo.Find(id);
                if (info == null)
                {
                    return HttpNotFound();
                }
                return View(info);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ROLE_ADMINISTRADOR")]
        public ActionResult Delete(int id) 
        {
            using (BlogContext context = new BlogContext())
            {
                DownloadFileInfo info = context.DownloadFileInfo.Find(id);
                context.DownloadFileInfo.Remove(info);
                context.SaveChanges();
                return RedirectToAction("Projetos");
            }
        }

        [HttpGet]
        [Authorize(Roles = "ROLE_ADMINISTRADOR")]
        public ActionResult DeleteArtigo(int? id) 
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else 
                {
                    var artigos = postRepos.Find(id);
                    postRepos.Dispose();
                    return View(artigos);   
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "ROLE_ADMINISTRADOR")]
        public ActionResult DeleteArtigo(int id) 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else 
            {
                postRepos.Excluir(p => p.Id == id);
                return RedirectToAction("AlternateIndex");
            }
        }

        [HttpGet]
        [Authorize(Roles = "ROLE_ADMINISTRADOR")]
        public ActionResult Edit(int? id) 
        {
            using (BlogContext context = new BlogContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DownloadFileInfo info = context.DownloadFileInfo.Find(id);
                if (info == null)
                {
                    return HttpNotFound();
                }
                return View(info);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ROLE_ADMINISTRADOR")]
        public ActionResult Edit([Bind(Include = "FileID, descricao")]DownloadFileInfo info) 
        {
            using (BlogContext context = new BlogContext())
            {
                if (ModelState.IsValid)
                {
                    context.Entry(info).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("Projetos");
                }
                return View(info);
            }
        }

        [HttpGet]
        public ActionResult Olhar(String id) 
        {
            using (BlogContext context = new BlogContext())
            {   
                int idSearch = int.Parse(id);
                Post post = context.Post.Where(p => p.Id == idSearch).FirstOrDefault<Post>();

                String nomeArquivo = Path.GetFileName(post.FotoAutor);
                String nomeFoto1 = Path.GetFileName(post.Caminho_Foto_1);
                String nomeFoto2 = Path.GetFileName(post.Caminho_Foto_2);
                String nomeFoto3 = Path.GetFileName(post.Caminho_Foto_3);
                String nomeFoto4 = Path.GetFileName(post.Caminho_Foto_4);
                String nomeFoto5 = Path.GetFileName(post.Caminho_Foto_5);

                ViewBag.Titulo = post.titulo;
                ViewBag.FotoAutor = "~/Content/Uploads/" + nomeArquivo;
                ViewBag.Foto1 = "~/Content/Uploads/" + nomeFoto1;
                ViewBag.Foto2 = "~/Content/Uploads/" + nomeFoto2;
                ViewBag.Foto3 = "~/Content/Uploads/" + nomeFoto3;
                ViewBag.Foto4 = "~/Content/Uploads/" + nomeFoto4;
                ViewBag.Foto5 = "~/Content/Uploads/" + nomeFoto5;

                return View(post);
            }
        }

        [HttpGet]
        [Authorize(Roles = "ROLE_ADMINISTRADOR")]
        public ActionResult CriarArtigo()
        {
            ViewData["Categorias"] = GetDropDown();
            return View();
        }

        public static List<SelectListItem> GetDropDown() 
        {
            using (BlogContext context = new BlogContext())
            {
                List<SelectListItem> ls = new List<SelectListItem>();
                var lm = context.Categoria.ToList<Categoria>();

                foreach (var c in lm)
                {
                    ls.Add(new SelectListItem() { Text = c.nome, Value = c.id.ToString() });
                }
                return ls;
            }
        }

        [HttpGet]
        public ActionResult Projetos() 
        {
            var listaDeArquivos = GetFiles();
            return View(listaDeArquivos);
        }

        [HttpGet]
        [Authorize(Roles = "ROLE_MEMBRO,ROLE_ADMINISTRADOR")]
        public FileResult Download(String id) 
        {
            int CurrentFileID = Convert.ToInt32(id);
            var filesCol = GetFiles();
            string CurrentFileName = (from fls in filesCol where fls.FileID == CurrentFileID
                                      select fls.FilePath).First();

            String nomeArquivo = Path.GetFileNameWithoutExtension(CurrentFileName);
            string contentType = String.Empty;

            if (CurrentFileName.Contains(".pdf"))
            {
                contentType = "application/pdf";
            }
            if(CurrentFileName.Contains(".docx")) 
            {
                contentType = "application/docx";
            }
            if (CurrentFileName.Contains(".zip")) 
            {
                contentType = "application/zip";
            }
            return File(CurrentFileName, contentType, nomeArquivo);
        }

        public List<DownloadFileInfo> GetFiles() 
        {            
            using (BlogContext context = new BlogContext())
            {
                List<DownloadFileInfo> listFiles = new List<DownloadFileInfo>();
                listFiles = context.DownloadFileInfo.ToList<DownloadFileInfo>();

                return listFiles;
            }
        }

        [HttpGet]
        [Authorize(Roles = "ROLE_ADMINISTRADOR")]
        public ActionResult Upload() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ROLE_ADMINISTRADOR")]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> arquivos, DownloadFileInfo info) 
        {
            try
            {
                using (BlogContext context = new BlogContext())
                {
                    foreach (var arquivo in arquivos)
                    {
                        if (arquivo != null && arquivo.ContentLength > 0)
                        {
                            if (ModelState.IsValid)
                            {
                                var nomeArquivo = Path.GetFileName(arquivo.FileName);
                                var caminho = Path.Combine(Server.MapPath("~/Content/Projetos"), nomeArquivo);

                                arquivo.SaveAs(caminho);

                                info.FileName = nomeArquivo;
                                info.FilePath = caminho;

                                context.DownloadFileInfo.Add(info);
                                context.SaveChanges();

                                return RedirectToAction("Upload");
                            }
                            else 
                            {
                                ModelState.AddModelError("", App_GlobalResources.Resource.File_error);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {                
                throw;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ROLE_ADMINISTRADOR")]
        public ActionResult CriarArtigo(IEnumerable<HttpPostedFileBase> arquivos, Post post, String Categoria)
        {
            ViewData["Categorias"] = GetDropDown();

            try
            {
                using (BlogContext context = new BlogContext())
                {
                    if (ModelState.IsValid == false)
                    {
                        foreach (var arquivo in arquivos)
                        {
                            if (arquivo != null && arquivo.ContentLength > 0)
                            {
                                var nomeArquivo = Path.GetFileName(arquivo.FileName);
                                var caminho = Path.Combine(Server.MapPath("~/Content/Uploads"), nomeArquivo);

                                if (Session["FOTO_1"] == null)
                                {
                                    Session["FOTO_1"] = caminho;
                                }

                                if (Session["FOTO_1"] != null)
                                {
                                    if (Session["FOTO_2"] == null)
                                    {
                                        String caminho1 = Session["FOTO_1"].ToString();
                                        String caminho2 = caminho;
                                        if (caminho1 != caminho2)
                                        {
                                            Session["FOTO_2"] = caminho;
                                        }
                                    }
                                }

                                if (Session["FOTO_2"] != null)
                                {
                                    if (Session["FOTO_3"] == null)
                                    {
                                        String caminho1 = Session["FOTO_2"].ToString();
                                        String caminho2 = caminho;
                                        if (caminho1 != caminho2)
                                        {
                                            Session["FOTO_3"] = caminho;
                                        }
                                    }
                                }

                                if (Session["FOTO_3"] != null)
                                {
                                    if (Session["FOTO_4"] == null)
                                    {
                                        String caminho1 = Session["FOTO_3"].ToString();
                                        String caminho2 = caminho;
                                        if (caminho1 != caminho2)
                                        {
                                            Session["FOTO_4"] = caminho;
                                        }
                                    }
                                }

                                if (Session["FOTO_4"] != null)
                                {
                                    if (Session["FOTO_5"] == null)
                                    {
                                        String caminho1 = Session["FOTO_4"].ToString();
                                        String caminho2 = caminho;
                                        if (caminho1 != caminho2)
                                        {
                                            Session["FOTO_5"] = caminho;
                                        }
                                    }
                                }

                                if (Session["FOTO_5"] != null)
                                {
                                    if (Session["FOTO_6"] == null)
                                    {
                                        String caminho1 = Session["FOTO_5"].ToString();
                                        String caminho2 = caminho;
                                        if (caminho1 != caminho2)
                                        {
                                            Session["FOTO_6"] = caminho;
                                        }
                                    }
                                }

                                arquivo.SaveAs(caminho);
                            }
                        }

                        post.FotoAutor = Session["FOTO_1"].ToString();
                        post.Caminho_Foto_1 = Session["FOTO_2"].ToString();
                        post.Caminho_Foto_2 = Session["FOTO_3"].ToString();
                        post.Caminho_Foto_3 = Session["FOTO_4"].ToString();
                        post.Caminho_Foto_4 = Session["FOTO_5"].ToString();
                        post.Caminho_Foto_5 = Session["FOTO_6"].ToString();

                        if (Categoria != null)
                        {
                            post.Categoria = context.Categoria.Find(int.Parse(Categoria));
                        }

                        post.Data_cadastro = DateTime.Now;
                        post.Aprovado = true;

                        context.Post.Add(post);
                        context.SaveChanges();

                        Session["FOTO_1"] = null;
                        Session["FOTO_2"] = null;
                        Session["FOTO_3"] = null;
                        Session["FOTO_4"] = null;
                        Session["FOTO_5"] = null;
                        Session["FOTO_6"] = null;

                        return RedirectToAction("Index");
                    }
                    else 
                    {
                        ModelState.AddModelError("", "Não foi possivel enviar o Artigo!");
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

            return View();
        }

    }
}
