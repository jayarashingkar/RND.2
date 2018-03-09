using Newtonsoft.Json;
using RNDSystems.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using RNDSystems.Common.Constants;
//using RNDSystems.Web.ViewModels;
//using System.Web.Mvc;

namespace RNDSystems.Web.Controllers
{
    [AllowAnonymous]
    public class RegisterController : BaseController
    {

        /// <summary>
        /// GET: Register User List
        /// </summary>
        /// <returns></returns>
        public ActionResult RegisterUserList()
        {
            RNDLogin NUR = null;

            List<SelectListItem> UserPermissionLevel = null;

            try
            {
                var client = GetHttpClient();
                var task = client.GetAsync(Api + "api/Register?UserId=" + 0).ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        NUR = JsonConvert.DeserializeObject<RNDLogin>(res.Result.Content.ReadAsStringAsync().Result);
                        if (NUR != null)
                        {
                            UserPermissionLevel = NUR.UserPermissionLevel;
                        }
                    }
                });
                task.Wait();

                ViewBag.ddlPermissionLevel = UserPermissionLevel;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View(NUR);
        }

        /// <summary>
        /// Retrieve Register user List details for Update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SaveRegisterUser(int id)
        {
            RNDLogin NUR = null;

            List<SelectListItem> UserPermissionLevel = null;

            try
            {
                var client = GetHttpClient();
                var task = client.GetAsync(Api + "api/Register?UserId=" + id).ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        NUR = JsonConvert.DeserializeObject<RNDLogin>(res.Result.Content.ReadAsStringAsync().Result);
                        if (NUR != null)
                        {
                            UserPermissionLevel = NUR.UserPermissionLevel;
                        }
                    }
                });
                task.Wait();

                ViewBag.ddlPermissionLevel = UserPermissionLevel;
                /*
                UserTypes.Add(PermissionConstants.Admin);
                UserTypes.Add(PermissionConstants.SuperAdmin);
                UserTypes.Add(PermissionConstants.NormalUser);
                UserTypes.Add(PermissionConstants.None);
                UserTypes.Add(PermissionConstants.ReadOnly);                   
                */
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View(NUR);
        }

        [HttpPost]
        public ActionResult Index(RNDLogin model)
        {
            return View(model);
        }

        /// <summary>
        /// Save or Update registered update List details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveRegisterUser(RNDLogin model)
        {
            var client = GetHttpClient();
            if (model.UserId == 0)
            {
                var task = client.PostAsJsonAsync(Api + "api/Register", model).ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        string Register = JsonConvert.DeserializeObject<string>(res.Result.Content.ReadAsStringAsync().Result);
                        if (Register != null)
                        {

                        }
                    }
                });
                task.Wait();
            }
            else
            {
                var task = client.PutAsJsonAsync(Api + "api/Register", model).ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        string Register = JsonConvert.DeserializeObject<string>(res.Result.Content.ReadAsStringAsync().Result);
                        if (Register != null)
                        {

                        }
                    }
                });
                task.Wait();
            }

            return RedirectToAction("RegisterUserList");
        }

    }
}