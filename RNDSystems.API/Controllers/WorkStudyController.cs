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
    public class WorkStudyController : UnSecuredController
    {
        /// <summary>
        /// Retrieve the Work study details
        /// </summary>
        /// <param name="recID"></param>
        /// <returns></returns>
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
                WS.Status = new List<SelectListItem>() { GetInitialSelectItem() };
                WS.StudyTypes = new List<SelectListItem>() { GetInitialSelectItem() };
                WS.Locations = new List<SelectListItem>() { GetInitialSelectItem() };
                if (recID > 0)
                {
                    SqlParameter param1 = new SqlParameter("@RecId", recID);
                    using (reader = ado.ExecDataReaderProc("RNDWorkStudy_ReadByID", "RND", new object[] { param1 }))
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
                                WS.Plant = Convert.ToString(reader["Plant"]);
                                WS.TempID = Convert.ToString(reader["TempID"]);
                                WS.EntryBy = Convert.ToString(reader["EntryBy"]);
                                WS.EntryDate = (!string.IsNullOrEmpty(reader["EntryDate"].ToString())) ? Convert.ToDateTime(reader["EntryDate"]) : (DateTime?)null;
                                WS.StudyScope = Convert.ToString(reader["StudyScope"]);
                                WS.Experimentation = Convert.ToString(reader["Experimentation"]);
                                WS.FinalSummary = Convert.ToString(reader["FinalSummary"]);
                                WS.Uncertainty = Convert.ToString(reader["Uncertainty"]);
                            }
                        }
                    }
                }
                using (reader = ado.ExecDataReaderProc("RNDStudyStatus_READ", "RND", null))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            WS.Status.Add(new SelectListItem
                            {
                                Value = Convert.ToString(reader["StudyStatus"]),
                                Text = Convert.ToString(reader["StatusDesc"]),
                                Selected = (WS.StudyStatus == Convert.ToString(reader["StudyStatus"])) ? true : false,
                            });
                        }
                    }
                }

                using (reader = ado.ExecDataReaderProc("RNDStudyType_READ", "RND", null))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            WS.StudyTypes.Add(new SelectListItem
                            {
                                Value = Convert.ToString(reader["TypeStudy"]),
                                Text = Convert.ToString(reader["TypeDesc"]),
                                Selected = (WS.StudyType == Convert.ToString(reader["TypeStudy"])) ? true : false,
                            });
                        }
                    }
                }

                 using (reader = ado.ExecDataReaderProc("RNDLocation_READ", "RND", null))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            WS.Locations.Add(new SelectListItem
                            {
                                Value = Convert.ToString(reader["Plant"]),
                                Text = Convert.ToString(reader["PlantDesc"]),                              
                                Selected =(!string.IsNullOrEmpty(WS.Plant)&&WS.Plant.Trim() == Convert.ToString(reader["Plant"])) ? true : false,
                             
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

// POST: api/WorkStudy
/// <summary>
/// Save or Update the Work study details
/// </summary>
/// <param name="workStudy"></param>
/// <returns></returns>
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
        SqlParameter param15 = new SqlParameter("@StudyScope", workStudy.StudyScope);
        SqlParameter param17 = new SqlParameter("@Experimentation", workStudy.Experimentation);
        SqlParameter param18 = new SqlParameter("@FinalSummary", workStudy.FinalSummary);
        SqlParameter param19 = new SqlParameter("@Uncertainty", workStudy.Uncertainty);

        if (workStudy.RecId > 0)
        {
            SqlParameter param16 = new SqlParameter("@RecId", workStudy.RecId);
            ado.ExecScalarProc("RNDWorkStudy_Update", "RND", new object[] { param1, param2, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15, param16, param17, param18, param19 });
        }
        else
        {
            using (SqlDataReader reader = ado.ExecDataReaderProc("RNDWorkStudy_Insert", "RND", new object[] { param1, param2, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15, param17, param18, param19 }))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        workStudy.RecId = Convert.ToInt32(reader["RecId"].ToString());
                    }
                }
            }

        }
    }
    catch (Exception ex)
    {
        _logger.Error(ex.Message);
        return new HttpResponseMessage(HttpStatusCode.InternalServerError);
    }
    return Serializer.ReturnContent(workStudy, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
}

// PUT: api/WorkStudy/5
public void Put(int id, [FromBody]string value)
{
}

// DELETE: api/WorkStudy/5
/// <summary>
/// Delete the Work study details
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
public HttpResponseMessage Delete(int id)
{
    try
    {
        CurrentUser user = ApiUser;
        AdoHelper ado = new AdoHelper();
        SqlParameter param1 = new SqlParameter("@RecId", id);
        ado.ExecScalarProc("RNDWorkStudy_Delete", "RND", new object[] { param1 });
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