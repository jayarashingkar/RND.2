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
    public class FatigueController : UnSecuredController
    {
        // GET: Results
        public HttpResponseMessage Post(FatigueViewModel ManulEntryData)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            try
            {
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                SqlParameter param1 = new SqlParameter("@SelectedTests", ManulEntryData.SelectedTests);
                SqlParameter param2 = new SqlParameter("@EntryBy", user.UserName);
                SqlParameter param3 = new SqlParameter("@EntryDate", DateTime.Now);
                SqlParameter param4 = new SqlParameter("@SpecimenDrawing", ManulEntryData.SpecimenDrawing);
                SqlParameter param5 = new SqlParameter("@MinStress", ManulEntryData.MinStress);
                SqlParameter param6 = new SqlParameter("@MaxStress", ManulEntryData.MaxStress);
                SqlParameter param7 = new SqlParameter("@MinLoad", ManulEntryData.MinLoad);
                SqlParameter param8 = new SqlParameter("@MaxLoad", ManulEntryData.MaxLoad);
                SqlParameter param9 = new SqlParameter("@WidthOrDia", ManulEntryData.WidthOrDia);
                SqlParameter param10 = new SqlParameter("@Thickness", ManulEntryData.Thickness);
                SqlParameter param11 = new SqlParameter("@HoleDia", ManulEntryData.HoleDia);
                SqlParameter param12 = new SqlParameter("@AvgChamferDepth", ManulEntryData.AvgChamferDepth);
                SqlParameter param13 = new SqlParameter("@Frequency", ManulEntryData.Frequency);
                SqlParameter param14 = new SqlParameter("@CyclesToFailure", ManulEntryData.CyclesToFailure);
                SqlParameter param15 = new SqlParameter("@Roughness", ManulEntryData.Roughness);
                SqlParameter param16 = new SqlParameter("@TestFrame", ManulEntryData.TestFrame);
                SqlParameter param17 = new SqlParameter("@Comment", ManulEntryData.Comment);
                SqlParameter param18 = new SqlParameter("@FractureLocation", ManulEntryData.FractureLocation);
                SqlParameter param19 = new SqlParameter("@Operator", ManulEntryData.Operator);
                SqlParameter param20 = new SqlParameter("@TestDate", ManulEntryData.TestDate);
                SqlParameter param21 = new SqlParameter("@TestTime", ManulEntryData.TestTime);

                sendMessage.Message = "";
                using (SqlDataReader reader = ado.ExecDataReaderProc("RNDFatigueResults_Insert", "RND", new object[]
                { param1, param2, param3, param4, param5, param6, param7, param8,param9, param10,
                  param11, param12, param13, param14, param15, param16, param17, param18,param19, param20, param21}))
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
                // return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Serializer.ReturnContent(sendMessage, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }
    }
}