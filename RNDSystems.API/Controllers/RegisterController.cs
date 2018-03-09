using RNDSystems.API.SQLHelper;
using RNDSystems.Common.Constants;
using RNDSystems.Common.Utilities;
using RNDSystems.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
namespace RNDSystems.API.Controllers
{
    public class RegisterController : UnSecuredController
    {
        /// <summary>
        /// Retrieve the Registered User details
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(int UserId)
        {
            _logger.Debug("Register Get Called");
            SqlDataReader reader = null;
            RNDLogin NUR = null;
            try
            {
                CurrentUser user = ApiUser;
                NUR = new RNDLogin();
                AdoHelper ado = new AdoHelper();
                NUR.UserPermissionLevel = new List<SelectListItem>() { GetInitialSelectItem() };
                if (UserId > 0)
                {
                    SqlParameter param1 = new SqlParameter("@UserId", UserId);
                    using (reader = ado.ExecDataReaderProc("RNDRegisteredUser_ReadByID", "RND", new object[] { param1 }))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                NUR.UserId = Convert.ToInt32(reader["UserId"]);
                                NUR.UserName = Convert.ToString(reader["UserName"]).Trim();
                                NUR.FirstName = Convert.ToString(reader["FirstName"]).Trim();
                                NUR.LastName = Convert.ToString(reader["LastName"]).Trim();
                                NUR.PermissionLevel = Convert.ToString(reader["PermissionLevel"]).Trim();
                            }
                        }
                    }
                }
                using (reader = ado.ExecDataReaderProc("RNDUserPermissionLevel_READ", "RND", null))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            NUR.UserPermissionLevel.Add(new SelectListItem
                            {
                                Value = Convert.ToString(reader["PermissionLevel"]),
                                Text = Convert.ToString(reader["PermissionLevel"]),
                                Selected = (NUR.PermissionLevel == Convert.ToString(reader["PermissionLevel"])) ? true : false,
                            });
                        }
                    }
                }
                return Serializer.ReturnContent(NUR, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// Save the new register user details
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(RNDLogin login)
        {
            try
            {
                if (login != null)
                {
                    bool exists = CheckIfUserExists(login.UserName);
                    if (exists)
                        return Serializer.ReturnContent("UserName already exists.", this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
                    AdoHelper ado = new AdoHelper();
                    string strCurrentDate = DateTime.Now.ToString();
                    byte[] passwordSalt = Encryptor.EncryptText(strCurrentDate, login.UserName);
                    string se = Convert.ToBase64String(passwordSalt);
                    byte[] passwordHash = Encryptor.GenerateHash(login.Password, se.ToString());
                    login.IsSecurityApplied = true;
                    login.PasswordHash = passwordHash;
                    login.PasswordSalt = passwordSalt;
                    login.CreatedOn = DateTime.Now;
                    CurrentUser user = ApiUser;
                    int UserID = 1;
                    string DefaultStatus = "A";
                    SqlParameter param1 = new SqlParameter("@UserName", login.UserName);
                    SqlParameter param2 = new SqlParameter("@FirstName", login.FirstName);
                    SqlParameter param3 = new SqlParameter("@LastName", login.LastName);
                    SqlParameter param4 = new SqlParameter("@PasswordHash", login.PasswordHash);
                    SqlParameter param5 = new SqlParameter("@PasswordSalt", login.PasswordSalt);
                    //  SqlParameter param6 = new SqlParameter("@UserType", login.UserType);
                    SqlParameter param6 = new SqlParameter("@UserType", login.PermissionLevel);
                    SqlParameter param7 = new SqlParameter("@PermissionLevel", login.PermissionLevel);

                    if (user != null)
                    {
                        UserID = user.UserId;
                        DefaultStatus = "DR";
                    }
                    SqlParameter param8 = new SqlParameter("@CreatedBy", UserID);
                    SqlParameter param9 = new SqlParameter("@CreatedOn", DateTime.Now);
                    //   SqlParameter param10 = new SqlParameter("@StatusCode", "DR");
                    SqlParameter param10 = new SqlParameter("@StatusCode", DefaultStatus);
                    var id = ado.ExecScalarProc("RNDLogin_Insert", "RND", new object[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10 });
                    if (id != null)
                    {
                        login.UserId = Convert.ToInt32(id);
                        return Serializer.ReturnContent(MessageConstants.UserRegistered, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Serializer.ReturnContent(HttpStatusCode.NotImplemented, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }
        /// <summary>
        /// Update the existing register user details
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(RNDLogin login)
        {
            try
            {
                if (login != null)
                {
                    AdoHelper ado = new AdoHelper();
                    CurrentUser user = ApiUser;
                    SqlParameter param1 = new SqlParameter("@UserId", login.UserId);
                    SqlParameter param2 = new SqlParameter("@FirstName", login.FirstName);
                    SqlParameter param3 = new SqlParameter("@LastName", login.LastName);
                    SqlParameter param4 = new SqlParameter("@UserType", login.UserType);
                    SqlParameter param5 = new SqlParameter("@PermissionLevel", login.PermissionLevel);
                    //  SqlParameter param1 = new SqlParameter("@UserName", login.UserName);
                    //  SqlParameter param2 = new SqlParameter("@FirstName", login.FirstName);
                    //  SqlParameter param3 = new SqlParameter("@LastName", login.LastName);
                    //  SqlParameter param4 = new SqlParameter("@PasswordHash", login.PasswordHash);
                    //  SqlParameter param5 = new SqlParameter("@PasswordSalt", login.PasswordSalt);
                    //  SqlParameter param6 = new SqlParameter("@UserType", login.UserType);
                    //  SqlParameter param7 = new SqlParameter("@PermissionLevel", login.PermissionLevel);
                    //  SqlParameter param8 = new SqlParameter("@CreatedBy", user.UserId);
                    //  SqlParameter param9 = new SqlParameter("@CreatedOn", DateTime.Now);
                    //  SqlParameter param10 = new SqlParameter("@StatusCode", "DR");
                    var id = ado.ExecScalarProc("RNDLogin_Update", "RND", new object[] { param1, param2, param3, param4, param5 });
                    return Serializer.ReturnContent(MessageConstants.UserRegistered, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Serializer.ReturnContent(HttpStatusCode.NotImplemented, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }
        /// <summary>
        /// Delete the register user details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                SqlParameter param1 = new SqlParameter("@UserId", id);
                ado.ExecScalarProc("RNDLogin_Delete", "RND", new object[] { param1 });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Serializer.ReturnContent(HttpStatusCode.OK, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }
        private bool CheckIfUserExists(string userName)
        {
            bool result = false;
            AdoHelper ado = new AdoHelper();
            SqlParameter param1 = new SqlParameter("@UserName", userName);
            try
            {
                var id = ado.ExecScalarProc("RNDCheckUserExists", "RND", new object[] { param1 });
                if (id != null)
                    if (Convert.ToInt32(id) > 0)
                        result = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return result;
        }
    }
}
