using log4net;
using Newtonsoft.Json;
using RNDSystems.API.SQLHelper;
using RNDSystems.Models;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using System.Web.Http.Filters;

namespace RNDSystems.API.Filters
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RNDSystemApiAuthorizationAttribute: AuthorizationFilterAttribute
    {
        #region Log4net

        public ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion
        /// <summary>
        /// Validate the Authorization details
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //base.OnAuthorization(actionContext);
            _logger.Debug("OnAuthorization called");
            bool isValid = false;
            string hostName = string.Empty;
            string controllerName = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            string verbName = actionContext.Request.Method.ToString();
            hostName = actionContext.Request.Headers.Host;
            if (controllerName == "Login" || controllerName == "Home")
            {
                //To Skip validation
                return;
            }
            isValid = AssignTenant(hostName);
            if (!isValid)
            {
                //_logger.Warn("Not Authorized");
                var unAuthorizeMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                unAuthorizeMessage.Headers.Add("IsAuthorized", "False");
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                return;
            }
        }

        /// <summary>
        /// Validate the Hostname
        /// </summary>
        /// <param name="hostName"></param>
        /// <returns></returns>
        private bool AssignTenant(string hostName)
        {
            bool isValid = false;
            try
            {
                if (!string.IsNullOrEmpty(hostName))
                {
                    var token = HttpContext.Current.Request.Headers.Get("Token");
                    if (token != null)
                    {
                        //Map the token with database token to get user details
                        //Sample                        
                        SqlDataReader reader = null;
                        CurrentUser dbCUser = null;
                        AdoHelper ado = new AdoHelper();
                        SqlParameter param1 = new SqlParameter("@Token", token);
                        using (reader = ado.ExecDataReaderProc("RNDGetUser_ReadByID", "RND", new object[] { param1 }))
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                dbCUser = new CurrentUser();
                                dbCUser.UserId = Convert.ToInt32(reader["UserId"]);
                                dbCUser.UserName = Convert.ToString(reader["UserName"]);
                                dbCUser.FullName = Convert.ToString(reader["FullName"]);
                            }                           
                        }
                        //var user = new CurrentUser { UserId = 1, UserName = "User 1", FullName = "Test User" };
                        if (dbCUser != null)
                        {
                            string data = JsonConvert.SerializeObject(dbCUser);
                            HttpContext.Current.Request.Headers.Remove("User");
                            HttpContext.Current.Request.Headers.Add("User", data);
                            isValid = true;
                        }
                        else
                        {
                            //dbCUser = new CurrentUser { UserId = 1, UserName = "User 1", FullName = "Test User" };
                            //string data = JsonConvert.SerializeObject(dbCUser);
                            //HttpContext.Current.Request.Headers.Remove("User");
                            //HttpContext.Current.Request.Headers.Add("User", data);
                            //isValid = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Er" + ex.Message);
            }
            return isValid;
        }


    }
}