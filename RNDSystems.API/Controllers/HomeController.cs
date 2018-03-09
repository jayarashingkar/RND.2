using log4net;
using RNDSystems.Common.Constants;
using RNDSystems.Models;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace RNDSystems.API.Controllers
{
    public class HomeController : Controller
    {
        #region Log4net

        public ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            try
            {
                RNDLogin rndLogin = new RNDLogin
                {
                    UserName = "admin1",
                    Password = "admin@123",
                    FirstName = "admin",
                    LastName = "admin",
                    CreatedBy = 1,
                    StatusCode = "A",                   
                    PermissionLevel = PermissionConstants.SuperAdmin,
                    UserType = PermissionConstants.SuperAdmin,
                };
                var config = new HttpConfiguration();
                WebApiConfig.Register(config);
                var server = new HttpServer(config);
                var client = new HttpClient(server);
                var response = client.PostAsJsonAsync(Request.Url.AbsoluteUri + "api/Register", rndLogin).Result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
            return View();
        }
    }
}
