using RNDSystems.API.SQLHelper;
using RNDSystems.Common.Constants;
using RNDSystems.Common.Utilities;
using RNDSystems.Models;
using RNDSystems.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace RNDSystems.API.Controllers
{
    public class LoginController : UnSecuredController
    {
        /// <summary>
        /// Login validate the registered user details
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(string userName)
        {
            _logger.Debug("Login Get called");
            SqlDataReader reader = null;
            RNDUserSecurityAnswer answer = null;
            try
            {
                CurrentUser user = ApiUser;
                answer = new RNDUserSecurityAnswer();
                AdoHelper ado = new AdoHelper();
                answer.RNDSecurityQuestions = new List<SelectListItem>() { GetInitialSelectItem() };
                if (!string.IsNullOrEmpty(userName))
                {
                    SqlParameter param1 = new SqlParameter("@UserName", userName);
                    using (reader = ado.ExecDataReaderProc("RNDUserSecurityAnswers_Read", "RND", new object[] { param1 }))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                answer.RNDSecurityQuestions.Add(new SelectListItem
                                {
                                    Value = Convert.ToString(reader["RNDSecurityQuestionId"]),
                                    Text = Convert.ToString(reader["Question"]),
                                    Selected = true,
                                });
                                answer.RNDSecurityQuestionId = Convert.ToInt32(reader["RNDSecurityQuestionId"]);
                            }
                        }
                    }
                }
                return Serializer.ReturnContent(answer, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Retrieve the registered user details
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(RNDLogin login)
        {
            SqlDataReader reader = null;
            RNDLogin dbUser = null;
            ApiViewModel VM = null;
            try
            {
                VM = new ApiViewModel();
                if (login != null)
                {
                    AdoHelper ado = new AdoHelper();
                    SqlParameter param1 = new SqlParameter("@UserName", login.UserName);
                    using (reader = ado.ExecDataReaderProc("RNDLogin_ReadByID", "RND", new object[] { param1 }))
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            dbUser = new RNDLogin();
                            dbUser.UserId = Convert.ToInt32(reader["UserId"]);
                            dbUser.FirstName = Convert.ToString(reader["FirstName"]);
                            dbUser.LastName = Convert.ToString(reader["LastName"]);
                            dbUser.UserType = Convert.ToString(reader["UserType"]);
                            dbUser.PasswordHash = (byte[])reader["PasswordHash"];
                            dbUser.PasswordSalt = (byte[])reader["PasswordSalt"];
                            dbUser.PermissionLevel = Convert.ToString(reader["PermissionLevel"]);
                            dbUser.IssueDate = (!string.IsNullOrEmpty(reader["IssueDate"].ToString())) ? Convert.ToDateTime(reader["IssueDate"]) : (DateTime?)null;
                            dbUser.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                            dbUser.CreatedOn = Convert.ToDateTime(reader["CreatedOn"]);
                            dbUser.StatusCode = Convert.ToString(reader["StatusCode"]);
                            byte[] strSalt = dbUser.PasswordSalt;
                            string salt = Convert.ToBase64String(strSalt);
                            byte[] dbPasswordHash = dbUser.PasswordHash;
                            byte[] userPasswordHash = Encryptor.GenerateHash(login.Password, salt);
                            bool chkPassword = Encryptor.CompareByteArray(dbPasswordHash, userPasswordHash);
                            if (chkPassword)
                            {
                                string token = Guid.NewGuid().ToString("D") + Guid.NewGuid().ToString("D");
                                dbUser.UserName = login.UserName;
                                dbUser.Token = token;
                                VM.Custom = dbUser;

                                ado = new AdoHelper();
                                SqlParameter param3 = new SqlParameter("@UserId", dbUser.UserId);
                                SqlParameter param4 = new SqlParameter("@Token", token);
                                ado.ExecScalarProc("RNDSecurityTokens_Insert", "RND", new object[] { param3, param4 });
                            }
                            else
                                VM.Message = MessageConstants.InValidPassword;
                        }
                        else
                            VM.Message = MessageConstants.InvalidUser;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Serializer.ReturnContent(VM, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }

        public HttpResponseMessage Get(string UserName, string UserAnswer)
        {
            SqlDataReader reader = null;
            RNDLogin dbUser = null;
            ApiViewModel VM = null;
            try
            {
                VM = new ApiViewModel();
                if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(UserAnswer))
                {
                    AdoHelper ado = new AdoHelper();
                    SqlParameter param1 = new SqlParameter("@UserName", UserName);
                    SqlParameter param2 = new SqlParameter("@UserAnswer", UserAnswer);
                    using (reader = ado.ExecDataReaderProc("RNDResetPassword", "RND", new object[] { param1, param2 }))
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            dbUser = new RNDLogin();
                            dbUser.UserId = Convert.ToInt32(reader["UserId"]);
                            dbUser.FirstName = Convert.ToString(reader["FirstName"]);
                            dbUser.LastName = Convert.ToString(reader["LastName"]);
                            dbUser.UserType = Convert.ToString(reader["UserType"]);
                            dbUser.PermissionLevel = Convert.ToString(reader["PermissionLevel"]);
                            dbUser.IssueDate = (!string.IsNullOrEmpty(reader["IssueDate"].ToString())) ? Convert.ToDateTime(reader["IssueDate"]) : (DateTime?)null;
                            dbUser.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                            dbUser.CreatedOn = Convert.ToDateTime(reader["CreatedOn"]);
                            dbUser.StatusCode = Convert.ToString(reader["StatusCode"]);
                            VM.Custom = dbUser;
                            string token = Guid.NewGuid().ToString("D") + Guid.NewGuid().ToString("D");
                            dbUser.UserName = UserName;
                            dbUser.Token = token;
                            VM.Custom = dbUser;

                            ado = new AdoHelper();
                            SqlParameter param3 = new SqlParameter("@UserId", dbUser.UserId);
                            SqlParameter param4 = new SqlParameter("@Token", token);
                            ado.ExecScalarProc("RNDSecurityTokens_Insert", "RND", new object[] { param3, param4 });
                        }
                        else
                            VM.Message = MessageConstants.InvalidUser;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Serializer.ReturnContent(VM, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }

        /// <summary>
        /// Save or Update the user details.
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(RNDUserSecurityAnswer answer)
        {
            SqlDataReader reader = null;
            ApiViewModel VM = new ApiViewModel();
          //  RNDLogin dbUser = null;
            try
            {
                //    VM = new ApiViewModel();
                if (answer != null)
                {

                    int charaters = CommonConstants.PasswordLength;
                    string newPassword = answer.Password;// charaters.RandomString();
                    string strCurrentDate = DateTime.Now.ToString();

                    byte[] strSaltTemp = Encryptor.EncryptText(strCurrentDate, answer.UserName);
                    string se = Convert.ToBase64String(strSaltTemp);
                    byte[] strPasswordHash = Encryptor.GenerateHash(newPassword, se.ToString());

                    AdoHelper ado = new AdoHelper();
                    SqlParameter param1 = new SqlParameter("@UserName", answer.UserName);
                    SqlParameter param2 = new SqlParameter("@RNDSecurityQuestionId", answer.RNDSecurityQuestionId);
                    SqlParameter param3 = new SqlParameter("@SecurityAnswer", answer.SecurityAnswer);
                    SqlParameter param4 = new SqlParameter("@PasswordHash", strPasswordHash);
                    SqlParameter param5 = new SqlParameter("@PasswordSalt", strSaltTemp);

                    using (reader = ado.ExecDataReaderProc("RNDUserPasswordReset", "RND", new object[] { param1, param2, param3, param4, param5 }))
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            int result = Convert.ToInt32(reader[0]);
                            string userStatus = reader[1].ToString();

                            if (result == 0)
                            {
                                VM.Message = "Wrong Security Answer.";
                            }
                            else
                            {
                                VM.Custom = new RNDLogin { Password = newPassword, StatusCode=userStatus };
                            }
                        }
                        else
                            VM.Message = MessageConstants.InvalidUser;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Serializer.ReturnContent(VM, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }

        /// <summary>
        /// Retrieve the logged in user security token
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        //public HttpResponseMessage GetUserDetails(string Token)
        //{
        //    SqlDataReader reader = null;
        //    CurrentUser dbCUser = null;
        //    ApiViewModel VM = null;
        //    try
        //    {
        //        VM = new ApiViewModel();
        //        if (!string.IsNullOrEmpty(Token))
        //        {
        //            AdoHelper ado = new AdoHelper();
        //            SqlParameter param1 = new SqlParameter("@Token", Token);
        //            using (reader = ado.ExecDataReaderProc("RNDGetUser_ReadByID", new object[] { param1 }))
        //            {
        //                if (reader.HasRows && reader.Read())
        //                {
        //                    dbCUser = new CurrentUser();
        //                    dbCUser.UserId = Convert.ToInt32(reader["UserId"]);
        //                    dbCUser.UserName = Convert.ToString(reader["UserName"]);
        //                    dbCUser.FullName = Convert.ToString(reader["FullName"]);
        //                }
        //                else
        //                    VM.Message = MessageConstants.InvalidUser;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message);
        //        return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        //    }
        //    return Serializer.ReturnContent(VM, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        //}
    }
}
