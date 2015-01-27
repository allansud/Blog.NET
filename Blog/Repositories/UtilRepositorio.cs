using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Blog.Repositories
{
    public class UtilRepositorio
    {
        public static Boolean VerificaCadastro(BlogContext context, String email) 
        {
            List<Usuario> user = context.Usuario.Where(u => u.email == email).ToList<Usuario>();

            if (user.Count >= 1)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }

        public static void EnviarEmail(String to, String nome, String mensagem) 
        {
            SmtpClient cliente = new SmtpClient();
            cliente.Host = "216.245.211.29";
            cliente.EnableSsl = false;
            cliente.Credentials = new NetworkCredential("naoresponder@allanfreitas.net", "Befw11s4");
            cliente.Port = 587;

            MailMessage message = new MailMessage();
            message.BodyEncoding = Encoding.UTF8;
            message.Sender = new MailAddress("naoresponder@allanfreitas.net", "Allan's Portfolio");
            message.From = new MailAddress("naoresponder@allanfreitas.net", "Allan's Portfolio");
            message.To.Add(new MailAddress(to, nome));
            message.Subject = "BEM VINDO AO ALLAN'S PORTFOLIO";
            message.Body = mensagem;
            message.IsBodyHtml = false;
            message.Priority = MailPriority.High;

            cliente.Send(message);
        }

        public static void EnviarEmail(Usuario usuario, String senha, String mensagem)
        {
            SmtpClient cliente = new SmtpClient();
            cliente.Host = "216.245.211.29";
            cliente.EnableSsl = false;
            cliente.Credentials = new NetworkCredential("naoresponder@allanfreitas.net", "Befw11s4");
            cliente.Port = 587;

            //Cria objeto string builder
            StringBuilder sbBody = new StringBuilder();

            //Adiciona estrutura HTML do E-Mail
            sbBody.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            sbBody.Append("<head><title>Untitled Document</title>");
            sbBody.Append("<style type='text/css'>body {margin-left: 0px;margin-top: 0px;margin-right: 0px;margin-bottom: 0px;background-color: #E1E0F2;}");
            sbBody.Append("body,td,th {font-family: Verdana, Geneva, sans-serif;font-size: 12px;}</style></head><body>");
            sbBody.Append("<strong><h3>.::FORMULÁRIO DE CADASTRO</h3></strong><br />");
            sbBody.Append("<b>Nome:</b><br />");
            //Adiciona texto digitado no TextBox txtNome
            sbBody.Append(usuario.nome);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>E-Mail:</b><br />");
            //Adiciona texto digitado no TextBox txtMail
            sbBody.Append(usuario.email);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Senha:</b><br />");
            //Adiciona texto digitado no TextBox senha
            sbBody.Append(senha);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Telefone Comercial:</b><br />");
            //Adiciona texto digitado no TextBox txtTelefone
            sbBody.Append(usuario.telefone);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Cidade:</b><br />");
            //Adiciona texto digitado no TextBox txtCidade 
            sbBody.Append(usuario.cidade);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Assunto:</b><br />");
            //Adiciona texto digitado no TextBox txtAssunto
            sbBody.Append("BEM VINDO AO ALLAN'S PORTFOLIO");
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Mensagem:</b><br />");
            //Adiciona texto digitado no TextBox txtMensagem
            sbBody.Append(mensagem);
            sbBody.Append("<br /><br />");
            sbBody.Append("<br /></body></html>");

            MailMessage message = new MailMessage();
            message.BodyEncoding = Encoding.UTF8;
            message.Sender = new MailAddress("naoresponder@allanfreitas.net", "Allan's Portfolio");
            message.From = new MailAddress("naoresponder@allanfreitas.net", "Allan's Portfolio");
            message.To.Add(new MailAddress(usuario.email, usuario.nome));
            message.Subject = "BEM VINDO AO ALLAN'S PORTFOLIO";
            message.Body = sbBody.ToString();
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;

            cliente.Send(message);

            EnviarEmailAdmin(usuario, "Um novo cadastro foi efetuado em seu site!");
        }

        public static void EnviarEmailTrocaSenha(Usuario usuario, String senha, String mensagem)
        {
            SmtpClient cliente = new SmtpClient();
            cliente.Host = "216.245.211.29";
            cliente.EnableSsl = false;
            cliente.Credentials = new NetworkCredential("naoresponder@allanfreitas.net", "Befw11s4");
            cliente.Port = 587;

            //Cria objeto string builder
            StringBuilder sbBody = new StringBuilder();

            //Adiciona estrutura HTML do E-Mail
            sbBody.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            sbBody.Append("<head><title>Untitled Document</title>");
            sbBody.Append("<style type='text/css'>body {margin-left: 0px;margin-top: 0px;margin-right: 0px;margin-bottom: 0px;background-color: #E1E0F2;}");
            sbBody.Append("body,td,th {font-family: Verdana, Geneva, sans-serif;font-size: 12px;}</style></head><body>");
            sbBody.Append("<strong><h3>.::ALTERAÇÃO DE SENHA</h3></strong><br />");
            sbBody.Append("<b>Nome:</b><br />");
            //Adiciona texto digitado no TextBox txtNome
            sbBody.Append(usuario.nome);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>E-Mail:</b><br />");
            //Adiciona texto digitado no TextBox txtMail
            sbBody.Append(usuario.email);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Senha:</b><br />");
            //Adiciona texto digitado no TextBox senha
            sbBody.Append(senha);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Telefone Comercial:</b><br />");
            //Adiciona texto digitado no TextBox txtTelefone
            sbBody.Append(usuario.telefone);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Cidade:</b><br />");
            //Adiciona texto digitado no TextBox txtCidade 
            sbBody.Append(usuario.cidade);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Assunto:</b><br />");
            //Adiciona texto digitado no TextBox txtAssunto
            sbBody.Append("ALTERAÇÃO DE SENHA REALIZADA COM SUCESSO!");
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Mensagem:</b><br />");
            //Adiciona texto digitado no TextBox txtMensagem
            sbBody.Append(mensagem);
            sbBody.Append("<br /><br />");
            sbBody.Append("<br /></body></html>");

            MailMessage message = new MailMessage();
            message.BodyEncoding = Encoding.UTF8;
            message.Sender = new MailAddress("naoresponder@allanfreitas.net", "Allan's Portfolio");
            message.From = new MailAddress("naoresponder@allanfreitas.net", "Allan's Portfolio");
            message.To.Add(new MailAddress(usuario.email, usuario.nome));
            message.Subject = "BEM VINDO AO ALLAN'S PORTFOLIO";
            message.Body = sbBody.ToString();
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;

            cliente.Send(message);

            EnviarEmailAdmin(usuario, "Um novo cadastro foi efetuado em seu site!");
        }

        public static void EnviarEmailContato(Contato contato)
        {
            SmtpClient cliente = new SmtpClient();
            cliente.Host = "216.245.211.29";
            cliente.EnableSsl = false;
            cliente.Credentials = new NetworkCredential("naoresponder@allanfreitas.net", "Befw11s4");
            cliente.Port = 587;

            //Cria objeto string builder
            StringBuilder sbBody = new StringBuilder();

            //Adiciona estrutura HTML do E-Mail
            sbBody.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            sbBody.Append("<head><title>Untitled Document</title>");
            sbBody.Append("<style type='text/css'>body {margin-left: 0px;margin-top: 0px;margin-right: 0px;margin-bottom: 0px;background-color: #E1E0F2;}");
            sbBody.Append("body,td,th {font-family: Verdana, Geneva, sans-serif;font-size: 12px;}</style></head><body>");
            sbBody.Append("<strong><h3>.::FORMULÁRIO DE CONTATO</h3></strong><br />");
            sbBody.Append("<b>Nome:</b><br />");
            //Adiciona texto digitado no TextBox txtNome
            sbBody.Append(contato.nome);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>E-Mail:</b><br />");
            //Adiciona texto digitado no TextBox txtMail
            sbBody.Append(contato.email);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Telefone Comercial:</b><br />");
            //Adiciona texto digitado no TextBox txtTelefone
            sbBody.Append(contato.telefone);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Cidade:</b><br />");
            //Adiciona texto digitado no TextBox txtCidade 
            sbBody.Append(contato.cidade);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Assunto:</b><br />");
            //Adiciona texto digitado no TextBox txtAssunto
            sbBody.Append(contato.assunto);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Mensagem:</b><br />");
            //Adiciona texto digitado no TextBox txtMensagem
            sbBody.Append(contato.mensagem);
            sbBody.Append("<br /><br />");
            sbBody.Append("<br /></body></html>");

            MailMessage message = new MailMessage();
            message.BodyEncoding = Encoding.UTF8;
            message.Sender = new MailAddress("naoresponder@allanfreitas.net", "Allan's Portfolio");
            message.From = new MailAddress("naoresponder@allanfreitas.net", "Allan's Portfolio");
            message.To.Add(new MailAddress("allan@allanfreitas.net", "Allan Freitas"));
            message.Subject = "BEM VINDO AO ALLAN'S PORTFOLIO";
            message.Body = sbBody.ToString();
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;

            cliente.Send(message);
        }

        public static void EnviarEmailAdmin(Usuario usuario, String mensagem)
        {
            SmtpClient cliente = new SmtpClient();
            cliente.Host = "216.245.211.29";
            cliente.EnableSsl = false;
            cliente.Credentials = new NetworkCredential("naoresponder@allanfreitas.net", "Befw11s4");
            cliente.Port = 587;

            //Cria objeto string builder
            StringBuilder sbBody = new StringBuilder();

            //Adiciona estrutura HTML do E-Mail
            sbBody.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            sbBody.Append("<head><title>Untitled Document</title>");
            sbBody.Append("<style type='text/css'>body {margin-left: 0px;margin-top: 0px;margin-right: 0px;margin-bottom: 0px;background-color: #E1E0F2;}");
            sbBody.Append("body,td,th {font-family: Verdana, Geneva, sans-serif;font-size: 12px;}</style></head><body>");
            sbBody.Append("<strong><h3>.::FORMULÁRIO DE CADASTRO</h3></strong><br />");
            sbBody.Append("<b>Nome:</b><br />");
            //Adiciona texto digitado no TextBox txtNome
            sbBody.Append(usuario.nome);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>E-Mail:</b><br />");
            //Adiciona texto digitado no TextBox txtMail
            sbBody.Append(usuario.email);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Telefone Comercial:</b><br />");
            //Adiciona texto digitado no TextBox txtTelefone
            sbBody.Append(usuario.telefone);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Cidade:</b><br />");
            //Adiciona texto digitado no TextBox txtCidade 
            sbBody.Append(usuario.cidade);
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Assunto:</b><br />");
            //Adiciona texto digitado no TextBox txtAssunto
            sbBody.Append("BEM VINDO AO ALLAN'S PORTFOLIO");
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Mensagem:</b><br />");
            //Adiciona texto digitado no TextBox txtMensagem
            sbBody.Append(mensagem);
            sbBody.Append("<br /><br />");
            sbBody.Append("<br /></body></html>");

            MailMessage message = new MailMessage();
            message.BodyEncoding = Encoding.UTF8;
            message.Sender = new MailAddress("naoresponder@allanfreitas.net", "Allan's Portfolio");
            message.From = new MailAddress("naoresponder@allanfreitas.net", "Allan's Portfolio");
            message.To.Add(new MailAddress(usuario.email, usuario.nome));
            message.Subject = "BEM VINDO AO ALLAN'S PORTFOLIO";
            message.Body = sbBody.ToString();
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;

            cliente.Send(message);
        }
    }
}