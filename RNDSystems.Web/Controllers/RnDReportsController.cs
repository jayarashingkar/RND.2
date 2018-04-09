using Newtonsoft.Json;
using RNDSystems.Models;
using RNDSystems.Models.ViewModels;
using RNDSystems.Models.ReportsViewModel;
using RNDSystems.Web.ViewModels;
using RNDSystems.Models.TestViewModels;
using RNDSystems.Models.ManualViewModels;
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
    public class RnDReportsController : BaseController
    {
        // GET: RnDReports

        #region Reports 
        //public ActionResult Reports(string WorkStudyID)     
        //{
        //    _logger.Debug("Reports");
        //    List<SelectListItem> ddlWorkStudyID = null;
        //    List<SelectListItem> ddTestType = null;
        //    bool isSuccess = false;
        //    ReportsViewModel reports = null;           
        //    try
        //    {
        //        ddlWorkStudyID = new List<SelectListItem>();
        //        ddTestType = new List<SelectListItem>();
        //        reports = new ReportsViewModel();

        //        var client = GetHttpClient();
        //        //  if (WorkStudyID == null)
        //        //  {       
        //        // var task = client.GetAsync(Api + "api/reports?recID=0&WorkStudyID=none").ContinueWith((res) =>
        //        if (WorkStudyID == null)
        //        {                   
        //            WorkStudyID = "none";
        //        }
        //        var task = client.GetAsync(Api + "api/reports?recID=0&WorkStudyID=" + WorkStudyID).ContinueWith((res) =>
        //        {
        //            if (res.Result.IsSuccessStatusCode)
        //            {
        //                isSuccess = true;
        //                reports = JsonConvert.DeserializeObject<ReportsViewModel>(res.Result.Content.ReadAsStringAsync().Result);
        //                if (reports != null)
        //                {
        //                    ddlWorkStudyID = reports.ddWorkStudyID;
        //                    ddTestType = reports.ddTestType;
        //                }
        //            }
        //        });

        //        task.Wait();                   

        //        if (WorkStudyID == "none")
        //        {
        //            ViewBag.ddlWorkStudyID = ddlWorkStudyID;
        //            ViewBag.ddTestType = ddTestType;
        //            return View(reports);

        //        }
        //        else
        //            return Json(new { isSuccess = isSuccess, ddTestType = ddTestType }, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex);
        //        isSuccess = false;
        //    }
        //    return Json(new { isSuccess = isSuccess, ddTestType = ddTestType }, JsonRequestBehavior.AllowGet);
        //   // return View();
        //}
        #endregion
       // public ActionResult Reports(string TestType)
        public ActionResult Reports()
        {
            _logger.Debug("Reports");
            List<SelectListItem> ddTestType = null;
            ReportsViewModel reports = null;
            try
            {
                ddTestType = new List<SelectListItem>();
                reports = new ReportsViewModel();

                var client = GetHttpClient();
               
                var task = client.GetAsync(Api + "api/reports?recID=0").ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        //isSuccess = true;
                        reports = JsonConvert.DeserializeObject<ReportsViewModel>(res.Result.Content.ReadAsStringAsync().Result);
                        if (reports != null)
                        {                            
                            ddTestType = reports.ddTestType;
                        }
                    }
                });

                task.Wait();
                ViewBag.ddTestType = ddTestType;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View(reports);
       }

        //public ActionResult GetReport(string WorkStudyID, string TestType) 
        //{
        //    // api/grid - api -getreports - in grid 
        //    // and send option of which testype report is needed 
        //    //switch (TestType.Trim())
        //    //{
        //    //    case 'Tension':

        //    //}
        //}



        public ActionResult TensionReportsList(string TestType)
        {
            ReportsViewModel model = new ReportsViewModel();

            model.TestType = "Tension";
            List<SelectListItem> ddlWorkStudyID = null;                 
            try
            {
                ddlWorkStudyID = new List<SelectListItem>();

                var client = GetHttpClient();
                var task = client.GetAsync(Api + "api/reports?recID=0&TestType=" + TestType).ContinueWith((res) =>            
                {
                if (res.Result.IsSuccessStatusCode)
                {

                    model = JsonConvert.DeserializeObject<ReportsViewModel>(res.Result.Content.ReadAsStringAsync().Result);
                    if (model != null)
                    {
                        ddlWorkStudyID = model.ddWorkStudyID;                        
                    }
                }
                });                
                task.Wait();
                ViewBag.ddlWorkStudyID = ddlWorkStudyID;
                ViewBag.TestType = TestType;
            }
            catch (Exception ex)
            {
                _logger.Error(ex); 
            }
            return View("TensionReportsList");
        }

        public ActionResult CompressionReportsList(string TestType)
        {
            ReportsViewModel model = new ReportsViewModel();

            model.TestType = "Compression";
            List<SelectListItem> ddlWorkStudyID = null;
            try
            {
                ddlWorkStudyID = new List<SelectListItem>();

                var client = GetHttpClient();
                var task = client.GetAsync(Api + "api/reports?recID=0&TestType=" + TestType).ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {

                        model = JsonConvert.DeserializeObject<ReportsViewModel>(res.Result.Content.ReadAsStringAsync().Result);
                        if (model != null)
                        {
                            ddlWorkStudyID = model.ddWorkStudyID;

                        }
                    }
                });
                task.Wait();
                ViewBag.ddlWorkStudyID = ddlWorkStudyID;
                ViewBag.TestType = TestType;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View("CompressionReportsList");
        }
        
         public ActionResult OpticalMountReportsList(string TestType)
        {
            ReportsViewModel model = new ReportsViewModel();

            model.TestType = "OpticalMount";
            List<SelectListItem> ddlWorkStudyID = null;
            try
            {
                ddlWorkStudyID = new List<SelectListItem>();

                var client = GetHttpClient();
                var task = client.GetAsync(Api + "api/reports?recID=0&TestType=" + TestType).ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {

                        model = JsonConvert.DeserializeObject<ReportsViewModel>(res.Result.Content.ReadAsStringAsync().Result);
                        if (model != null)
                        {
                            ddlWorkStudyID = model.ddWorkStudyID;
                        }
                    }
                });
                task.Wait();
                ViewBag.ddlWorkStudyID = ddlWorkStudyID;
                ViewBag.TestType = TestType;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View("OpticalMountReportsList");
        }
        // public ViewResult MacroEtchReportsList(string WorkStudyID)
        public ActionResult MacroEtchReportsList(string TestType)
        {
            // api/grid - api -getreports - in grid 
            // and send option of which testype report is needed 
            //switch (TestType.Trim())
            //{
            //    case 'Tension':

            //}
          //  MacroEtchViewModel model = new MacroEtchViewModel();
           // model.WorkStudyID = WorkStudyID;
           // return RedirectToAction("MacroEtchReportsList");
            return View("MacroEtchReportsList");
        }

        [HttpPost]

        // public ActionResult ExportToExcel(int test, string TestType , string SearchBy)
        //     public ActionResult ExportToExcel(DataGridoption ExportDataFilter)
        public void ExportToExcel(DataGridoption ExportDataFilter)
        {
             _logger.Debug("ExportToExcel");
 
           string TestType = ExportDataFilter.Screen;
            try
            {
                switch (TestType.Trim())
                {
                    case "Tension":                       
                         ExportTension(ExportDataFilter );
                        break;
                    case "Compression":
                        ExportCompression(ExportDataFilter);
                        break;
                    case "OpticalMount":
                        ExportOpticalMount(ExportDataFilter);
                        break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
           // return RedirectToAction("Reports");
       }

        private void ExportTension( DataGridoption ExportDataFilter)
        {
            var client = GetHttpClient();
              List<TensionViewModel> lstExportTension = new List<TensionViewModel>();

              DataSearch <TensionViewModel> objTension = null;
            var Tensiontask = client.PostAsJsonAsync(Api + "api/Grid", ExportDataFilter).ContinueWith((res) =>
            {
                if (res.Result.IsSuccessStatusCode)
                {
                    objTension = JsonConvert.DeserializeObject<DataSearch<TensionViewModel>>(res.Result.Content.ReadAsStringAsync().Result);
                }
            });
            Tensiontask.Wait();
            if (objTension != null && objTension.items != null && objTension.items.Count > 0)
            {
                lstExportTension = objTension.items;
                string fileName = "TensionReport" + "_" + DateTime.Now.ToString().Replace(" ", "").Replace("-", "").Replace(":", "");
                GetExcelFile<TensionViewModel>(lstExportTension, fileName, true);
            }         
        }

        private void ExportCompression(DataGridoption ExportDataFilter)
        {
            var client = GetHttpClient();
            List<CompressionViewModel> lstExportCompression = new List<CompressionViewModel>();
            DataSearch<CompressionViewModel> objCompression = null;
            var Compressiontask = client.PostAsJsonAsync(Api + "api/Grid", ExportDataFilter).ContinueWith((res) =>
            {
                if (res.Result.IsSuccessStatusCode)
                {
                    objCompression = JsonConvert.DeserializeObject<DataSearch<CompressionViewModel>>(res.Result.Content.ReadAsStringAsync().Result);
                }
            });
            Compressiontask.Wait();
            if (objCompression != null && objCompression.items != null && objCompression.items.Count > 0)
            {
                lstExportCompression = objCompression.items;
                string fileName = "Compression" + "_" + DateTime.Now.ToString().Replace(" ", "").Replace("-", "").Replace(":", "");
                GetExcelFile<CompressionViewModel>(lstExportCompression, fileName);
            }
        }
        private void ExportOpticalMount(DataGridoption ExportDataFilter)
        {
            var client = GetHttpClient();
            List<OpticalMountViewModel> lstExportOpticalMount = new List<OpticalMountViewModel>();
            DataSearch<OpticalMountViewModel> objOpticalMount = null;
            var OpticalMounttask = client.PostAsJsonAsync(Api + "api/Grid", ExportDataFilter).ContinueWith((res) =>
            {
                if (res.Result.IsSuccessStatusCode)
                {
                    objOpticalMount = JsonConvert.DeserializeObject<DataSearch<OpticalMountViewModel>>(res.Result.Content.ReadAsStringAsync().Result);
                }
            });
            OpticalMounttask.Wait();
            if (objOpticalMount != null && objOpticalMount.items != null && objOpticalMount.items.Count > 0)
            {
                lstExportOpticalMount = objOpticalMount.items;
                string fileName = "OpticalMount" + "_" + DateTime.Now.ToString().Replace(" ", "").Replace("-", "").Replace(":", "");
                GetExcelFile<OpticalMountViewModel>(lstExportOpticalMount, fileName);
            }
        }
    }
}