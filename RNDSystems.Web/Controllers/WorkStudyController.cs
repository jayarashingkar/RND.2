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
    public class WorkStudyController : BaseController
    {
        /// <summary>
        /// Retrive work study details
        /// </summary>
        /// <returns></returns>
        #region WorkStudy
        public ActionResult WorkSutdyList()
        {
            _logger.Debug("WorkSutdyList");
            List<SelectListItem> studyTypes = null;
            List<SelectListItem> locations = null;
            List<SelectListItem> status = null;
            try
            {
                var client = GetHttpClient();
                var task = client.GetAsync(Api + "api/workstudy?recID=0").ContinueWith((res) =>
                  {
                      if (res.Result.IsSuccessStatusCode)
                      {
                          RNDWorkStudy workStudy = JsonConvert.DeserializeObject<RNDWorkStudy>(res.Result.Content.ReadAsStringAsync().Result);
                          if (workStudy != null)
                          {
                              studyTypes = workStudy.StudyTypes;
                              locations = workStudy.Locations;
                              status = workStudy.Status;
                          }
                      }
                  });
                task.Wait();
                ViewBag.ddStatus = status;
                ViewBag.ddStudyTypes = studyTypes;
                ViewBag.ddLocation = locations;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View();
        }

        /// <summary>
        /// Retrieve work study List details for Update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SaveWorkStudy(int id)
        {
            RNDWorkStudy workStudy = null;
            List<SelectListItem> studyTypes = null;
            List<SelectListItem> locations = null;
            List<SelectListItem> status = null;
            try
            {
                var client = GetHttpClient();
                var task = client.GetAsync(Api + "api/workstudy?recID=" + id).ContinueWith((res) =>
                  {
                      if (res.Result.IsSuccessStatusCode)
                      {
                          workStudy = JsonConvert.DeserializeObject<RNDWorkStudy>(res.Result.Content.ReadAsStringAsync().Result);
                          if (workStudy != null)
                          {
                              studyTypes = workStudy.StudyTypes;
                              locations = workStudy.Locations;
                              status = workStudy.Status;
                          }
                      }
                  });
                task.Wait();

                locations.ForEach(l => {
                    if (l.Selected)
                    {
                        workStudy.Plant = l.Value;
                    }
                });

                ViewBag.ddStatus = status;
                ViewBag.ddStudyTypes = studyTypes;
                ViewBag.ddLocation = locations;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View(workStudy);
        }

        [HttpPost]
        public ActionResult ExportToExcel(string searchWorkStudyNumber, List<string> StudyType, List<string> Plant, List<string> StudyStatus)
        {
            _logger.Debug("WorkSutdyList ExportToExcel");
            string SearchBy = "";
            DataGridoption ExportDataFilter = new DataGridoption();

            if (!string.IsNullOrEmpty(searchWorkStudyNumber))
            {
                SearchBy = SearchBy + ";" + "WorkStudyID:" + searchWorkStudyNumber;
            }

            if (!string.IsNullOrEmpty(StudyType[0].ToString()))
            {
                SearchBy = SearchBy + ";" + "StudyType:" + StudyType[0].ToString();
            }

            if (!string.IsNullOrEmpty(Plant[0].ToString()))
            {
                SearchBy = SearchBy + ";" + "Plant:" + Plant[0].ToString();
            }

            if (!string.IsNullOrEmpty(StudyStatus[0].ToString()))
            {
                SearchBy = SearchBy + ";" + "StudyStatus:" + StudyStatus[0].ToString();
            }

            ExportDataFilter.Screen = "WorkStudy";
            ExportDataFilter.filterBy = "all";
            ExportDataFilter.pageIndex = 0;
            ExportDataFilter.pageSize = 10000;
            ExportDataFilter.searchBy = SearchBy;

            List<RNDWorkStudyViewModel> lstExportWorkStudy = new List<RNDWorkStudyViewModel>();
            DataSearch<RNDWorkStudyViewModel> objWorkstudy = null;

            try
            {
                var client = GetHttpClient();
                var task = client.PostAsJsonAsync(Api + "api/Grid", ExportDataFilter).ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        objWorkstudy = JsonConvert.DeserializeObject<DataSearch<RNDWorkStudyViewModel>>(res.Result.Content.ReadAsStringAsync().Result);
                    }
                });

                task.Wait();

                if (objWorkstudy != null && objWorkstudy.items != null && objWorkstudy.items.Count > 0)
                {
                    lstExportWorkStudy = objWorkstudy.items;
                    string fileName = "WorkSutdyList" + "_" + DateTime.Now.ToString().Replace(" ", "").Replace("-", "").Replace(":", "");
                    GetExcelFile<RNDWorkStudyViewModel>(lstExportWorkStudy, fileName);
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return RedirectToAction("WorkSutdyList");
        }

        /// <summary>
        /// Save or Update work study details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveWorkStudy(RNDWorkStudy model)
        {
            var client = GetHttpClient();
            RNDWorkStudy workStudy = null;
            var task = client.PostAsJsonAsync(Api + "api/workstudy", model).ContinueWith((res) =>
               {
                   if (res.Result.IsSuccessStatusCode)
                   {
                       workStudy = JsonConvert.DeserializeObject<RNDWorkStudy>(res.Result.Content.ReadAsStringAsync().Result);

                   }
               });
            task.Wait();
            if (workStudy != null && Request.Form["AssignMatterial"] != null)
            {
                return RedirectToAction("AssignMaterialList", "AssignMaterial", new { recId = workStudy.RecId, workStudyID = workStudy.WorkStudyID });
            }
            return RedirectToAction("WorkSutdyList");
        }
        #endregion

    }
}
