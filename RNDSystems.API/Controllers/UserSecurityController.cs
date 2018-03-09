using RNDSystems.API.SQLHelper;
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
    public class UserSecurityController : UnSecuredController
    {

        /// <summary>
        /// Retrieve the Registered user security quetion details
        /// </summary>
        /// <param name="recID"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(int recID)
        {
            _logger.Debug("UserSecurity Get called");
            SqlDataReader reader = null;
            RNDUserSecurityAnswer security = null;
            try
            {
                CurrentUser user = ApiUser;
                security = new RNDUserSecurityAnswer();
                AdoHelper ado = new AdoHelper();
                security.RNDSecurityQuestions = new List<SelectListItem>() { GetInitialSelectItem() };
                if (recID > 0)
                {

                }
                else
                {
                    using (reader = ado.ExecDataReaderProc("RNDSecurityQuestions_READ", "RND", null))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                security.RNDSecurityQuestions.Add(new SelectListItem
                                {
                                    Value = Convert.ToString(reader["RNDSecurityQuestionId"]),
                                    Text = Convert.ToString(reader["Question"]),
                                });
                            }
                        }
                    }
                }
                security.UserName = ApiUser.UserName;
                return Serializer.ReturnContent(security, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }


        ///// <summary>
        ///// Retrieve the Registered user security quetion details
        ///// </summary>
        ///// <param name="recID"></param>
        ///// <returns></returns>
        //public HttpResponseMessage Get(int recID)
        //{
        //    _logger.Debug("UserSecurity Get called");
        //    SqlDataReader reader = null;
        //    RNDUserSecurityAnswer security = null;
        //    try
        //    {
        //        CurrentUser user = ApiUser;
        //        security = new RNDUserSecurityAnswer();
        //        AdoHelper ado = new AdoHelper();
        //        security.RNDSecurityQuestions = new List<SelectListItem>() { GetInitialSelectItem() };
        //        if (recID > 0)
        //        {

        //        }
        //        using (reader = ado.ExecDataReaderProc("RNDSecurityQuestions_READ", "RND", null))
        //        {
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    security.RNDSecurityQuestions.Add(new SelectListItem
        //                    {
        //                        Value = Convert.ToString(reader["RNDSecurityQuestionId"]),
        //                        Text = Convert.ToString(reader["Question"]),
        //                    });
        //                }
        //            }
        //        }
        //        return Serializer.ReturnContent(security, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message);
        //        return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        //    }
        //}

        /// <summary>
        /// Save the Registered user security quetion details
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(RNDUserSecurityAnswer answer)
        {
            string data = string.Empty;
            try
            {
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                SqlParameter param1 = new SqlParameter("@RNDLoginId", user.UserId);
                SqlParameter param2 = new SqlParameter("@RNDSecurityQuestionId", answer.RNDSecurityQuestionId);
                //
                SqlParameter param3 = new SqlParameter("@SecurityAnswer", answer.SecurityAnswer);
                SqlParameter param4 = new SqlParameter("@CreatedBy", user.UserId);


                string strCurrentDate = DateTime.Now.ToString();
                byte[] passwordSalt = Encryptor.EncryptText(strCurrentDate, user.UserName);
                string se = Convert.ToBase64String(passwordSalt);
                byte[] passwordHash = Encryptor.GenerateHash(answer.Password, se.ToString());

                SqlParameter param5 = new SqlParameter("@PasswordHash", passwordHash);
                SqlParameter param6 = new SqlParameter("@PasswordSalt", passwordSalt);

                var id = ado.ExecScalarProc("RNDUserSecurityAnswers_Insert", "RND", new object[] { param1, param2, param3, param4, param5, param6 });
                if (id != null)
                {
                    answer.RNDUserSecurityAnswerId = Convert.ToInt32(id);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Serializer.ReturnContent(answer, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }
    }
}
