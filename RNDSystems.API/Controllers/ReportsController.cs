
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
        public HttpResponseMessage Get(int recID, string WorkStudyID)
        {
            _logger.Debug("Reports Get Called");
            try
            {
               // string WorkStudyID = "";
                SqlDataReader reader = null;
                RNDReports reports = new RNDReports();
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();

                // reports.ddTestType = new List<SelectListItem>() { GetInitialSelectItem() };
                //   reports.ddWorkStudyID = new List<SelectListItem>() { GetInitialSelectItem() };
                reports.ddTestType = new List<SelectListItem>();
                reports.ddWorkStudyID = new List<SelectListItem>();

                if ((recID == 0)&&(WorkStudyID == "''"))
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
                                reports.WorkStudyID = Convert.ToString(reader["firstWorkStudyID"]);
                            }
                        }
                    }
                    SqlParameter param2 = new SqlParameter("@WorkStudyID", reports.WorkStudyID);
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
                else if ((recID == 0) && (WorkStudyID != "''"))
                {
                    SqlParameter param1 = new SqlParameter("@WorkStudyID", WorkStudyID);
                    using (reader = ado.ExecDataReaderProc("RNDGetTestTypeFromTesting", "RND",param1)) 
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
               return Serializer.ReturnContent(reports,this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters,this.Request);
            }
           catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}