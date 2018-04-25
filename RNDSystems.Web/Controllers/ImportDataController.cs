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
        public ActionResult ImportData(string WorkStudyID)
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
                if (WorkStudyID == null)
                {
                    ddTestTypes = new List<SelectListItem>();
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
                            }
                        }
                    });
                    task.Wait();
                    ViewBag.ddTestTypes = ddTestTypes;
                    ViewBag.ddTestTypesDefault = ddTestTypes;
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


        [HttpPost]
        //     public ActionResult Upload(HttpPostedFileBase file, ImportDataViewModel model)
        public ActionResult Upload(HttpPostedFileBase file, string selectedTestType)
        {
            try
            {
                var client = GetHttpClient();
                if (file != null && file.ContentLength > 0)
                {
                    ApiViewModel importData = new ApiViewModel();
                    var fileName = Path.GetFileName(file.FileName);
                    BinaryReader b = new BinaryReader(file.InputStream);
                    byte[] binData = b.ReadBytes(file.ContentLength);
                    string results = System.Text.Encoding.UTF8.GetString(binData);

                    //results.HasFieldsEnclosedInQuotes = false;
                    //results.Delimiters = new[] { "," };
                   


                    // int i = 0;
                    //var task = client.GetAsync(Api + "api/ImportData?file="+ file + "&selectedTestType="+ selectedTestType).ContinueWith((res) =>
                    //{
                    //    if (res.Result.IsSuccessStatusCode)
                    //    {
                    //        importData = JsonConvert.DeserializeObject<ApiViewModel>(res.Result.Content.ReadAsStringAsync().Result);
                    //    }
                    //});
                    var task = client.GetAsync(Api + "api/ImportData?id=0" + "&results=" + results + "&selectedTestType=" + selectedTestType).ContinueWith((res) =>
                    {
                        if (res.Result.IsSuccessStatusCode)
                        {
                            importData = JsonConvert.DeserializeObject<ApiViewModel>(res.Result.Content.ReadAsStringAsync().Result);
                        }
                    });
                    task.Wait();
                    #region switch TestType
                    //switch (selectedTestType)
                    //{
                    //    case "Tension":
                    //        {
                    //            importData = UploadTension(token);
                                
                    //            break;
                    //        }
                    //    case "Compression":
                    //        {
                    //            importData = UploadCompression(token);
                    //            break;
                    //        }
                    //    case "Bearing":
                    //        {
                               
                    //            break;
                    //        }
                    //    case "Shear":
                    //        {
                               
                    //            break;
                    //        }
                    //    case "Notch Yield":
                    //        {
                                
                    //            break;
                    //        }
                    //    case "Residual Strength":
                    //        {
                               
                    //            break;
                    //        }
                    //    case "Fracture Toughness":
                    //        {
                               
                    //            break;
                    //        }
                    //    case "Modulus Tension":
                    //        {
                                
                    //            break;
                    //        }
                    //    case "Modulus Compression":
                    //        {
                               
                    //            break;
                    //        }
                    //    case "Fatigue":
                    //        {
                    //            break;
                    //        }
                    //    default:
                    //        break;

                    //}
                    #endregion 
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }

            return RedirectToAction("ImportData");
        }

      
        //private ApiViewModel UploadTension(string[] token)
        //{
        //    ApiViewModel importData = new ApiViewModel();
        //    var client = GetHttpClient();
        //    List<TensionViewModel> tensionList = new List<TensionViewModel>();
        //    string[] splitnew = null;
        //    int i = 0;
        //    while (i < token.Length - 1)
        //    {
        //        TensionViewModel tensionData = new TensionViewModel();
        //        //i = 0;
        //        if (token[i].Contains('\n') && splitnew[1] != null)
        //        {
        //            tensionData.WorkStudyID = splitnew[1]; i++;
        //        }
        //        else
        //        {
        //            tensionData.WorkStudyID = Convert.ToString(token[i]); i++;
        //        }

        //        tensionData.TestNo = Convert.ToInt32(token[i]); i++;
        //        tensionData.SubConduct = Convert.ToDecimal(token[i]); i++;
        //        tensionData.SurfConduct = Convert.ToDecimal(token[i]); i++;
        //        tensionData.FtuKsi = Convert.ToDecimal(token[i]); i++;
        //        tensionData.FtyKsi = Convert.ToDecimal(token[i]); i++; ;
        //        tensionData.eElongation = Convert.ToDecimal(token[i]); i++;
        //        tensionData.EModulusMpsi = Convert.ToDecimal(token[i]); i++;
        //        tensionData.SpeciComment = Convert.ToString(token[i]); i++;
        //        tensionData.Operator = Convert.ToString(token[i]); i++;
        //        tensionData.TestDate = Convert.ToString(token[i]); i++;

        //        if ((token[i]).Contains('\n'))
        //            splitnew = token[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
        //        tensionData.TestTime = Convert.ToString(splitnew[0]);

        //        tensionList.Add(tensionData);
        //    }

        //    DataSearch<TensionViewModel> dsTension = new DataSearch<TensionViewModel>();
        //    dsTension.items = tensionList;
        //   // dsTension.Message = selectedTestType;


        //    var task = client.PostAsJsonAsync(Api + "api/ImportData", dsTension).ContinueWith((res) =>
        //    {
        //        if (res.Result.IsSuccessStatusCode)
        //        {
        //            importData = JsonConvert.DeserializeObject<ApiViewModel>(res.Result.Content.ReadAsStringAsync().Result);
        //        }
        //    });
        //    task.Wait();
         

        //    return importData;
        //}

        //private ApiViewModel UploadCompression(string[] token)
        //{ 
        //    ApiViewModel importData = new ApiViewModel();
        //    var client = GetHttpClient();
        //    List<CompressionViewModel> CompressionList = new List<CompressionViewModel>();
        //    string[] splitnew = null;
        //    int i = 0;
                      
        //    try
        //    {
        //        i = 0;
        //        while (i < token.Length - 1)
        //        {
        //            token[i] = token[i].Replace("\"","");
        //            token[i] = token[i].Replace("\"/", "");
        //            i++;
        //        }

        //        i = 0;
        //        while (i < token.Length - 1)
        //        {
        //            CompressionViewModel CompressionData = new CompressionViewModel();
        //            //i = 0;
        //            if (token[i].Contains('\n') && splitnew[1] != null)
        //            {
        //                CompressionData.WorkStudyID = splitnew[1]; i++;
        //            }
        //            else
        //            {
                        
        //                CompressionData.WorkStudyID = Convert.ToString(token[i]); i++;
        //            }
        //            CompressionData.TestNo = Convert.ToInt32(token[i]); i++;
        //            CompressionData.SubConduct = Convert.ToDecimal(token[i]); i++;
        //            CompressionData.SurfConduct = Convert.ToDecimal(token[i]); i++;
        //            CompressionData.FcyKsi = Convert.ToDecimal(token[i]); i++;

        //            CompressionData.EcModulusMpsi = Convert.ToDecimal(token[i]); i++;
        //            CompressionData.SpeciComment = Convert.ToString(token[i]); i++;
        //            CompressionData.Operator = Convert.ToString(token[i]); i++;
        //            CompressionData.TestDate = Convert.ToString(token[i]); i++;

        //            if ((token[i]).Contains('\n'))
        //                splitnew = token[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
        //            CompressionData.TestTime = Convert.ToString(splitnew[0]);

        //            CompressionList.Add(CompressionData);
        //        }

        //        DataSearch<CompressionViewModel> dsCompression = new DataSearch<CompressionViewModel>();
        //        dsCompression.items = CompressionList;
        //        //dsCompression.Message = selectedTestType;

        //        var task = client.PostAsJsonAsync(Api + "api/ImportData", dsCompression).ContinueWith((res) =>
        //        {
        //            if (res.Result.IsSuccessStatusCode)
        //            {
        //                importData = JsonConvert.DeserializeObject<ApiViewModel>(res.Result.Content.ReadAsStringAsync().Result);
        //            }
        //        });
        //        task.Wait();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message);
        //        importData.Message = ex.Message;
        //    }
        
        //    return importData;
        //}
    }
}