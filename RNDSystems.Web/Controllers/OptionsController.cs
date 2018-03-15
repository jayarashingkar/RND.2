using Newtonsoft.Json;
using RNDSystems.Models;

using RNDSystems.Common.Constants;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using RNDSystems.Models.ViewModels;

namespace RNDSystems.Web.Controllers
{
    public class OptionsController : BaseController
    {
        // GET: Options
        public ActionResult SetOptions()
        {
            _logger.Debug("SetOptions");

            string[] strOptions = {"Study Type", "Location", "Location1", "Database", "TestLab"};
            List<SelectListItem> ddOptionList = new List<SelectListItem>();
            try
            {
                int intRowId = 0;
                string strValue = string.Empty;
                while (intRowId < strOptions.Length)
                {
                    strValue = strOptions[intRowId];
                    ddOptionList.Add(new SelectListItem
                    {
                        Value = strValue,
                        Text = strValue,
                       // Selected = (testingOrientation == Convert.ToString(strValue)) ? true : false,
                    });     
                    intRowId += 1;
                }
                ViewBag.ddOptionList = ddOptionList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View();
        }

        public ActionResult OptionView(int id, string optionType)
        {
            OptionsViewModel model = new OptionsViewModel();
            model.optionType = optionType;
            model.RecId = id;
            return PartialView(model);
        }

        public ActionResult StudyTypeOptionView(int id, string optionType)
        {
            OptionsViewModel model = new OptionsViewModel();
            model.optionType = optionType;
            model.RecId = id;
            return PartialView(model);
        }

        public ActionResult LocationView(int id, string optionType)
        {
            OptionsViewModel model = new OptionsViewModel();
            model.optionType = optionType;
            model.RecId = id;
            return PartialView(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult StudyType( string action, OptionsViewModel model)
        {
            var client = GetHttpClient();
            OptionsViewModel options;
            try
            {
                //add = 1 , 
               if (action == CRUDConstants.Add)
                {
                    var task = client.PostAsJsonAsync(Api + "api/Options", model).ContinueWith((res) =>
                    {
                        if (res.Result.IsSuccessStatusCode)
                        {
                            options = JsonConvert.DeserializeObject<OptionsViewModel>(res.Result.Content.ReadAsStringAsync().Result);

                        }
                    });
                    task.Wait();
                }               
            }
           catch(Exception ex)
            {
                _logger.Error(ex);
            }
            return PartialView(model);
        }

    }
}