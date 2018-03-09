using log4net;
using RNDSystems.Models;
using RNDSystems.Web.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RNDSystems.Web.Controllers
{
    [RNDAuthActionFilter]
    public class BaseController : Controller
    {
        #region Log4net

        public ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion


        public static string Api = string.Empty;
        static BaseController()
        {
            Api = ConfigurationManager.AppSettings["Api"];
        }

        // GET: Base
        /// <summary>
        /// Retrieve logged in User details
        /// </summary>
        public CurrentUser LoggedInUser
        {
            /* 
             * comment here
             * This method is used in GetHttpClient()
             * Reterive the logged in user details from Session
             * Returns current User that is logged in that is stored in session
            */
            get
            {
                CurrentUser currentUser = null;
                if (Session["CurrentUser"] != null)
                {
                    currentUser = (CurrentUser)Session["CurrentUser"];
                }
                return currentUser;
            }

        }

        /// <summary>
        /// GetHttpClient connect the api with token
        /// </summary>
        /// <returns></returns>
        public HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (LoggedInUser != null)
                client.DefaultRequestHeaders.Add("Token", LoggedInUser.Token);
            return client;
        }

        //public void GetExcelFile<T>(int i, IEnumerable<T> dataSource, string fileName)
        //{
        //    GridView gridview = new GridView();
        //    gridview.DataSource = dataSource;
        //    gridview.DataBind();
        //    Response.Clear();
        //    Response.ClearContent();
        //    Response.Buffer = true;
        //    Response.ContentType = "application/ms-excel";
        //    Response.AppendHeader("content-disposition", "attachment; filename=" + fileName + ".xls");
        //    Response.Charset = "";
        //    StringWriter objStringWriter = new StringWriter();
        //    HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
        //    gridview.RenderControl(objHtmlTextWriter);
        //    Response.Output.Write(objStringWriter.ToString());
        //    Response.Flush();
        //    Response.End();
        //    return View();
        //}

        public ActionResult GetExcelFile<T>(IEnumerable<T> dataSource,string fileName)
        {
            GridView gridview = new GridView();            
            gridview.DataSource = dataSource;
            gridview.DataBind();
            Response.Clear();             
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename="+fileName+".xls");
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gridview.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());          
            Response.Flush();
            Response.End();
            return View();
        }
    }
}