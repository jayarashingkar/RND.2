using RNDSystems.Models.ReportsViewModel;
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
    public class ReportsController : UnSecuredController
    {
        // GET: Reports
        public HttpResponseMessage Get(int recID=0, string WorkStudyID = "none")
        {
            _logger.Debug("Reports Get Called");
            ReportsViewModel reports = new ReportsViewModel();
            try
            {
                // string WorkStudyID = "";
                SqlDataReader reader = null;
                
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();

                // reports.ddTestType = new List<SelectListItem>() { GetInitialSelectItem() };
                //   reports.ddWorkStudyID = new List<SelectListItem>() { GetInitialSelectItem() };
                reports.ddTestType = new List<SelectListItem>();
                reports.ddWorkStudyID = new List<SelectListItem>();

                //  if ((recID == 0) && (WorkStudyID == "none"))
                if (WorkStudyID == "none")
                {
                    using (reader = ado.ExecDataReaderProc("RNDGetWorkStudyFromTesting", "RND"))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                reports.ddWorkStudyID.Add(new SelectListItem
                                {
                                    Value = Convert.ToString(reader["WorkStudyID"]),
                                    Text = Convert.ToString(reader["WorkStudyID"]),
                                    Selected = (reports.WorkStudyID == Convert.ToString(reader["WorkStudyID"])) ? true : false,
                                });
                                //reports.WorkStudyID = Convert.ToString(reader["firstWorkStudyID"]);
                            }
                        }
                    }
                }
                else
                {
                    SqlParameter param2 = new SqlParameter("@WorkStudyID", WorkStudyID);
                    using (reader = ado.ExecDataReaderProc("RNDGetTestTypeFromTesting", "RND", param2))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string TestType = (Convert.ToString(reader["TestType"]));
                                // if  ((Convert.ToString(reader["TestType"])!= null) && (Convert.ToString(reader["TestType"]) != ""))
                                if ((TestType != null) && (TestType != ""))
                                {
                                    reports.ddTestType.Add(new SelectListItem
                                    {
                                        Value = Convert.ToString(reader["TestType"]),
                                        Text = Convert.ToString(reader["TestType"]),
                                        Selected = (reports.TestType == Convert.ToString(reader["TestType"])) ? true : false,
                                    });
                                }
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
            return Serializer.ReturnContent(reports, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }
    }
}