﻿using RNDSystems.API.SQLHelper;
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
    public class AssignMaterialController : UnSecuredController
    {
        /// <summary>
        /// Retrieve the Assign Material
        /// </summary>
        /// <param name="recID"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(int recID)
        {
            _logger.Debug("Assign Material Get Called");
            RNDMaterial AM = null;
            try
            {
                CurrentUser user = ApiUser;
                AM = new RNDMaterial();
                AdoHelper ado = new AdoHelper();

                AM.ddlAlloy = new List<SelectListItem>() { GetInitialSelectItem() };
                AM.ddlTemper = new List<SelectListItem>() { GetInitialSelectItem() };

                if (recID > 0)
                {
                    SqlParameter param1 = new SqlParameter("@RecId", recID);
                    using (SqlDataReader reader = ado.ExecDataReaderProc("RNDAssignMaterial_ReadByID", "RND", new object[] { param1 }))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                AM.RecID = Convert.ToInt32(reader["RecID"]);
                                AM.WorkStudyID = Convert.ToString(reader["WorkStudyID"]);
                                AM.SoNum = Convert.ToString(reader["SoNum"]);
                                AM.MillLotNo = Convert.ToInt32(reader["MillLotNo"]);
                                AM.CustPart = Convert.ToString(reader["CustPart"]);
                                AM.UACPart = Convert.ToDecimal(reader["UACPart"]);
                                AM.Alloy = Convert.ToString(reader["Alloy"]);
                                AM.Temper = Convert.ToString(reader["Temper"]);
                                AM.GageThickness = Convert.ToString(reader["GageThickness"]);
                                AM.Location2 = Convert.ToString(reader["Location2"]);
                                AM.Hole = Convert.ToString(reader["Hole"]);
                                AM.PieceNo = Convert.ToString(reader["PieceNo"]);
                                AM.Comment = Convert.ToString(reader["Comment"]);
                                AM.EntryDate = (!string.IsNullOrEmpty(reader["EntryDate"].ToString())) ? Convert.ToDateTime(reader["EntryDate"]) : (DateTime?)null;
                                AM.EntryBy = Convert.ToString(reader["EntryBy"]);
                                AM.DBCntry = Convert.ToString(reader["DBCntry"]);
                                //AM.RCS = Convert.ToChar(reader["RCS"]);
                            }
                        }
                        if (ado._conn != null && ado._conn.State == System.Data.ConnectionState.Open)
                        {
                            ado._conn.Close(); ado._conn.Dispose();
                        }

                    }
                }

                return Serializer.ReturnContent(AM, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Retrieve the MillLotNo
        /// </summary>
        /// <param name="MillLotNo"></param>
        /// <param name="recID"></param>
        /// <returns></returns>
        // public HttpResponseMessage Get(int MillLotNo, int recID)
      //  public HttpResponseMessage Get(int MillLotNo, int recID, string DataBaseName)
      //{
      //      _logger.Debug("Assign Material Mill Lot No Get Called");
      //      //  SqlDataReader reader = null;
      //      RNDMaterial AM = null;
      //      string DBName;// = "RDBPROD";
      //      try
      //      {
      //          CurrentUser user = ApiUser;
      //          AM = new RNDMaterial();
      //          AdoHelper ado = new AdoHelper();
      //          AM.ddlAlloy = new List<SelectListItem>() { GetInitialSelectItem() };
      //          if (MillLotNo > 0)
      //          // if (UACPART > 0)
      //          {
      //              SqlParameter param1 = new SqlParameter("@MillLotID", MillLotNo);
      //              //  SqlParameter param1 = new SqlParameter("@UACPART", UACPART);

      //              switch (DataBaseName)
      //              {
      //                  case "ROM":
      //                      DBName = "ROPROD";//Romania Productin Database
      //                      break;
      //                  case "USA":
      //                      DBName = "RDBPROD";//US Production Database
      //                      break;
      //                  case "NO":
      //                      //No Database selected 
      //                      DBName = "NO";
      //                      break;
      //                  default:
      //                      DBName = "RDBPROD";//US Production Database
      //                      break;
      //              }

      //              if (DBName != "NO")
      //              {
      //                  using (SqlDataReader reader = ado.ExecDataReaderProc("RNDUACPART_READ", DBName, new object[] { param1 }))
      //                  {
      //                      if (reader.HasRows)
      //                      {
      //                          if (reader.Read())
      //                          {
      //                              AM.MillLotNo = Convert.ToInt32(reader["MillLotID"]);
      //                              //AM.RecID = Convert.ToInt32(reader["RecID"]);
      //                              AM.CustPart = Convert.ToString(reader["CustPartNo"]);
      //                              AM.UACPart = Convert.ToDecimal(reader["UACPart"]);
      //                              AM.Alloy = Convert.ToString(reader["Alloy"]);
      //                              AM.Temper = Convert.ToString(reader["Temper"]);
      //                              AM.SoNum = Convert.ToString(reader["SoNum"]);
      //                          }
      //                      }
      //                      if (ado._conn != null && ado._conn.State == System.Data.ConnectionState.Open)
      //                      {
      //                          ado._conn.Close(); ado._conn.Dispose();
      //                      }
      //                  }

      //              }
      //              else
      //              {
      //                  AM.MillLotNo = -1;
      //              }  
      //          }
      //      }
      //      catch (Exception ex)
      //      {
      //          _logger.Error(ex.Message);
      //          AM.MillLotNo = 0;
      //          AM.Comment = ex.Message;
      //      }
      //      return Serializer.ReturnContent(AM, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
      //  }


        public HttpResponseMessage Get(int MillLotNo, int recID, string DataBaseName)
        {
            _logger.Debug("Assign Material Mill Lot No Get Called");
            //  SqlDataReader reader = null;
            RNDMaterial AM = null;
            string DBName;// = "RDBPROD";
            string storeProc = "RNDUACPART_READ";
            try
            {
                CurrentUser user = ApiUser;
                AM = new RNDMaterial();
                AdoHelper ado = new AdoHelper();
                AM.ddlAlloy = new List<SelectListItem>() { GetInitialSelectItem() };
                if (MillLotNo > 0)
                {
                    SqlParameter param1 = new SqlParameter("@MillLotID", MillLotNo);

                    switch (DataBaseName)
                    {
                        case "ROM":
                            //  DBName = "ROPROD";//Romania Productin Database
                            DBName = "RDBPROD";// using US Production Database for RO and USA
                            storeProc = "RNDUACPART_READ_RO";
                            break;
                        case "USA":
                            DBName = "RDBPROD";//US Production Database
                            storeProc = "RNDUACPART_READ";
                            break;
                        case "NO":
                            //No Database selected 
                            DBName = "NO";
                            storeProc = "RNDUACPART_READ";
                            break;
                        default:
                            DBName = "RDBPROD";//US Production Database
                            break;
                    }

                    if (DBName != "NO")
                    {
                        //using (SqlDataReader reader = ado.ExecDataReaderProc("RNDUACPART_READ", DBName, new object[] { param1 }))
                        using (SqlDataReader reader = ado.ExecDataReaderProc(storeProc, DBName, new object[] { param1 }))
                        {
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    AM.MillLotNo = Convert.ToInt32(reader["MillLotID"]);
                                    //AM.RecID = Convert.ToInt32(reader["RecID"]);
                                    AM.CustPart = Convert.ToString(reader["CustPartNo"]);
                                    AM.UACPart = Convert.ToDecimal(reader["UACPart"]);
                                    AM.Alloy = Convert.ToString(reader["Alloy"]);
                                    AM.Temper = Convert.ToString(reader["Temper"]);
                                    AM.SoNum = Convert.ToString(reader["SoNum"]);
                                }
                            }
                            if (ado._conn != null && ado._conn.State == System.Data.ConnectionState.Open)
                            {
                                ado._conn.Close(); ado._conn.Dispose();
                            }
                        }

                    }
                    else
                    {
                        AM.MillLotNo = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                AM.MillLotNo = 0;
                AM.Comment = ex.Message;
            }
            return Serializer.ReturnContent(AM, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }

        // POST: api/AssignMaterial
        /// <summary>
        /// Save or Update the Assign Material
        /// </summary>
        /// <param name="AssignMaterial"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(RNDMaterial AssignMaterial)
        {
            _logger.Debug("Assign Material Post Called");

            string data = string.Empty;
            try
            {
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                if (AssignMaterial.Hole == null)
                    AssignMaterial.Hole = "";
                if (AssignMaterial.PieceNo == null)
                    AssignMaterial.PieceNo = "";
                if (AssignMaterial.DBCntry == null)
                    AssignMaterial.DBCntry = "NO";
                if (AssignMaterial.CustPart == null)
                    AssignMaterial.CustPart = "";
                SqlParameter param1 = new SqlParameter("@WorkStudyID", AssignMaterial.WorkStudyID);
                SqlParameter param2 = new SqlParameter("@SoNum", AssignMaterial.SoNum);
                SqlParameter param3 = new SqlParameter("@MillLotNo", AssignMaterial.MillLotNo);
                SqlParameter param4 = new SqlParameter("@CustPart", AssignMaterial.CustPart);
                SqlParameter param5 = new SqlParameter("@UACPart", AssignMaterial.UACPart);
                SqlParameter param6 = new SqlParameter("@Alloy", AssignMaterial.Alloy);
                SqlParameter param7 = new SqlParameter("@Temper", AssignMaterial.Temper);
                SqlParameter param8 = new SqlParameter("@GageThickness", AssignMaterial.GageThickness);
                SqlParameter param9 = new SqlParameter("@Location2", AssignMaterial.Location2);
                SqlParameter param10 = new SqlParameter("@Hole", AssignMaterial.Hole);
                SqlParameter param11 = new SqlParameter("@PieceNo", AssignMaterial.PieceNo);
                SqlParameter param12 = new SqlParameter("@Comment", AssignMaterial.Comment);
                SqlParameter param13 = new SqlParameter("@EntryDate", DateTime.Now);
                SqlParameter param14 = new SqlParameter("@EntryBy", user.UserName);

                SqlParameter param15 = new SqlParameter("@DBCntry", AssignMaterial.DBCntry);
                //   SqlParameter param16 = new SqlParameter("@RCS", AssignMaterial.RCS);

                if (AssignMaterial.RecID > 0)
                {
                    SqlParameter param17 = new SqlParameter("@RecId", AssignMaterial.RecID);
                    ado.ExecScalarProc("RNDAssignMaterial_Update", "RND", new object[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15, param17 });
                }
                else
                {
                    ado.ExecScalarProc("RNDAssignMaterial_Insert", "RND", new object[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15 });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Serializer.ReturnContent(AssignMaterial, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }

        // PUT: api/AssignMaterial/1
        public void Put(string ids, string MillLotNo)
        {
        }

        // DELETE: api/AssignMaterial/1
        /// <summary>
        /// Delete the Particular Assign Material
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id)
        {
            _logger.Debug("Assign Material Delete Called");
            try
            {
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                SqlParameter param1 = new SqlParameter("@RecId", id);
                ado.ExecScalarProc("RNDMaterial_Delete", "RND", new object[] { param1 });
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
