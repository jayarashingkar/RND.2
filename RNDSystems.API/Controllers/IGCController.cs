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
    public class IGCController : UnSecuredController
    {
        // GET: Results
        public HttpResponseMessage Post(IGCViewModel ManulEntryData)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            try
            {
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                SqlParameter param1 = new SqlParameter("@SelectedTests", ManulEntryData.SelectedTests);
                SqlParameter param2 = new SqlParameter("@EntryBy", user.UserName);
                SqlParameter param3 = new SqlParameter("@EntryDate", DateTime.Now);
                SqlParameter param4 = new SqlParameter("@SpeciComment", ManulEntryData.SpeciComment);
                SqlParameter param5 = new SqlParameter("@Operator", ManulEntryData.Operator);
                SqlParameter param6 = new SqlParameter("@TestDate", ManulEntryData.TestDate);
                SqlParameter param7 = new SqlParameter("@TimeHrs", ManulEntryData.TimeHrs);
                SqlParameter param8 = new SqlParameter("@TimeMns", ManulEntryData.TimeMns);
                SqlParameter param9 = new SqlParameter("@SubConduct", ManulEntryData.SubConduct);
                SqlParameter param10 = new SqlParameter("@SurfConduct", ManulEntryData.SurfConduct);
                SqlParameter param11 = new SqlParameter("@MinDepth", ManulEntryData.MinDepth);
                SqlParameter param12 = new SqlParameter("@MaxDepth", ManulEntryData.MaxDepth);
                SqlParameter param13 = new SqlParameter("@AvgDepth", ManulEntryData.AvgDepth);
               
                sendMessage.Message = "";
                using (SqlDataReader reader = ado.ExecDataReaderProc("RNDIGCResults_Insert", "RND", new object[]
                { param1, param2, param3, param4, param5, param6, param7, param8,param9, param10, param11, param12, param13}))
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
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                // return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Serializer.ReturnContent(sendMessage, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }
    }
}