using RNDSystems.API.SQLHelper;
using RNDSystems.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using RNDSystems.Models.ViewModels;
using RNDSystems.Common.Constants;

namespace RNDSystems.API.Controllers
{
    public class OptionsController : UnSecuredController
    {
        // GET: Options
        public HttpResponseMessage Post(OptionsViewModel option)
        {
            string data = string.Empty;
            try
            {
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();

                switch (option.optionType)
                {
                    case "StudyType":
                        {
                            SqlParameter param2 = new SqlParameter("@TypeDesc", option.TypeDesc);
                            SqlParameter param3 = new SqlParameter("@TypeStudy", option.TypeStudy);
                            if (option.RecId > 0)
                            {
                                SqlParameter param1 = new SqlParameter("@RecID", option.RecId);
                                ado.ExecScalarProc("RNDStudyType_Update", "RND", new object[] { param1, param2, param3 });
                            }
                            else
                            {
                                using (SqlDataReader reader = ado.ExecDataReaderProc("StudyType_Insert", "RND", new object[] { param2, param3 }))
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            option.RecId = Convert.ToInt32(reader["RecId"].ToString());
                                        }
                                    }
                                }

                            }
                            break;
                        }
                    //  case OptionTypeConstants.Location:
                    case "Location":
                        {
                            SqlParameter param2 = new SqlParameter("@Plant", option.Plant);
                            SqlParameter param3 = new SqlParameter("@PlantDesc", option.PlantDesc);
                            SqlParameter param4 = new SqlParameter("@PlantState", option.PlantState);                            
                            SqlParameter param5 = new SqlParameter("@PlantType", option.PlantType);
                            if (option.RecId > 0)
                            {
                                SqlParameter param1 = new SqlParameter("@RecID", option.RecId);
                                ado.ExecScalarProc("RNDLocation_Update", "RND", new object[] { param1, param2, param3, param4, param5});
                            }
                            else
                            {
                                using (SqlDataReader reader = ado.ExecDataReaderProc("RNDLocation_Insert", "RND", new object[] { param2, param3, param4, param5 }))
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            option.RecId = Convert.ToInt32(reader["RecId"].ToString());
                                        }
                                    }
                                }

                            }
                            break;

                        }
                    default:
                        break;
                }    
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Serializer.ReturnContent(option, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }

    }
}