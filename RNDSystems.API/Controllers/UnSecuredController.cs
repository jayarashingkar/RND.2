using log4net;
using Newtonsoft.Json;
using RNDSystems.Models;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace RNDSystems.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UnSecuredController : ApiController
    {
        #region Log4net

        public ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        private CurrentUser currentUser;
        public CurrentUser ApiUser
        {
            get
            {
                try
                {
                    var user = HttpContext.Current.Request.Headers.Get("User");
                    if (user != null)
                    {
                        currentUser = JsonConvert.DeserializeObject<CurrentUser>(user.ToString());
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return currentUser;
            }
        }

        protected static SelectListItem GetInitialSelectItem()
        {
            return new SelectListItem { Text = "Please Select", Value = "-1" };
        }
    }
}
