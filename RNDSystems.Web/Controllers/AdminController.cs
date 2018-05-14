using Newtonsoft.Json;
using RNDSystems.Models;
using RNDSystems.Models.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using System.Web;
using System;

namespace RNDSystems.Web.Controllers
{
    public class AdminController : BaseController
    {
        #region SecuityConfig

        [AllowAnonymous]
        public ActionResult SecuityConfig()
        {
            bool IsSecurityApplied = LoggedInUser.IsSecurityApplied;
            RNDUserSecurityAnswer rndUserSecurityAnswer = null;
            // store list of Security question in  ViewBag.ddSecurityQuestions to access from Front End

            //IEnumerable<LMSSecurityQuestion> securityQuestions = unitofwork.LMSSecurityQuestionRepository.Get(x => x.StatusCode == StatusCodeConstants.Active);
            //List<SelectListItem> ddSecurityQuestions = new List<SelectListItem> { new SelectListItem { Text = "Please Select", Value = "-1" } };
            //securityQuestions.ToList().ForEach(x => ddSecurityQuestions.Add(new SelectListItem { Text = x.Question, Value = x.LMSSecurityQuestionId.ToString() }));

            List<SelectListItem> ddSecurityQuestions = null;
            try
            {
                var client = GetHttpClient();
                var task = client.GetAsync(Api + "api/UserSecurity?recID=0").ContinueWith((res) =>
               {
                   if (res.Result.IsSuccessStatusCode)
                   {
                      // RNDUserSecurityAnswer 
                       rndUserSecurityAnswer = JsonConvert.DeserializeObject<RNDUserSecurityAnswer>(res.Result.Content.ReadAsStringAsync().Result);
                       if (rndUserSecurityAnswer != null)
                       {
                           ddSecurityQuestions = rndUserSecurityAnswer.RNDSecurityQuestions;                          
                           rndUserSecurityAnswer.IsSecurityApplied = IsSecurityApplied;
                       }
                   }
               });
                task.Wait();             
                // ViewBag.UserName = UserName;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            ViewBag.ddSecurityQuestions = ddSecurityQuestions;

            // returns if the Security question is assigned to the Logged in user.
            // return View(IsSecurityApplied);
          
            return View(rndUserSecurityAnswer);
          
        }

        [HttpPost]
        public ActionResult SecuityConfig(RNDUserSecurityAnswer model)
        {        
            string message = "";
            bool isSuccess = false;
            try
            {
                //start here               
                var client = GetHttpClient();
                var task = client.PutAsJsonAsync(Api + "api/login", model).ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        ApiViewModel VM = JsonConvert.DeserializeObject<ApiViewModel>(res.Result.Content.ReadAsStringAsync().Result);
                        if (VM != null)
                        {
                            if (!string.IsNullOrEmpty(VM.Message))
                                message = VM.Message;
                            else if (VM.Custom != null)
                            {
                                message = VM.Custom.Password;
                                isSuccess = true;
                                LoggedInUser.IsSecurityApplied = true;
                            }
                            if (isSuccess && this.HttpContext.Session["CurrentUser"] != null)
                            {
                                CurrentUser currentUser = (CurrentUser)this.HttpContext.Session["CurrentUser"];
                                currentUser.StatusCode = VM.Custom.StatusCode;
                                this.HttpContext.Session["CurrentUser"] = currentUser;
                            }
                        }
                    }
                });
                task.Wait();
               
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
                  
            return RedirectToAction("WorkSutdyList", "WorkStudy");
        }
          

        #endregion
        //GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}