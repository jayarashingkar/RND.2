using System.Web.Mvc;

namespace RNDSystems.Web.Controllers
{
    public class LogOutController : BaseController
    {
        /// <summary>
        /// Logout Index
        /// </summary>
        /// <returns></returns>
        // GET: LogOut
        public ActionResult Index()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}