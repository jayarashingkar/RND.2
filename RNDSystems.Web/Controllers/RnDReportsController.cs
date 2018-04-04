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
        public ActionResult Reports(string WorkStudyID)
        {
            _logger.Debug("Reports");
            List<SelectListItem> ddlWorkStudyID = null;
            List<SelectListItem> ddTestType = null;
            bool isSuccess = false;
            ReportsViewModel reports = null;           
            try
            {
                ddlWorkStudyID = new List<SelectListItem>();
                ddTestType = new List<SelectListItem>();
                reports = new ReportsViewModel();

                var client = GetHttpClient();
                //  if (WorkStudyID == null)
                //  {       
                // var task = client.GetAsync(Api + "api/reports?recID=0&WorkStudyID=none").ContinueWith((res) =>
                if (WorkStudyID == null)
                {                   
                    WorkStudyID = "none";
                }
                var task = client.GetAsync(Api + "api/reports?recID=0&WorkStudyID=" + WorkStudyID).ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                        reports = JsonConvert.DeserializeObject<ReportsViewModel>(res.Result.Content.ReadAsStringAsync().Result);
                        if (reports != null)
                        {
                            ddlWorkStudyID = reports.ddWorkStudyID;
                            ddTestType = reports.ddTestType;
                        }
                    }
                });

                task.Wait();                   
             
                if (WorkStudyID == "none")
                {
                    ViewBag.ddlWorkStudyID = ddlWorkStudyID;
                    ViewBag.ddTestType = ddTestType;
                    return View(reports);

                }
                else
                    return Json(new { isSuccess = isSuccess, ddTestType = ddTestType }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                isSuccess = false;
            }
            return Json(new { isSuccess = isSuccess, ddTestType = ddTestType }, JsonRequestBehavior.AllowGet);
           // return View();
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
     
            [HttpGet]
        public ActionResult TensionReportsList(string WorkStudyID)
        {
            // api/grid - api -getreports - in grid 
            // and send option of which testype report is needed 
            //switch (TestType.Trim())
            //{
            //    case 'Tension':

            //}
            TensionViewModel model = new TensionViewModel();
            model.WorkStudyID = WorkStudyID;
            //  return RedirectToAction("TensionReportsList", new { RecID =0, WorkStudyID = model.WorkStudyID });

            // return View("TensionReportsList", model);
            return View(model);
        }

        public ActionResult CompressionReportslList(string WorkStudyID)
        {
            // api/grid - api -getreports - in grid 
            // and send option of which testype report is needed 
            //switch (TestType.Trim())
            //{
            //    case 'Tension':

            //}
            CompressionViewModel model = new CompressionViewModel();
            model.WorkStudyID = WorkStudyID;
            return View(model);
        }

        public ActionResult OpticalMountReportslList(string WorkStudyID)
        {
            // api/grid - api -getreports - in grid 
            // and send option of which testype report is needed 
            //switch (TestType.Trim())
            //{
            //    case 'Tension':

            //}
            OpticalMountViewModel model = new OpticalMountViewModel();
            model.WorkStudyID = WorkStudyID;
            return View(model);
        }

        [HttpPost]
        public ActionResult ExportToExcel(string ddlWorkStudyID, string ddTestType, string searchFromDate, string searchToDate)
        {
            //Remove later - for testing purpose only
            ddTestType = "Tension";

            _logger.Debug("WorkSutdyList ExportToExcel");
            string SearchBy = "";
            DataGridoption ExportDataFilter = new DataGridoption();

            if (!string.IsNullOrEmpty(ddlWorkStudyID))
            {
                SearchBy = SearchBy + ";" + "WorkStudyID:" + ddlWorkStudyID;
            }

            if (!string.IsNullOrEmpty(ddTestType))
            {
                SearchBy = SearchBy + ";" + "TestType:" + ddTestType;
            }

            if (!string.IsNullOrEmpty(searchFromDate))
            {
                SearchBy = SearchBy + ";" + "searchFromDate:" + searchFromDate;
            }

            if (!string.IsNullOrEmpty(searchToDate))
            {
                SearchBy = SearchBy + ";" + "searchToDate:" + searchToDate;
            }

            ExportDataFilter.Screen = "Reports";
            ExportDataFilter.filterBy = "all";
            ExportDataFilter.pageIndex = 0;
            ExportDataFilter.pageSize = 10000;
            ExportDataFilter.searchBy = SearchBy;

            List<ReportsViewModel> lstExportReports = new List<ReportsViewModel>();
            DataSearch<ReportsViewModel> objReports = null;

            try
            {
                var client = GetHttpClient();
                var task = client.PostAsJsonAsync(Api + "api/Grid", ExportDataFilter).ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        objReports = JsonConvert.DeserializeObject<DataSearch<ReportsViewModel>>(res.Result.Content.ReadAsStringAsync().Result);
                    }
                });

                task.Wait();

                if (objReports != null && objReports.items != null && objReports.items.Count > 0)
                {
                    lstExportReports = objReports.items;
                    string fileName = "Reports" + "_" + DateTime.Now.ToString().Replace(" ", "").Replace("-", "").Replace(":", "");
                    GetExcelFile<ReportsViewModel>(lstExportReports, fileName);
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return RedirectToAction("Reports");
        }

    }
}