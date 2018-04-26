using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Text;

using RNDSystems.Models.ManualViewModels;
using RNDSystems.Models.ViewModels;
using RNDSystems.API.SQLHelper;
using RNDSystems.Models;

namespace RNDSystems.API.Controllers
{
    public class SCCResultsController : UnSecuredController
    {
        // GET: Results
        public HttpResponseMessage Post(SCCViewModel ManulEntryData)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            try
            {

                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                SqlParameter param1 = new SqlParameter("@SelectedTests", ManulEntryData.SelectedTests);
                SqlParameter param2 = new SqlParameter("@StressKsi", ManulEntryData.StressKsi);
                SqlParameter param3 = new SqlParameter("@TimeDays", ManulEntryData.TimeDays);
                SqlParameter param4 = new SqlParameter("@TestStatus", ManulEntryData.TestStatus);
                SqlParameter param5 = new SqlParameter("@SpeciComment", ManulEntryData.SpeciComment);
                SqlParameter param6 = new SqlParameter("@Operator", ManulEntryData.Operator);
                SqlParameter param7 = new SqlParameter("@TestStartDate", ManulEntryData.TestStartDate);
                SqlParameter param8 = new SqlParameter("@TestEndDate", ManulEntryData.TestEndDate);
                SqlParameter param9 = new SqlParameter("@EntryBy", user.UserName);
                SqlParameter param10 = new SqlParameter("@EntryDate", DateTime.Now);

                sendMessage.Message = "";
                using (SqlDataReader reader = ado.ExecDataReaderProc("RNDSCCResults_Insert", "RND", new object[]
                {  param2, param3, param4, param5, param6, param7, param8, param9, param10}))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            sendMessage.Custom = Convert.ToInt32(reader["TestingNo"]);
                            if (sendMessage.Message == "")
                                sendMessage.Message += sendMessage.Custom;
                            else
                                sendMessage.Message += ", " + sendMessage.Custom;
                        }
                        sendMessage.Success = true;
                    }
                    if (ado._conn != null && ado._conn.State == System.Data.ConnectionState.Open)
                    {
                        ado._conn.Close(); ado._conn.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
            }
            return Serializer.ReturnContent(sendMessage, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }
    }
}