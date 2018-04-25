using Newtonsoft.Json;
using RNDSystems.Models;
using RNDSystems.Models.TestViewModels;
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
using System.Linq;
using System.Web;



namespace RNDSystems.Web.Controllers
{
    public class ImportDataController : BaseController
    {
                
        // GET: ImportData
        public ActionResult ImportData(string result)
        {
            _logger.Debug("ImportData");
            List<SelectListItem> ddTestTypes = null;

            ImportDataViewModel data = new ImportDataViewModel();
            try
            {
               if (result == null)             
                {
                    data.results = "";
                    ddTestTypes = new List<SelectListItem>();               
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
                            }
                        }
                    });
                    task.Wait();
                    ViewBag.ddTestTypes = ddTestTypes;
                    ViewBag.ddTestTypesDefault = ddTestTypes;
                }
                else
                {
                     data.results = TempData["results"].ToString(); 
                   // data.results = result;
                    // populate ddTestNos by sending the WorkStudyId as parameter
                }
            }
            catch (Exception ex)
            {
                data.results = ex.ToString();
                _logger.Error(ex);
              
            }
            return View(data);
        }


        [HttpPost]
        //     public ActionResult Upload(HttpPostedFileBase file, ImportDataViewModel model)
        public ActionResult Upload(HttpPostedFileBase file, string selectedTestType)
        {
            ApiViewModel importData = new ApiViewModel();
            try
            {               
                var client = GetHttpClient();
                if (file != null && file.ContentLength > 0)
                {                    
                    var fileName = Path.GetFileName(file.FileName);
                    BinaryReader b = new BinaryReader(file.InputStream);
                    byte[] binData = b.ReadBytes(file.ContentLength);
                    string results = System.Text.Encoding.UTF8.GetString(binData);

                    var task = client.GetAsync(Api + "api/ImportData?id=0" + "&results=" + results + "&selectedTestType=" + selectedTestType).ContinueWith((res) =>
                    {
                        if (res.Result.IsSuccessStatusCode)
                        {
                            importData = JsonConvert.DeserializeObject<ApiViewModel>(res.Result.Content.ReadAsStringAsync().Result);
                        }
                    });
                    task.Wait();               
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                importData.Message = ex.ToString();
            }
            TempData["result"] = importData.Message;
            return RedirectToAction("ImportData");
            // return RedirectToAction("ImportData", new { result = importData.Message });

        }
        
    }
}