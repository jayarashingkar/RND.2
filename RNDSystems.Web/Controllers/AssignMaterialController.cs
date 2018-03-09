using Newtonsoft.Json;
using RNDSystems.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace RNDSystems.Web.Controllers
{
    public class AssignMaterialController : BaseController
    {
        // GET: AssignMaterial
        /// <summary>
        /// Retrieve Assign Material List details
        /// </summary>
        /// <param name="recId"></param>
        /// <param name="workStudyID"></param>
        /// <returns></returns>
        public ActionResult AssignMaterialList(int recId, string workStudyID)
        {
            _logger.Debug("AssignMaterialList");
            /*
            List<SelectListItem> ddlAlloy = null;
            List<SelectListItem> ddlTemper = null;
            */
            RNDMaterial material = null;
            try
            {
                var client = GetHttpClient();
                var task = client.GetAsync(Api + "api/AssignMaterial?recID=0").ContinueWith((res) =>
                {
                    if (res.Result.IsSuccessStatusCode)
                    {
                        RNDMaterial rndMaterial = JsonConvert.DeserializeObject<RNDMaterial>(res.Result.Content.ReadAsStringAsync().Result);
                        if (rndMaterial != null)
                        {
                            /*
                            ddlAlloy = rndMaterial.ddlAlloy;
                            ddlTemper = rndMaterial.ddlTemper;
                            */
                        }
                    }
                });
                task.Wait();
                material = new RNDMaterial
                {
                    WorkStudyID = workStudyID
                };
                /*
                ViewBag.ddAlloy = ddlAlloy;
                ViewBag.ddTemper = ddlTemper;
                */
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View(material);
        }
                
        /// <summary>
        /// Retrieve Assign Material List details for Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workStudyId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SaveAssignMaterial(int id, string workStudyId)
        {
            RNDMaterial Material = new RNDMaterial(); 
            List<SelectListItem> DataBase = null;

             DataBase = new List<SelectListItem>();
            // AM.ddlAlloy = new List<SelectListItem>() { GetInitialSelectItem() };     
            DataBase.Add(new SelectListItem
            {
                Value = "NO",
                Text = "None",
                Selected = true,
            });
            DataBase.Add(new SelectListItem
            {
                // Value = "RO",
                Value = "ROM",
                //  Text = "RO",
                Text = "Romania Database",
                Selected = true,
            }); 
            DataBase.Add(new SelectListItem
            {
                Value = "USA",
                Text = "US Database",
                Selected = true,
            });
            /*
            List<SelectListItem> ddlAlloy = null;
            List<SelectListItem> ddlTemper = null;
            */
            try
            {
                if (id >0)
                {
                    var client = GetHttpClient();
                    var task = client.GetAsync(Api + "api/AssignMaterial?recID=" + id).ContinueWith((res) =>
                    {
                        if (res.Result.IsSuccessStatusCode)
                        {
                            Material = JsonConvert.DeserializeObject<RNDMaterial>(res.Result.Content.ReadAsStringAsync().Result);
                            if (Material != null)
                            {
                                /*
                                ddlAlloy = Material.ddlAlloy;
                                ddlTemper = Material.ddlTemper;
                                */
                                if (!string.IsNullOrEmpty(workStudyId))
                                    Material.WorkStudyID = workStudyId;
                            }
                        }
                    });

                    task.Wait();
                }
                else
                {
                   
                    Material.WorkStudyID = workStudyId;                    
                }
                ViewBag.ddDataBase = DataBase;
                /*
                ViewBag.ddAlloy = ddlAlloy;
                ViewBag.ddTemper = ddlTemper;
                */
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View(Material);
            //return RedirectToAction("SaveAssignMaterial");
        }

        /// <summary>
        /// Save or Update Assign Material List details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveAssignMaterial(RNDMaterial model)
        {
            var client = GetHttpClient();

            if (model.IsCopy)
            {
                model.RecID = 0;
                model.IsCopy = false;
            }
            var task = client.PostAsJsonAsync(Api + "api/AssignMaterial", model).ContinueWith((res) =>
            {
                if (res.Result.IsSuccessStatusCode)
                {
                    RNDMaterial RNDMaterial = JsonConvert.DeserializeObject<RNDMaterial>(res.Result.Content.ReadAsStringAsync().Result);
                    if (RNDMaterial != null)
                    {

                    }
                }
            });
            task.Wait();
            return RedirectToAction("AssignMaterialList", new { recId = model.RecID, workStudyID = model.WorkStudyID });
        }

        /// <summary>
        /// User entered the MillLotNo and moves to another control, If the MillLotNo is exist and
        /// it will ftech the value and shows the UACList grid.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="MillLotNo"></param>
        /// <returns></returns>
        public ActionResult UACListing(int id, string MillLotNo)
        {
            return PartialView();
        }      
    }
}