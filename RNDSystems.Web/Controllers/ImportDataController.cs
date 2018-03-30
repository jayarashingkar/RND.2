﻿using Newtonsoft.Json;
using RNDSystems.Models;
using RNDSystems.Models.ViewModels;
using RNDSystems.Web.ViewModels;
using System;
using System.Text;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace RNDSystems.Web.Controllers
{
    public class ImportDataController : BaseController
    {
        // GET: ImportData
        public ActionResult ImportData(string WorkStudyId)
        {
            _logger.Debug("ImportData");
            List<SelectListItem> ddTestTypes = null;
         //   List<SelectListItem> ddWorkStudyId = null;
            // List<SelectListItem> ddTestNos = null;
            // ImportDataViewModel data = null;
         //   string[] strTestTypes = { "Tension", "Compression", "Bearing", "Shear", "Notch Yield", "Residual Strength", "Fracture Toughness", "Modulus Tension", "Modulus Compression", "Fatigue Testing"};
           // string strWorkStudyId = "Currently Unavailable" ;

            try
            {
                if (WorkStudyId == null)
                {
                    ddTestTypes = new List<SelectListItem>();
                  //  ddWorkStudyId = new List<SelectListItem>();

                    ImportDataViewModel data = new ImportDataViewModel();

                    var client = GetHttpClient();
                    //   var task = client.GetAsync(Api + "api/ImportData?WorkStudyId=none").ContinueWith((res) =>
                    var task = client.GetAsync(Api + "api/ImportData?Active=2").ContinueWith((res) =>
                    {
                        if (res.Result.IsSuccessStatusCode)
                        {
                            data = JsonConvert.DeserializeObject<ImportDataViewModel>(res.Result.Content.ReadAsStringAsync().Result);
                            if (data != null)
                            {
                                 ddTestTypes = data.ddTestType;
                                //ddWorkStudyId = data.ddWorkStudyID;
                            }
                        }
                    });
                    task.Wait();
                    ViewBag.ddTestTypes = ddTestTypes;


                    //ddWorkStudyId.Add(new SelectListItem
                    //{
                    //    Value = strWorkStudyId,
                    //    Text = strWorkStudyId,
                    //    Selected = (Convert.ToString(data.WorkStudyID) == Convert.ToString(strWorkStudyId)) ? true : false,
                    //});
                    //ViewBag.ddWorkStudyId = ddWorkStudyId;
                }
                else
                {
                    // populate ddTestNos by sending the WorkStudyId as parameter
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View();
        }

    }
}