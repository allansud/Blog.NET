using Blog.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class BlogContext : DbContext
    {
        public BlogContext() : base("BlogContext") 
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        private UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Permissao> Permissao { get; set; }
        public DbSet<Quotes> Quote { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<DownloadFileInfo> DownloadFileInfo { get; set; }
        public DbSet<ReuniaoFamiliar> ReuniaoFamiliar { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public void Seed(BlogContext context) 
        {
            try
            {
                var reunioesFamiliares = new List<ReuniaoFamiliar>
                {
                    new ReuniaoFamiliar {Titulo = "A Razão de Nossa Esperança", Palestrante = "Junior", Data = DateTime.Now.ToShortDateString() },
                    new ReuniaoFamiliar {Titulo = "Amar os Outrose Viver com as DIferenças", Palestrante = "Salvio", Data = DateTime.Now.ToShortDateString() },
                    new ReuniaoFamiliar {Titulo = "Pais: Os melhores Professores do Evangelho", Palestrante = "Arivaldo", Data = DateTime.Now.ToShortDateString() },
                    new ReuniaoFamiliar {Titulo = "Sim, Senhor, Eu Te Seguirei", Palestrante = "Darci", Data = DateTime.Now.ToShortDateString() },
                    new ReuniaoFamiliar {Titulo = "Aproximar-se do Trono de Deus com Confiança", Palestrante = "Daisy", Data = DateTime.Now.ToShortDateString() }
                };

                reunioesFamiliares.ForEach(r => context.ReuniaoFamiliar.Add(r));
                context.SaveChanges();

                var quotes = new List<Quotes>
                {
                    new Quotes {quote = "Learn from yesterday, live for today, hope for tomorrow. The important thing is not to stop questioning. Albert Einstein"},
                    new Quotes {quote = "The best and most beautiful things in the world cannot be seen or even touched - they must be felt with the heart. Helen Keller"},
                    new Quotes {quote = "Nothing is impossible, the word itself says 'I'm possible'! Audrey Hepburn"},
                    new Quotes {quote = "As we express our gratitude, we must never forget that the highest appreciation is not to utter words, but to live by them. John F. Kennedy"}
                };

                quotes.ForEach(q => context.Quote.Add(q));
                context.SaveChanges();

                var categorias = new List<Categoria>
                {
                    new Categoria { nome = "Mikrotik"},
                    new Categoria { nome = "Java"},
                    new Categoria { nome = "ASP.NET Web Forms"},
                    new Categoria { nome = "ASP.NET MVC" }
                };

                categorias.ForEach(c => context.Categoria.Add(c));
                context.SaveChanges();

            }
            catch (DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors  in dbEx.EntityValidationErrors) 
                {
                    foreach (var validationError in validationErrors.ValidationErrors) 
                    {
                        string mensagem = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);
                        raise = new InvalidOperationException(mensagem, raise);
                    }
                }
            }
        }

        public class DropCreateifChangeInitializer : DropCreateDatabaseIfModelChanges<BlogContext> 
        {
            protected override void Seed(BlogContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }
    }
}