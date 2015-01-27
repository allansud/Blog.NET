using Blog.Models;
using Blog.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Blog.Security
{
    public class CustomMembership : MembershipProvider
    {

        private UsuarioRepositorio userRo = new UsuarioRepositorio();

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                Usuario user = userRo.GetUsuario(username, oldPassword);

                using (BlogContext context = new BlogContext())
                {
                    string sql = "UPDATE Usuario SET senha = @senha WHERE codigo = @codigo";
                    List<SqlParameter> parameterList = new List<SqlParameter>();
                    parameterList.Add(new SqlParameter("@codigo", user.codigo));
                    parameterList.Add(new SqlParameter("@senha", userRo.EncryptPassword(newPassword)));
                    SqlParameter[] parameters = parameterList.ToArray();
                    context.Database.ExecuteSqlCommand(sql, parameters);

                    context.SaveChanges();
                    UtilRepositorio.EnviarEmailTrocaSenha(user, newPassword, "SUA SENHA FOI ALTERADA COM SUCESSO!!!");
                    return true;
                }               
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            List<Usuario> usuarios = userRo.GetUsuarios();
            MembershipUserCollection users = new MembershipUserCollection();
            foreach (var user in usuarios)
            {
                MembershipUser memUser = new MembershipUser("CustomMembershipProvider",
                                                   user.email, user.codigo, user.email,
                                                   string.Empty, string.Empty,
                                                   true, false, DateTime.MinValue,
                                                   DateTime.MinValue,
                                                   DateTime.MinValue,
                                                   DateTime.Now, DateTime.Now);
                users.Add(memUser);
            }
            totalRecords = users.Count;
            return users;
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            //User userRep = new User();
            Usuario user = userRo.GetUsuario(username); // db.Usuario.Where(u => u.Email == username).SingleOrDefault();

            //userRep.GetAllUsers().SingleOrDefaultu => u.UserName == username);

            if (user != null)
            {
                MembershipUser memUser = new MembershipUser("CustomMembershipProvider",
                                               user.email, user.codigo, user.email,
                                               string.Empty, string.Empty,
                                               true, false, DateTime.MinValue,
                                               DateTime.MinValue,
                                               DateTime.MinValue,
                                               DateTime.Now, DateTime.Now);
                return memUser;
            }
            return null;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            int usuarioid = Int32.Parse(providerUserKey.ToString());
            Usuario user = userRo.GetUsuario(usuarioid);

            if (user != null)
            {
                MembershipUser memUser = new MembershipUser("CustomMembershipProvider",
                                               user.email, user.codigo, user.email,
                                               string.Empty, string.Empty,
                                               true, false, DateTime.MinValue,
                                               DateTime.MinValue,
                                               DateTime.MinValue,
                                               DateTime.Now, DateTime.Now);
                return memUser;
            }
            return null;
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            Usuario userObj = userRo.GetUsuario(username, password);
            if (userObj != null) 
            {
                HttpContext.Current.Items.Add("User", userObj);
                return true;
            }

            return false;
        }

        public MembershipUser CreateUser(string username, string password, string email, string roles, string passwordQuestion,
            string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            status = MembershipCreateStatus.Success;
            MembershipUser memUser = null;

            if (userRo.GetUsuario(username) != null)
                status = MembershipCreateStatus.DuplicateUserName;
            else if (userRo.GetUsuarioByEmail(email) != null)
                status = MembershipCreateStatus.DuplicateEmail;
            else
            {
                Usuario usuario = new Usuario();
                usuario.email = username;
                usuario.senha = userRo.EncryptPassword(password);
                usuario.email = email;

                userRo.AddUsuario(usuario, roles);

                if (usuario != null)
                {
                    memUser = new MembershipUser("CustomMembershipProvider",
                                    username, usuario.codigo, usuario.email,
                                    string.Empty, string.Empty,
                                    true, false, DateTime.MinValue,
                                    DateTime.MinValue,
                                    DateTime.MinValue,
                                    DateTime.Now, DateTime.Now);
                }
                else
                {
                    status = MembershipCreateStatus.UserRejected;
                }

            }
            return memUser;
        }
    }
}