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
    public class WorkStudyListController : UnSecuredController
    {
        public HttpResponseMessage Get(int recID)
        {
            _logger.Debug("WorkStudy Get called");
            SqlDataReader reader = null;
            RNDWorkStudy WS = null;
            try
            {
                CurrentUser user = ApiUser;
                WS = new RNDWorkStudy();
                AdoHelper ado = new AdoHelper();
                if (recID > 0)
                {
                    SqlParameter param1 = new SqlParameter("@RecId", recID);
                    using (reader = ado.ExecDataReaderProc("RNDWorkStudy_ReadByID", new object[] { param1 }))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                WS.RecId = Convert.ToInt32(reader["RecId"]);
                                WS.WorkStudyID = Convert.ToString(reader["WorkStudyID"]);
                                WS.StudyType = Convert.ToString(reader["StudyType"]);
                                WS.StudyTitle = Convert.ToString(reader["StudyDesc"]);
                                WS.StudyDesc = Convert.ToString(reader["StudyDesc"]);
                                WS.PlanOSCost = Convert.ToDecimal(reader["PlanOSCost"]);
                                WS.AcctOSCost = Convert.ToDecimal(reader["AcctOSCost"]);
                                WS.StudyStatus = Convert.ToString(reader["StudyStatus"]);
                                WS.StartDate = Convert.ToString(reader["StartDate"]);
                                WS.DueDate = Convert.ToString(reader["DueDate"]);
                                WS.CompleteDate = Convert.ToString(reader["CompleteDate"]);
                                WS.Plant = Convert.ToInt32(reader["Plant"]);
                                WS.TempID = Convert.ToString(reader["TempID"]);
                                WS.EntryBy = Convert.ToString(reader["EntryBy"]);
                                WS.EntryDate = (!string.IsNullOrEmpty(reader["EntryDate"].ToString())) ? Convert.ToDateTime(reader["EntryDate"]) : (DateTime?)null;

                                //check for [RNDStudyScope]
                                
                            }
                        }
                    }
                }
                using (reader = ado.ExecDataReaderProc("RNDStudyStatus_READ", null))
                {
                    if (reader.HasRows)
                    {
                        WS.Status = new List<SelectListItem>();
                        while (reader.Read())
                        {
                            WS.Status.Add(new SelectListItem
                            {
                                Value = Convert.ToString(reader["StudyStatus"]),
                                Text = Convert.ToString(reader["StatusDesc"]),
                            });
                        }
                    }
                }

                using (reader = ado.ExecDataReaderProc("RNDStudyType_READ", null))
                {
                    if (reader.HasRows)
                    {
                        WS.StudyTypes = new List<SelectListItem>();
                        while (reader.Read())
                        {
                            WS.StudyTypes.Add(new SelectListItem
                            {
                                Value = Convert.ToString(reader["TypeStudy"]),
                                Text = Convert.ToString(reader["TypeDesc"]),
                            });
                        }
                    }
                }

                using (reader = ado.ExecDataReaderProc("RNDLocation_READ", null))
                {
                    if (reader.HasRows)
                    {
                        WS.Locations = new List<SelectListItem>();
                        while (reader.Read())
                        {
                            WS.Locations.Add(new SelectListItem
                            {
                                Value = Convert.ToString(reader["Plant"]),
                                Text = Convert.ToString(reader["PlantDesc"]),
                            });
                        }
                    }
                }
                return Serializer.ReturnContent(WS, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        // POST: api/WorkStudylist
        public HttpResponseMessage Post(RNDWorkStudy workStudy)
        {
            string data = string.Empty;
            try
            {
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                SqlParameter param1 = new SqlParameter("@WorkStudyID", workStudy.WorkStudyID);
                SqlParameter param2 = new SqlParameter("@StudyType", workStudy.StudyType);
                //SqlParameter param3 = new SqlParameter("@StudyTitle", workStudy.StudyTitle);
                SqlParameter param4 = new SqlParameter("@StudyDesc", workStudy.StudyDesc);
                SqlParameter param5 = new SqlParameter("@PlanOSCost", workStudy.PlanOSCost);
                SqlParameter param6 = new SqlParameter("@AcctOSCost", workStudy.AcctOSCost);
                SqlParameter param7 = new SqlParameter("@StudyStatus", workStudy.StudyStatus);
                SqlParameter param8 = new SqlParameter("@StartDate", workStudy.StartDate);
                SqlParameter param9 = new SqlParameter("@DueDate", workStudy.DueDate);
                SqlParameter param10 = new SqlParameter("@CompleteDate", workStudy.CompleteDate);
                SqlParameter param11 = new SqlParameter("@Plant", workStudy.Plant);
                SqlParameter param12 = new SqlParameter("@EntryBy", user.UserId);
                SqlParameter param13 = new SqlParameter("@EntryDate", DateTime.Now);
                SqlParameter param14 = new SqlParameter("@TempID", workStudy.TempID);
                if (workStudy.RecId > 0)
                {
                    SqlParameter param15 = new SqlParameter("@RecId", workStudy.RecId);
                    ado.ExecScalarProc("RNDWorkStudy_Update", new object[] { param1, param2, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15 });
                }
                else
                {
                    ado.ExecScalarProc("RNDWorkStudy_Insert", new object[] { param1, param2, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14 });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Serializer.ReturnContent(workStudy, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }

        // PUT: api/WorkStudylist/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WorkStudylist/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                SqlParameter param1 = new SqlParameter("@RecId", id);
                ado.ExecScalarProc("RNDWorkStudy_Delete", new object[] { param1 });
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