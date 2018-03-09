
using RNDSystems.API.SQLHelper;
using RNDSystems.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace RNDSystems.API.Controllers
{
    public class ProcessingController : UnSecuredController
    {
        public HttpResponseMessage Get(int recID, int MillLotNo, string WorkStudyId)
        {
            _logger.Debug("Processing Get Called");
            SqlDataReader reader = null;
            RNDProcessing PM = null;

            CurrentUser user = ApiUser;
            PM = new RNDProcessing();
            AdoHelper ado = new AdoHelper();

            //    PM.ddMillLotNo = new List<SelectListItem>() { GetInitialSelectItem() };
            PM.ddPieceNo = new List<SelectListItem>() { GetInitialSelectItem() };
            PM.ddHole = new List<SelectListItem>() { GetInitialSelectItem() };
            try
            {
                if (!string.IsNullOrEmpty(WorkStudyId))
                {
                    SqlParameter param0 = new SqlParameter("@WorkStudyID", WorkStudyId);
                    SqlParameter param1 = new SqlParameter("@MillLotNo", MillLotNo);

                    using (reader = ado.ExecDataReaderProc("RNDHole_READByMillLotNo", "RND", param0, param1))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string Hole = Convert.ToString(reader["Hole"]);
                                if ((Hole != " ") && (Hole != "") && (Hole != null))
                                {
                                    PM.ddHole.Add(new SelectListItem
                                    {
                                        Value = Hole,
                                        Text = Hole,
                                        Selected = (PM.Hole == Hole) ? true : false,
                                    });
                                }

                            }
                        }
                    }
                    SqlParameter param2 = new SqlParameter("@WorkStudyID", WorkStudyId);
                    SqlParameter param3 = new SqlParameter("@MillLotNo", MillLotNo);
                    using (reader = ado.ExecDataReaderProc("RNDPcNo_READByMilLotNo", "RND", param2, param3))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string PieceNo = Convert.ToString(reader["PieceNo"]);
                                if ((PieceNo != " ") && (PieceNo != "") && (PieceNo != null))
                                {
                                    PM.ddPieceNo.Add(new SelectListItem
                                    {
                                        Value = PieceNo,
                                        Text = PieceNo,
                                        Selected = (PM.PieceNo == PieceNo) ? true : false,
                                    });
                                }
                            }
                        }
                    }
                }
                return Serializer.ReturnContent(PM, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }


        //public HttpResponseMessage Get(string WorkStudyID)
        //{
        //    _logger.Debug("Processing Material Get(string WorkSutdyId) Called");
        //    SqlDataReader reader = null;
        //    RNDProcessing PM = null;
        //    try
        //    {
        //        CurrentUser user = ApiUser;
        //        PM = new RNDProcessing();
        //        AdoHelper ado = new AdoHelper();
        //        PM.ddHTLogID = new List<SelectListItem>() { GetInitialSelectItem() };
        //        PM.ddAgeLotID = new List<SelectListItem>() { GetInitialSelectItem() };
        //        return Serializer.ReturnContent(PM, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message);
        //        return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        //    }
        //}

        /// <summary>
        /// Get for Processing Material List page - populating the dropdown Menus 
        /// MillLotNo, PieceNo,Hole
        /// </summary>
        /// <param name="recID"></param>
        /// <param name="WorkStudyId"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(string WorkStudyId, int recID)
        {
            _logger.Debug("Processing Get Called");
            SqlDataReader reader = null;
            RNDProcessing PM = null;

            CurrentUser user = ApiUser;
            PM = new RNDProcessing();
            AdoHelper ado = new AdoHelper();

            PM.ddMillLotNo = new List<SelectListItem>() { GetInitialSelectItem() };
            PM.ddPieceNo = new List<SelectListItem>() { GetInitialSelectItem() };
            PM.ddHole = new List<SelectListItem>() { GetInitialSelectItem() };

            PM.ddHTLogID = new List<SelectListItem>() { GetInitialSelectItem() };
            PM.ddAgeLotID = new List<SelectListItem>() { GetInitialSelectItem() };
            try
            {
                if (!string.IsNullOrEmpty(WorkStudyId))
                {
                    SqlParameter param0 = new SqlParameter("@WorkStudyID", WorkStudyId);
                    using (reader = ado.ExecDataReaderProc("RNDMillLotNo_READ", "RND", param0))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PM.ddMillLotNo.Add(new SelectListItem
                                {
                                    Value = Convert.ToString(reader["MillLotNo"]),
                                    Text = Convert.ToString(reader["MillLotNo"]),
                                    Selected = (PM.MillLotNo == Convert.ToInt32(reader["MillLotNo"])) ? true : false,
                                });
                            }
                        }
                    }

                    SqlParameter param1 = new SqlParameter("@WorkStudyId", WorkStudyId);

                    using (reader = ado.ExecDataReaderProc("RNDHTLogID_Read", "RND", param1))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string HTLogID = Convert.ToString(reader["HTLogID"]);
                                if ((HTLogID != " ") && (HTLogID != "") && (HTLogID != null))
                                {
                                    PM.ddHTLogID.Add(new SelectListItem
                                    {
                                        Value = HTLogID,
                                        Text = HTLogID,
                                        Selected = (PM.HTLogID == HTLogID) ? true : false,
                                    });
                                }
                            }
                        }
                    }

                    SqlParameter param2 = new SqlParameter("@WorkStudyId", WorkStudyId);

                    using (reader = ado.ExecDataReaderProc("RNDAgeLotID_Read", "RND", param2))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string AgeLotID = Convert.ToString(reader["AgeLotID"]);
                              
                                if ((AgeLotID != " ") && (AgeLotID != "") && (AgeLotID != null))
                                {
                                    PM.ddAgeLotID.Add(new SelectListItem
                                    {
                                        Value = AgeLotID,
                                         Text = AgeLotID,                                       
                                        Selected = (PM.AgeLotID == AgeLotID) ? true : false,
                                    });
                                }
                            }
                        }
                    }

                }

                return Serializer.ReturnContent(PM, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Retrieve the Processing Material details
        /// </summary>
        /// <param name="recID"></param>
        /// <returns></returns>
        //public HttpResponseMessage Get(int recID, string workStudyID)
        public HttpResponseMessage Get(int recID)
        {
            _logger.Debug("Processing Get Called");
            SqlDataReader reader = null;
            RNDProcessing PM = null;

            try
            {
                CurrentUser user = ApiUser;
                PM = new RNDProcessing();
                AdoHelper ado = new AdoHelper();
                PM.ddMillLotNo = new List<SelectListItem>() { GetInitialSelectItem() };
                PM.ddPieceNo = new List<SelectListItem>() { GetInitialSelectItem() };
                PM.ddHole = new List<SelectListItem>() { GetInitialSelectItem() };

                PM.ddAgeLotID = new List<SelectListItem>() { GetInitialSelectItem() };
                PM.ddHTLogID = new List<SelectListItem>() { GetInitialSelectItem() };

                if (recID > 0)
                {
                    SqlParameter param1 = new SqlParameter("@RecId", recID);
                    using (reader = ado.ExecDataReaderProc("RNDProcessingMaterial_ReadByID", "RND", new object[] { param1 }))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                PM.RecID = Convert.ToInt32(reader["RecID"]);
                                PM.WorkStudyID = Convert.ToString(reader["WorkStudyID"]);
                                PM.MillLotNo = Convert.ToInt32(reader["MillLotNo"]);
                                PM.Hole = Convert.ToString(reader["Hole"]);
                                PM.PieceNo = Convert.ToString(reader["PieceNo"]);
                                PM.Sonum = Convert.ToString(reader["Sonum"]);
                                PM.ProcessNo = Convert.ToByte(reader["ProcessNo"]);
                                PM.ProcessID = Convert.ToString(reader["ProcessID"]);
                                PM.HTLogNo = Convert.ToInt32(reader["HTLogNo"]);
                                PM.HTLogID = Convert.ToString(reader["HTLogID"]);
                                PM.AgeLotNo = Convert.ToInt32(reader["AgeLotNo"]);
                                PM.AgeLotID = Convert.ToString(reader["AgeLotID"]);

                                PM.SHTTemp = Convert.ToString(reader["SHTTemp"]);
                                PM.SHSoakHrs = Convert.ToString(reader["SHSoakHrs"]);
                                PM.SHSoakMns = Convert.ToString(reader["SHSoakMns"]);
                                PM.SHTStartHrs = Convert.ToString(reader["SHTStartHrs"]);
                                PM.SHTStartMns = Convert.ToString(reader["SHTStartMns"]);
                                //PM.SHTDate = (!string.IsNullOrEmpty(reader["SHTDate"].ToString())) ? Convert.ToDateTime(reader["SHTDate"]) : (DateTime?)null;
                                PM.SHTDate = Convert.ToString(reader["SHTDate"]);

                                PM.StretchPct = Convert.ToString(reader["StretchPct"]);
                                PM.AfterSHTHrs = Convert.ToString(reader["AfterSHTHrs"]);
                                PM.AfterSHTMns = Convert.ToString(reader["AfterSHTMns"]);
                                PM.NatAgingHrs = Convert.ToString(reader["NatAgingHrs"]);
                                PM.NatAgingMns = Convert.ToString(reader["NatAgingMns"]);
                                PM.ArtStartHrs = Convert.ToString(reader["ArtStartHrs"]);
                                PM.ArtStartMns = Convert.ToString(reader["ArtStartMns"]);
                                //PM.ArtAgeDate = (!string.IsNullOrEmpty(reader["ArtAgeDate"].ToString())) ? Convert.ToDateTime(reader["ArtAgeDate"]) : (DateTime?)null;
                                PM.ArtAgeDate = Convert.ToString(reader["ArtAgeDate"]);

                                PM.ArtAgeTemp1 = Convert.ToString(reader["ArtAgeTemp1"]);
                                PM.ArtAgeHrs1 = Convert.ToString(reader["ArtAgeHrs1"]);
                                PM.ArtAgeMns1 = Convert.ToString(reader["ArtAgeMns1"]);
                                PM.ArtAgeTemp2 = Convert.ToString(reader["ArtAgeTemp2"]);
                                PM.ArtAgeHrs2 = Convert.ToString(reader["ArtAgeHrs2"]);
                                PM.ArtAgeMns2 = Convert.ToString(reader["ArtAgeMns2"]);
                                PM.ArtAgeTemp3 = Convert.ToString(reader["ArtAgeTemp3"]);
                                PM.ArtAgeHrs3 = Convert.ToString(reader["ArtAgeHrs3"]);
                                PM.ArtAgeMns3 = Convert.ToString(reader["ArtAgeMns3"]);

                                PM.FinalTemper = Convert.ToString(reader["FinalTemper"]);
                                PM.TargetCount = Convert.ToString(reader["TargetCount"]);
                                PM.ActualCount = Convert.ToString(reader["ActualCount"]);
                                // PM.RCS = Convert.ToString(reader["RCS"]);

                                PM.RNDLotID = Convert.ToString(reader["RNDLotID"]);

                                PM.total = Convert.ToInt32(reader["total"]);
                            }
                        }
                    }
                    //populate all the dropdowns
                    //start here 

                    //ViewBag.ddSHTStartHours = ddSHTStartHrs;
                    //ViewBag.ddArtStartHours = ddArtStartHrs;
                    //ViewBag.ddSHTStartMinutes = ddSHTStartMin;
                    //ViewBag.ddArtStartMinutes = ddArtStartMin;

                    //ViewBag.ddlMillLotNo = ddlMillLotNo;
                    //ViewBag.ddlHole = ddlHole;
                    //ViewBag.ddlPieceNo = ddlPieceNo;

                    //ViewBag.ddlRCS = ddlRCS;
                    //ends here

                    if (!string.IsNullOrEmpty(PM.WorkStudyID))
                    {

                        SqlParameter param2 = new SqlParameter("@WorkStudyId", PM.WorkStudyID);
                        SqlParameter param3 = new SqlParameter("@MillLotNo", PM.MillLotNo);

                        using (reader = ado.ExecDataReaderProc("RNDHole_READByMillLotNo", "RND", param2, param3))
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string Hole = Convert.ToString(reader["Hole"]);
                                    if ((Hole != " ") && (Hole != "") && (Hole != null))
                                    {
                                        PM.ddHole.Add(new SelectListItem
                                        {
                                            Value = Hole,
                                            Text = Hole,
                                            Selected = (PM.Hole == Hole) ? true : false,
                                        });
                                    }
                                }
                            }
                        }
                        SqlParameter param4 = new SqlParameter("@WorkStudyId", PM.WorkStudyID);
                        SqlParameter param5 = new SqlParameter("@MillLotNo", PM.MillLotNo);
                        using (reader = ado.ExecDataReaderProc("RNDPcNo_READByMilLotNo", "RND", param4, param5))
                        {
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    string PieceNo = Convert.ToString(reader["PieceNo"]);
                                    if ((PieceNo != " ") && (PieceNo != "") && (PieceNo != null))
                                    {
                                        PM.ddPieceNo.Add(new SelectListItem
                                        {
                                            Value = PieceNo,
                                            Text = PieceNo,
                                            Selected = (PM.PieceNo == PieceNo) ? true : false,
                                        });
                                    }
                                }
                            }
                        }
                        SqlParameter param6 = new SqlParameter("@WorkStudyId", PM.WorkStudyID);
                        using (reader = ado.ExecDataReaderProc("RNDMillLotNo_READ", "RND", param6))
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    PM.ddMillLotNo.Add(new SelectListItem
                                    {
                                        Value = Convert.ToString(reader["MillLotNo"]),
                                        Text = Convert.ToString(reader["MillLotNo"]),
                                        Selected = (PM.MillLotNo == Convert.ToInt32(reader["MillLotNo"])) ? true : false,
                                    });
                                }
                            }
                        }
                    }



                }

                //    using (reader = ado.ExecDataReaderProc("RNDAgeLotID_Read", "RND"))
                //    {
                //        if (reader.HasRows)
                //        {
                //            while (reader.Read())
                //            {
                //                PM.ddAgeLotID.Add(new SelectListItem
                //                {
                //                    Value = Convert.ToString(reader["AgeLotID"]),
                //                    Text = Convert.ToString(reader["AgeLotID"]),
                //                    Selected = (PM.AgeLotID == Convert.ToString(reader["AgeLotID"])) ? true : false,
                //                });
                //            }
                //        }
                //    }
                //    using (reader = ado.ExecDataReaderProc("RNDHTLogID_Read", "RND"))
                //    {
                //        if (reader.HasRows)
                //        {
                //            while (reader.Read())
                //            {
                //                PM.ddHTLogID.Add(new SelectListItem
                //                {
                //                    Value = Convert.ToString(reader["HTLogID"]),
                //                    Text = Convert.ToString(reader["HTLogID"]),
                //                    Selected = (PM.HTLogID == Convert.ToString(reader["HTLogID"])) ? true : false,
                //                });
                //            }
                //        }
                //    }
                //}
                return Serializer.ReturnContent(PM, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HTGroupName"></param>
        /// <param name="SelectedRecords"></param>
        /// <returns></returns>

        // public HttpResponseMessage Post(string HTGroupName, List<string> SelectedRecords)
        //   public HttpResponseMessage Get(string GroupName, string SelectedRecords, string GroupType)
        public HttpResponseMessage Get(string GroupName, string SelectedRecords, string GroupType)
        {
            //   dynamic ds = null;

            //  bool isSuccess = false;

            try
            {
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();

                SqlParameter param1 = new SqlParameter("@SelectedRecords", SelectedRecords);
                SqlParameter param2 = new SqlParameter("@GroupName", GroupName);
                SqlParameter param3 = new SqlParameter("@GroupType", GroupType);

                //if (recId in SelectedRecords ) 
                   ado.ExecScalarProc("RNDGroupName_Insert", "RND", new object[] { param1, param2, param3 });
               // ado.ExecScalarProc("RNDGroupName_Insert", "RND", new object[] { param1, param3 });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            return Serializer.ReturnContent(HttpStatusCode.OK, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }

        // POST: api/AssignMaterial
        /// <summary>
        /// Save or Update the Processing Material details
        /// </summary>
        /// <param name="ProcessingMaterial"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(RNDProcessing ProcessingMaterial)
        {
            _logger.Debug("Processing Material Post Called");

            string data = string.Empty;
            try
            {
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();

                if (ProcessingMaterial.PieceNo == null)
                    ProcessingMaterial.PieceNo = "";

                if (ProcessingMaterial.Hole == null)
                    ProcessingMaterial.Hole = "";

                string PieceNo = ProcessingMaterial.PieceNo;                
                if ((PieceNo.Trim()) == "-1")
                {
                    ProcessingMaterial.PieceNo = "";
                }
                string Hole = ProcessingMaterial.Hole;
                if ((Hole.Trim()) == "-1")
                {
                    ProcessingMaterial.Hole = "";
                }

                //if (ProcessingMaterial.AgeLotNo == null)
                //{
                //    ProcessingMaterial.AgeLotNo = 0;
                //}
                SqlParameter param1 = new SqlParameter("@WorkStudyID", ProcessingMaterial.WorkStudyID);
                SqlParameter param2 = new SqlParameter("@MillLotNo", ProcessingMaterial.MillLotNo);

                SqlParameter param3 = new SqlParameter("@PieceNo", ProcessingMaterial.PieceNo);
                SqlParameter param4 = new SqlParameter("@FinalTemper", ProcessingMaterial.FinalTemper);

                SqlParameter param5 = new SqlParameter("@Sonum", ProcessingMaterial.Sonum);
                SqlParameter param6 = new SqlParameter("@ProcessNo", ProcessingMaterial.ProcessNo);
                SqlParameter param7 = new SqlParameter("@ProcessID", ProcessingMaterial.ProcessID);
                SqlParameter param8 = new SqlParameter("@HTLogID", ProcessingMaterial.HTLogID);

                SqlParameter param9 = new SqlParameter("@AgeLotNo", ProcessingMaterial.AgeLotNo);
                SqlParameter param10 = new SqlParameter("@Hole", ProcessingMaterial.Hole);

                SqlParameter param11 = new SqlParameter("@AgeLotID", ProcessingMaterial.AgeLotID);

                SqlParameter param12 = new SqlParameter("@SHTTemp", ProcessingMaterial.SHTTemp);
                SqlParameter param13 = new SqlParameter("@SHSoakHrs", ProcessingMaterial.SHSoakHrs);
                SqlParameter param14 = new SqlParameter("@SHSoakMns", ProcessingMaterial.SHSoakMns);
                SqlParameter param15 = new SqlParameter("@SHTStartHrs", ProcessingMaterial.SHTStartHrs);
                SqlParameter param16 = new SqlParameter("@SHTStartMns", ProcessingMaterial.SHTStartMns);
                SqlParameter param17 = new SqlParameter("@StretchPct", ProcessingMaterial.StretchPct);
                //  SqlParameter param18 = new SqlParameter("@RCS", ProcessingMaterial.RCS);
                SqlParameter param19 = new SqlParameter("@SHTDate", ProcessingMaterial.SHTDate);
                SqlParameter param20 = new SqlParameter("@AfterSHTHrs", ProcessingMaterial.AfterSHTHrs);
                SqlParameter param21 = new SqlParameter("@AfterSHTMns", ProcessingMaterial.AfterSHTMns);
                SqlParameter param22 = new SqlParameter("@NatAgingHrs", ProcessingMaterial.NatAgingHrs);
                SqlParameter param23 = new SqlParameter("@NatAgingMns", ProcessingMaterial.NatAgingMns);
                SqlParameter param24 = new SqlParameter("@ArtStartHrs", ProcessingMaterial.ArtStartHrs);
                SqlParameter param25 = new SqlParameter("@ArtStartMns", ProcessingMaterial.ArtStartMns);
                SqlParameter param26 = new SqlParameter("@ArtAgeDate", ProcessingMaterial.ArtAgeDate);

                SqlParameter param27 = new SqlParameter("@ArtAgeTemp1", ProcessingMaterial.ArtAgeTemp1);
                SqlParameter param28 = new SqlParameter("@ArtAgeHrs1", ProcessingMaterial.ArtAgeHrs1);
                SqlParameter param29 = new SqlParameter("@ArtAgeMns1", ProcessingMaterial.ArtAgeMns1);
                SqlParameter param30 = new SqlParameter("@ArtAgeTemp2", ProcessingMaterial.ArtAgeTemp2);
                SqlParameter param31 = new SqlParameter("@ArtAgeHrs2", ProcessingMaterial.ArtAgeHrs2);
                SqlParameter param32 = new SqlParameter("@ArtAgeMns2", ProcessingMaterial.ArtAgeMns2);
                SqlParameter param33 = new SqlParameter("@ArtAgeTemp3", ProcessingMaterial.ArtAgeTemp3);
                SqlParameter param34 = new SqlParameter("@ArtAgeHrs3", ProcessingMaterial.ArtAgeHrs3);
                SqlParameter param35 = new SqlParameter("@ArtAgeMns3", ProcessingMaterial.ArtAgeMns3);

                SqlParameter param36 = new SqlParameter("@TargetCount", ProcessingMaterial.TargetCount);
                SqlParameter param37 = new SqlParameter("@ActualCount", ProcessingMaterial.ActualCount);
                SqlParameter param38 = new SqlParameter("@total", ProcessingMaterial.total);

                // SqlParameter param39 = new SqlParameter("@RNDLotID", ProcessingMaterial.RNDLotID);
                SqlParameter param41 = new SqlParameter("@HTLogNo", ProcessingMaterial.HTLogNo);
                if (ProcessingMaterial.RecID > 0)
                {
                    SqlParameter param40 = new SqlParameter("@RecId", ProcessingMaterial.RecID);
                    ado.ExecScalarProc("RNDProcessingMaterial_Update", "RND", new object[] {
                        param40,param1, param2, param3,param4,
                        //param5,param6,
                        //param7,param8, param9,  
                        param10,
                       // param11,
                        param12, param13,param14,
                        param15, param16, param17, param19, param20,
                        param21, param22, param23,param24, param25,
                        param26, param27, param28, param29, param30,
                        param31, param32, param33, param34, param35,
                        param36, param37
                        //param38,
                        //param39,
                       // param41
                    });
                }
                else
                {
                    ado.ExecScalarProc("RNDProcessingMaterial_Insert", "RND", new object[] { param1, param2, param3,
                        param4,
                        param9, param10,  param12, param13,
                        param14, param15, param16, param17, param19, param20, param21, param22, param23,
                        param24, param25, param26, param27, param28, param29, param30, param31, param32, param33,
                        param34, param35, param36, param37,
                        param38,
                        //param39, 
                        param41 });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Serializer.ReturnContent(ProcessingMaterial, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }

        // PUT: api/ProcessingMaterial/1
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProcessingMaterial/1
        /// <summary>
        /// Delete the Processing Material details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id)
        {
            _logger.Debug("Processing Material Delete Called");
            try
            {
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                SqlParameter param1 = new SqlParameter("@RecId", id);
                ado.ExecScalarProc("RNDProcessing_Delete", "RND", new object[] { param1 });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Serializer.ReturnContent(HttpStatusCode.OK, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }
    }
}
