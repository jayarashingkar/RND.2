using Newtonsoft.Json;
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
    public class RnDReportsController : BaseController
    {
        // GET: RnDReports
        public ActionResult Reports(string WorkStudyID)
        {
            _logger.Debug("Reports");
            List<SelectListItem> ddlWorkStudyID = null;
            List<SelectListItem> ddTestType = null;

            RNDReports reports = null;           
            try
            {
                ddlWorkStudyID = new List<SelectListItem>();
                ddTestType = new List<SelectListItem>();
                reports = new RNDReports();

                var client = GetHttpClient();
                if (WorkStudyID == null)
                {       
                    var task = client.GetAsync(Api + "api/reports?recID=0&WorkStudyID=''").ContinueWith((res) =>
                    {
                        if (res.Result.IsSuccessStatusCode)
                        {
                            reports = JsonConvert.DeserializeObject<RNDReports>(res.Result.Content.ReadAsStringAsync().Result);
                            if (reports != null)
                            {
                                ddlWorkStudyID = reports.ddWorkStudyID;
                                ddTestType = reports.ddTestType;
                            }
                        }                       
                    });                   
                   task.Wait();                    
                }
                else
                {
                    var task = client.GetAsync(Api + "api/reports?recID=0&WorkStudyID=" +WorkStudyID).ContinueWith((res) =>
                    {
                        if (res.Result.IsSuccessStatusCode)
                        {
                            reports = JsonConvert.DeserializeObject<RNDReports>(res.Result.Content.ReadAsStringAsync().Result);
                            if (reports != null)
                            {
                                ddlWorkStudyID = reports.ddWorkStudyID;
                                ddTestType = reports.ddTestType;
                               // reports.WorkStudyID = WorkStudyID;
                            }
                        }
                      
                    });
                    task.Wait();                
                }
                ViewBag.ddlWorkStudyID = ddlWorkStudyID;
                ViewBag.ddTestType = ddTestType;
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
            }
            return View(reports);
           // return View();
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

            List<RNDReports> lstExportReports = new List<RNDReports>();
            DataSearch<RNDReports> objReports = null;

            try
            {
                var client = GetHttpClient();
                var task = client.PostAsJsonAsync(Api + "api/Grid", ExportDataFilter).ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        objReports = JsonConvert.DeserializeObject<DataSearch<RNDReports>>(res.Result.Content.ReadAsStringAsync().Result);
                    }
                });

                task.Wait();

                if (objReports != null && objReports.items != null && objReports.items.Count > 0)
                {
                    lstExportReports = objReports.items;
                    string fileName = "Reports" + "_" + DateTime.Now.ToString().Replace(" ", "").Replace("-", "").Replace(":", "");
                    GetExcelFile<RNDReports>(lstExportReports, fileName);
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