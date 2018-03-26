using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RNDSystems.Models.ManualViewModels;

namespace RNDSystems.Web.Controllers
{
    public class ResultsController : BaseController
    {
        public ActionResult AddResults(bool isSuccess, string SelectedTests)
        {
            ManualDataViewModel data = new ManualDataViewModel();
            isSuccess = false;
            List<SelectListItem> ddTestType = null;
            try
            {
                ddTestType = new List<SelectListItem>();
                var client = GetHttpClient();
                var task = client.GetAsync(Api + "api/ImportData?Active=3").ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        data = JsonConvert.DeserializeObject<ManualDataViewModel>(res.Result.Content.ReadAsStringAsync().Result);
                        if (data != null)
                        {
                            ddTestType = data.ddTestType;
                            //ddWorkStudyId = data.ddWorkStudyID;
                        }
                    }
                });

                task.Wait();

                ViewBag.ddTestTypesManual = ddTestType;
                //data.WorkStudyID = WorkStudyID;
                data.SelectedTests = SelectedTests;

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            //return Json(new { isSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
            return View(data);
        }



        // GET: Results
        //public ActionResult SCCResult()
        //{
        //    SCCViewModel scc = new SCCViewModel();
        //    return View();
        //}
    }
}