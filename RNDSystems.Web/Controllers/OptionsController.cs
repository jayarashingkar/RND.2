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
            OptionsViewModel optionModel = new OptionsViewModel();
            return View(optionModel);

        }
        //public ActionResult SetOptions()
        //{
        //    _logger.Debug("SetOptions");

        //    // string[] strOptions = {"Study Type", "Location", "Location1", "Database", "TestLab"};
        //    //string[] strOptions = { "Study Type", "Location", "Location1", "Database", "TestLab" };
        //    List<SelectListItem> ddStudyTypeList = new List<SelectListItem>();
        //    List<SelectListItem> ddLocationList = new List<SelectListItem>();
        //    OptionsViewModel optionModel = new OptionsViewModel();
        //    try
        //    {
        //        var client = GetHttpClient();
        //        var task = client.GetAsync(Api + "api/Options?recID=0&optionType=none").ContinueWith((res) =>
        //            {
        //                if (res.Result.IsSuccessStatusCode)
        //                {
        //                    optionModel = JsonConvert.DeserializeObject<OptionsViewModel>(res.Result.Content.ReadAsStringAsync().Result);
        //                    if (optionModel != null)
        //                    {
        //                        ddStudyTypeList = optionModel.StudyTypeList;
        //                        ddLocationList = optionModel.LocationList;
        //                    }
        //                }
        //            });
        //            task.Wait();
        //            ViewBag.ddStudyTypeList = ddStudyTypeList;
        //            ViewBag.ddLocationList = ddLocationList;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex);
        //    }
        //    return View(optionModel);
        //}  

    }
}