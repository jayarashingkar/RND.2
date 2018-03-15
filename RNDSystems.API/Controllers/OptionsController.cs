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
        public HttpResponseMessage Get(int recID, string optionType)
        {
            _logger.Debug("Options Get called");
            SqlDataReader reader = null;
            OptionsViewModel options = null;
            try
            {
                CurrentUser user = ApiUser;
                options = new OptionsViewModel();
                AdoHelper ado = new AdoHelper();
                options.StudyTypeList = new List<SelectListItem>() { GetInitialSelectItem() };
                options.StudyTypeList = new List<SelectListItem>() { GetInitialSelectItem() };

                #region Edit

                if (recID > 0)
                {
                    SqlParameter param1 = new SqlParameter("@RecId", recID);
                    switch (optionType)
                    {
                        case "StudyType":
                            using (reader = ado.ExecDataReaderProc("RNDStudyType_ReadByID", "RND", new object[] { param1 }))
                            {
                                if (reader.HasRows)
                                {
                                    if (reader.Read())
                                    {
                                        options.RecId = Convert.ToInt32(reader["RecId"]);
                                        options.TypeStudy = Convert.ToString(reader["TypeStudy"]);
                                        options.TypeDesc = Convert.ToString(reader["TypeDesc"]);
                                    }
                                }
                            }
                            break;
                        case "Location":
                            using (reader = ado.ExecDataReaderProc("RNDLocation_ReadByID", "RND", new object[] { param1 }))
                            {
                                if (reader.HasRows)
                                {
                                    if (reader.Read())
                                    {
                                        options.RecId = Convert.ToInt32(reader["RecId"]);
                                        options.Plant = Convert.ToInt16(reader["Plant"]);
                                        options.PlantDesc = Convert.ToString(reader["PlantDesc"]);
                                        options.PlantState = Convert.ToString(reader["PlantState"]);
                                        options.PlantType = Convert.ToByte(reader["PlantType"]);
                                    }
                                }
                            }
                            break;
                        default:break;
                    }
                }
                #endregion  
                using (reader = ado.ExecDataReaderProc("RNDStudyType_READ", "RND", null))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            options.StudyTypeList.Add(new SelectListItem
                            {
                                Value = Convert.ToString(reader["TypeStudy"]),
                                Text = Convert.ToString(reader["TypeDesc"]),
                              //  Selected = (options.StudyTypeList == Convert.ToString(reader["TypeStudy"])) ? true : false,
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
                            options.LocationList.Add(new SelectListItem
                            {
                                Value = Convert.ToString(reader["Plant"]),
                                Text = Convert.ToString(reader["PlantDesc"]),
                               // Selected = (!string.IsNullOrEmpty(WS.Plant) && WS.Plant.Trim() == Convert.ToString(reader["Plant"])) ? true : false,

                            });
                        }
                    }
                }
                return Serializer.ReturnContent(options, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }


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
                            //SqlParameter param3 = new SqlParameter("@TypeStudy", option.TypeStudy);
                            if (option.RecId > 0)
                            {
                                SqlParameter param1 = new SqlParameter("@RecID", option.RecId);
                               // ado.ExecScalarProc("RNDStudyType_Update", "RND", new object[] { param1, param2, param3 });
                                ado.ExecScalarProc("RNDStudyType_Update", "RND", new object[] { param1, param2 });
                            }
                            else
                            {
                                // using (SqlDataReader reader = ado.ExecDataReaderProc("StudyType_Insert", "RND", new object[] { param2, param3 }))
                                using (SqlDataReader reader = ado.ExecDataReaderProc("StudyType_Insert", "RND", new object[] { param2 }))
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
                            //SqlParameter param2 = new SqlParameter("@Plant", option.Plant);
                            SqlParameter param3 = new SqlParameter("@PlantDesc", option.PlantDesc);
                            SqlParameter param4 = new SqlParameter("@PlantState", option.PlantState);                            
                            SqlParameter param5 = new SqlParameter("@PlantType", option.PlantType);
                            if (option.RecId > 0)
                            {
                                SqlParameter param1 = new SqlParameter("@RecID", option.RecId);
                                //ado.ExecScalarProc("RNDLocation_Update", "RND", new object[] { param1, param2, param3, param4, param5});
                                ado.ExecScalarProc("RNDLocation_Update", "RND", new object[] { param1, param3, param4, param5 });

                            }
                            else
                            {
                                //using (SqlDataReader reader = ado.ExecDataReaderProc("RNDLocation_Insert", "RND", new object[] { param2, param3, param4, param5 }))
                                using (SqlDataReader reader = ado.ExecDataReaderProc("RNDLocation_Insert", "RND", new object[] { param3, param4, param5 }))

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

        //  public HttpResponseMessage Delete(int id, string optionType)
        public HttpResponseMessage Delete(ApiViewModel data)
        {
            int id = Convert.ToInt32(data.Message);
            string optionType = data.Message1;
            try
            {
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                SqlParameter param1 = new SqlParameter("@RecId", id); 
                switch (optionType)
                {
                    case "StudyType":                           
                            ado.ExecScalarProc("RNDStudyType_Delete", "RND", new object[] { param1 });
                            break;
                    case "Location":                           
                            ado.ExecScalarProc("RNDLocation_Delete", "RND", new object[] { param1 });
                            break;
                    default: break;
                }             
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