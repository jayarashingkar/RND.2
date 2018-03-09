using Newtonsoft.Json;
using RNDSystems.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace RNDSystems.Web.Controllers
{
    public class ProcessingMaterialController : BaseController
    {


        /// <summary>
        /// Get Processing Material
        /// </summary>
        /// <param name="recId"></param>
        /// <param name="workStudyID"></param>
        /// <returns></returns>
        public ActionResult ProcessingMaterialList(int recId, string workStudyID)
        {
            _logger.Debug("ProcessingMaterialList");
            List<SelectListItem> ddHTLogID = null;
            List<SelectListItem> ddAgeLotID = null;
            List<SelectListItem> ddMillLotNo = null;

            RNDProcessing processing = null;
            try
            {
                ddHTLogID = new List<SelectListItem>();
                ddAgeLotID = new List<SelectListItem>();
                var client = GetHttpClient();
                var task = client.GetAsync(Api + "api/Processing?recID=0&workStudyID=" + workStudyID).ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        RNDProcessing rndProcessing = JsonConvert.DeserializeObject<RNDProcessing>(res.Result.Content.ReadAsStringAsync().Result);
                        if (rndProcessing!=null)
                        {
                            ddHTLogID = rndProcessing.ddHTLogID;
                            ddAgeLotID = rndProcessing.ddAgeLotID;
                            ddMillLotNo = rndProcessing.ddMillLotNo;
                        }
                    }
                });
                task.Wait();
                ViewBag.ddHTLogID = ddHTLogID;
                ViewBag.ddAgeLotID = ddAgeLotID;
                ViewBag.ddMillLotNo = ddMillLotNo;

                processing = new RNDProcessing
                {
                    WorkStudyID = workStudyID
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View(processing);
        }
               
        /// <summary>
        /// Retrieve Processing Material List details for Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workStudyId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SaveProcessingMaterial(int id, string workStudyId)
        {
            RNDProcessing processing = null;

            List<SelectListItem> ddSHTStartHrs = null;
          
            List<SelectListItem> ddArtStartHrs = null;
            List<SelectListItem> ddSHTStartMin = null;
          
            List<SelectListItem> ddArtStartMin = null;
        //    List<SelectListItem> ddlRCS = null;

            List<SelectListItem> ddlMillLotNo = null;
            List<SelectListItem> ddlPieceNo = null;
            List<SelectListItem> ddlHole = null;         
           
            string strValue = string.Empty;
            int totalMinutes = 59;
            int intRowId = 0;
            try
            {
                var client = GetHttpClient();

                //start here
                if (workStudyId == null)
                {
                    var task = client.GetAsync(Api + "api/Processing?recID=" + id).ContinueWith((res) =>
                    {
                        if (res.Result.IsSuccessStatusCode)
                        {
                            processing = JsonConvert.DeserializeObject<RNDProcessing>(res.Result.Content.ReadAsStringAsync().Result);
                            if (processing != null)
                            {                               
                                ddlMillLotNo = processing.ddMillLotNo;
                                ddlHole = processing.ddHole;
                                ddlPieceNo = processing.ddPieceNo;
                            }
                        }
                    });
                    task.Wait();                   
                }
                //ends here
                else
                {                   
                    var task = client.GetAsync(Api + "api/Processing?recID=" + id + "&workStudyID=" + workStudyId).ContinueWith((res) =>
                    {
                        if (res.Result.IsSuccessStatusCode)
                        {
                            processing = JsonConvert.DeserializeObject<RNDProcessing>(res.Result.Content.ReadAsStringAsync().Result);
                            if (processing != null)
                            {
                                if (!string.IsNullOrEmpty(workStudyId))
                                {
                                    processing.WorkStudyID = workStudyId;

                                }
                                ddlMillLotNo = processing.ddMillLotNo;
                                ddlHole = processing.ddHole;
                                ddlPieceNo = processing.ddPieceNo;
                            }
                        }
                    });
                    task.Wait();
                }

                ddSHTStartHrs = new List<SelectListItem>();
                ddArtStartHrs = new List<SelectListItem>();
                ddSHTStartMin = new List<SelectListItem>();
                ddArtStartMin = new List<SelectListItem>();
              //  ddlRCS = new List<SelectListItem>();
                
                while (intRowId <= totalMinutes)
                {
                    if (intRowId > 9)
                    {
                        strValue = Convert.ToString(intRowId);
                    }
                    else
                    {
                        strValue = "0" + Convert.ToString(intRowId);
                    }
                    
                    if(intRowId < 24)
                    {
                        ddSHTStartHrs.Add(new SelectListItem
                        {
                            Value = strValue,
                            Text = strValue,
                            Selected = (Convert.ToString(processing.SHTStartHrs) == Convert.ToString(strValue)) ? true : false,
                        });
                        ddArtStartHrs.Add(new SelectListItem
                        {
                            Value = strValue,
                            Text = strValue,
                            Selected = (Convert.ToString(processing.ArtStartHrs) == Convert.ToString(strValue)) ? true : false,
                        });
                       
                    }

                    ddSHTStartMin.Add(new SelectListItem
                    {
                        Value = strValue,
                        Text = strValue,
                        Selected = (Convert.ToString(processing.SHTStartMns) == Convert.ToString(strValue)) ? true : false,
                    });
                    ddArtStartMin.Add(new SelectListItem
                    {
                        Value = strValue,
                        Text = strValue,
                        Selected = (Convert.ToString(processing.ArtStartMns) == Convert.ToString(strValue)) ? true : false,
                    });
                   
                    intRowId += 1;
                }

                //ddlRCS.Add(new SelectListItem
                //{
                //    Value = Convert.ToString(' '),
                //    Text = Convert.ToString(' '),
                //    Selected = (Convert.ToString(processing.RCS) == Convert.ToString(' ')) ? true : false,
                //});
                //ddlRCS.Add(new SelectListItem
                //{
                //    Value = Convert.ToString('1'),
                //    Text = Convert.ToString('1'),
                //    Selected = (Convert.ToString(processing.RCS) == Convert.ToString('1')) ? true : false,
                //});    

                ViewBag.ddSHTStartHours = ddSHTStartHrs;
                ViewBag.ddArtStartHours = ddArtStartHrs;
                ViewBag.ddSHTStartMinutes = ddSHTStartMin;
                ViewBag.ddArtStartMinutes = ddArtStartMin;

                ViewBag.ddlMillLotNo = ddlMillLotNo;
                ViewBag.ddlHole = ddlHole;
                ViewBag.ddlPieceNo = ddlPieceNo;

               // ViewBag.ddlRCS = ddlRCS;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View(processing);
        }

        /// <summary>
        /// Save or Update Processing Material List details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveProcessingMaterial(RNDProcessing model)
        {
            var client = GetHttpClient();
            
            try
            {
                var task = client.PostAsJsonAsync(Api + "api/Processing", model).ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        RNDProcessing RNDProcessing = JsonConvert.DeserializeObject<RNDProcessing>(res.Result.Content.ReadAsStringAsync().Result);
                    }
                });
                task.Wait();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return RedirectToAction("ProcessingMaterialList", new { recId = model.RecID, workStudyID = model.WorkStudyID });
        }
    }
}