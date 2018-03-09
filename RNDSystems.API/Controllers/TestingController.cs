using RNDSystems.API.SQLHelper;
using RNDSystems.Models;
using RNDSystems.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace RNDSystems.API.Controllers
{
    public class TestingController : UnSecuredController
    {
        public HttpResponseMessage Get(int recID)
        {
            _logger.Debug("Testing Get Called");
            SqlDataReader reader = null;
            RNDTesting TM = null;
            try
            {
                CurrentUser user = ApiUser;
                TM = new RNDTesting();
                AdoHelper ado = new AdoHelper();

                //  TM.ddTestType = new List<SelectListItem>() { GetInitialSelectItem() };
                TM.ddTestType = new List<SelectListItem>() { GetInitialSelectItem() };
                TM.ddLotID = new List<SelectListItem>() { GetInitialSelectItem() };
                TM.ddSubTestType = new List<SelectListItem>() { GetInitialSelectItem() };
                TM.ddLocation2 = new List<SelectListItem>() { GetInitialSelectItem() };
                TM.ddHole = new List<SelectListItem>() { GetInitialSelectItem() };
                TM.ddPieceNo = new List<SelectListItem>() { GetInitialSelectItem() };

                if (recID > 0)
                {
                    //edit
                    SqlParameter param1 = new SqlParameter("@TestingNo", recID);
                    using (reader = ado.ExecDataReaderProc("RNDTestingMaterial_ReadByTestingNo", "RND",new object[] { param1 }))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                TM.TestingNo = Convert.ToInt32(reader["TestingNo"]);
                                TM.WorkStudyID = Convert.ToString(reader["WorkStudyID"]);
                                TM.LotID = Convert.ToString(reader["LotID"]);
                                TM.MillLotNo = Convert.ToInt32(reader["MillLotNo"]);
                                TM.SoNum = Convert.ToString(reader["SoNum"]);
                                TM.Hole = Convert.ToString(reader["Hole"]);
                                TM.PieceNo = Convert.ToString(reader["PieceNo"]);
                                TM.Alloy = Convert.ToString(reader["Alloy"]);
                                TM.Temper = Convert.ToString(reader["Temper"]);
                                TM.CustPart = Convert.ToString(reader["CustPart"]);
                                TM.UACPart = Convert.ToInt32(reader["UACPart"]);
                                TM.GageThickness = Convert.ToString(reader["GageThickness"]);
                                TM.Orientation = Convert.ToString(reader["Orientation"]);
                                TM.Location1 = Convert.ToString(reader["Location1"]);
                                TM.Location2 = Convert.ToString(reader["Location2"]);
                                TM.Location3 = Convert.ToString(reader["Location3"]);
                                TM.SpeciComment = Convert.ToString(reader["SpeciComment"]);
                                TM.TestType = Convert.ToString(reader["TestType"]).Trim();
                                TM.SubTestType = Convert.ToString(reader["SubTestType"]);
                                TM.Status = Convert.ToChar(reader["Status"]);
                                TM.Selected = Convert.ToChar(reader["Selected"]);
                                TM.EntryDate = (!string.IsNullOrEmpty(reader["EntryDate"].ToString())) ? Convert.ToDateTime(reader["EntryDate"]) : (DateTime?)null;
                                TM.EntryBy = Convert.ToString(reader["EntryBy"]);
                                TM.TestLab = Convert.ToString(reader["TestLab"]);
                                TM.Printed = Convert.ToChar(reader["Printed"]);
                                TM.Replica = Convert.ToString(reader["Replica"]);
                               // TM.RCS = Convert.ToChar(reader["RCS"]);
                                TM.total = 0;
                            }
                        }
                    }
                    string WorkStudyID = TM.WorkStudyID;
                    SqlParameter param2 = new SqlParameter("@WorkStudyID", WorkStudyID);
                    using (reader = ado.ExecDataReaderProc("RNDLotID_READ", "RND", param2))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TM.ddLotID.Add(new SelectListItem
                                {
                                    Value = Convert.ToString(reader["LotID"]),
                                    Text = Convert.ToString(reader["LotID"]),
                                    Selected = (TM.LotID == Convert.ToString(reader["LotID"])) ? true : false,
                                });
                            }
                        }
                       
                    }
                    SqlParameter param3 = new SqlParameter("@TestType", TM.TestType);
                    if (!string.IsNullOrEmpty(TM.TestType))
                    {
                        
                        using (reader = ado.ExecDataReaderProc("RNDSubTestType_READ", "RND", param3))
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    TM.ddSubTestType.Add(new SelectListItem
                                    {
                                        Value = Convert.ToString(reader["SubTestType"]),
                                        Text = Convert.ToString(reader["SubTestType"]),
                                        Selected = (TM.SubTestType == Convert.ToString(reader["SubTestType"])) ? true : false,
                                    });
                                }
                            }
                        }
                    }
                    SqlParameter param4 = new SqlParameter("@MillLotNo", TM.MillLotNo);
                    SqlParameter param41 = new SqlParameter("@WorkStudyID", TM.WorkStudyID);

                    using (reader = ado.ExecDataReaderProc("RNDGetLocation2", "RND", param4, param41))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TM.ddLocation2.Add(new SelectListItem
                                {
                                    Value = Convert.ToString(reader["Location2"]),
                                    Text = Convert.ToString(reader["Location2"]),
                                    Selected = (TM.Location2 == Convert.ToString(reader["Location2"])) ? true : false,
                                });
                            }
                        }
                    }
                    SqlParameter param5 = new SqlParameter("@ProcessID", TM.LotID);
                    using (reader = ado.ExecDataReaderProc("RNDGetHole", "RND", param5))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {  
                                TM.ddHole.Add(new SelectListItem
                                {
                                    Value = Convert.ToString(reader["Hole"]),
                                    Text = Convert.ToString(reader["Hole"]),
                                    Selected = (TM.Hole == Convert.ToString(reader["Hole"])) ? true : false,
                                });                               
                            }
                        }
                    }
                    SqlParameter param6 = new SqlParameter("@ProcessID", TM.LotID);
                    using (reader = ado.ExecDataReaderProc("RNDGetPieceNo", "RND", param6))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TM.ddPieceNo.Add(new SelectListItem
                                {
                                    Value = Convert.ToString(reader["PieceNo"]),
                                    Text = Convert.ToString(reader["PieceNo"]),
                                    Selected = (TM.PieceNo == Convert.ToString(reader["PieceNo"])) ? true : false,
                                });
                            }
                        }
                    }
                }

                using (reader = ado.ExecDataReaderProc("RNDTestType_READ", "RND"))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            TM.ddTestType.Add(new SelectListItem
                            {
                                Value = Convert.ToString(reader["TestDesc"]).Trim(),
                                Text = Convert.ToString(reader["TestDesc"]).Trim(),
                                Selected = (TM.TestType == Convert.ToString(reader["TestDesc"]).Trim()) ? true : false,                               
                            });
                        }
                    }
                }
               
                return Serializer.ReturnContent(TM, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Called from SaveTestingMaterial - page
        /// </summary>
        /// <param name="recID"></param>
        /// <param name="WorkStudyID"></param>
        /// <returns></returns>
        
        public HttpResponseMessage Get(int recID, string WorkStudyID)
        {
            _logger.Debug("Testing Get with WorkStudy Called");
            SqlDataReader reader = null;
            RNDTesting TM = null;

            CurrentUser user = ApiUser;
            TM = new RNDTesting();
            AdoHelper ado = new AdoHelper();

            TM.ddTestType = new List<SelectListItem>() { GetInitialSelectItem() };
            TM.ddLotID = new List<SelectListItem>() { GetInitialSelectItem() };
            try
            {
                if (!string.IsNullOrEmpty(WorkStudyID))
                {
                    SqlParameter param0 = new SqlParameter("@WorkStudyID", WorkStudyID);
                    using (reader = ado.ExecDataReaderProc("RNDLotID_READ", "RND",param0))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TM.ddLotID.Add(new SelectListItem
                                {
                                    Value = Convert.ToString(reader["LotID"]),
                                    Text = Convert.ToString(reader["LotID"]),
                                    Selected = (TM.LotID == Convert.ToString(reader["LotID"])) ? true : false,
                                });
                            }
                        }
                    }
                  
                    using (reader = ado.ExecDataReaderProc("RNDTestType_READ", "RND"))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TM.ddTestType.Add(new SelectListItem
                                {
                                    Value = Convert.ToString(reader["TestDesc"]),
                                    Text = Convert.ToString(reader["TestDesc"]),
                                    Selected = (TM.TestType == Convert.ToString(reader["TestDesc"])) ? true : false,
                                });
                            }
                        }
                    }

                }
                return Serializer.ReturnContent(TM, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Loc2"></param>
        /// <param name="LotId"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(string Loc2, string LotId, string WorkStudyID)
        {

            _logger.Debug("Testing Get with WorkStudy and Loc2");
            SqlDataReader reader = null;
            RNDTesting TM = null;

            CurrentUser user = ApiUser;
            TM = new RNDTesting();
            AdoHelper ado = new AdoHelper();

            try
            {
                if (!string.IsNullOrEmpty(LotId))
                {
                    int MillLotNo = findMillLotNo(LotId);

                    SqlParameter param0 = new SqlParameter("@MillLotNo", MillLotNo);
                    SqlParameter param1 = new SqlParameter("@Loc2", Loc2);
                    SqlParameter param2 = new SqlParameter("@WorkStudyID", WorkStudyID);
                    using (reader = ado.ExecDataReaderProc("RNDGageThickness_READ", "RND", param2,param0,param1))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                TM.GageThickness = Convert.ToString(reader["GageThickness"]);
                            }                           
                        }
                    }
                }
                return Serializer.ReturnContent(TM, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        
        public HttpResponseMessage Get(string WorkStudyID, string LotID, int recID)
        {
            // read MillLotNo,SoNum,Hole,PieceNo - from RNDProcessing
            // send MillLotNo - as parameter read UACPArt, Alloy , Temper from RNDMAterial - give the first record.
           
            _logger.Debug("Testing Get with WorkStudy and LotID Called");
            SqlDataReader reader = null;
            RNDTesting TM = null;

            CurrentUser user = ApiUser;
            TM = new RNDTesting();
            AdoHelper ado = new AdoHelper();


            //    TM.ddGageThickness = new List<SelectListItem>() { GetInitialSelectItem() };
            TM.ddHole = new List<SelectListItem>() { GetInitialSelectItem() };
            TM.ddPieceNo = new List<SelectListItem>() { GetInitialSelectItem() }; 
            TM.ddLocation2 = new List<SelectListItem>() { GetInitialSelectItem() };
           // TM.ddSubTestType = new List<SelectListItem>() { GetInitialSelectItem() };

            try
            {
                //alloy temper uacpart and cust part can be added directly to table - its not in UI.
                //if (!string.IsNullOrEmpty(WorkStudyID) && !string.IsNullOrEmpty(LotID) && !string.IsNullOrEmpty(TestType))
                if (!string.IsNullOrEmpty(WorkStudyID) && !string.IsNullOrEmpty(LotID))
                {
                    int MillLotNo = findMillLotNo(LotID);

                    //can be called during insert - as it is not in UI
                   // SqlParameter param0 = new SqlParameter("@MillLotNo", MillLotNo);
                   //// SqlParameter param1 = new SqlParameter("@testdesc", TestType);

                   // using (reader = ado.ExecDataReaderProc("RNDGetAlloyPartTemper", "RND", param0))
                   // {
                   //     if (reader.HasRows)
                   //     {
                   //         if (reader.Read())
                   //         {
                   //             TM.UACPart = Convert.ToDecimal(reader["UACPart"]);
                   //             TM.CustPart = Convert.ToString(reader["CustPart"]);
                   //             TM.Alloy = Convert.ToString(reader["Alloy"]);
                   //             TM.Temper = Convert.ToString(reader["Temper"]);
                   //         }                                                
                   //     }
                   // }

                    //populate the Location2 dropdown menu - after getting the LotID from the user.
                    SqlParameter param1 = new SqlParameter("@MillLotNo", MillLotNo);
                    SqlParameter param11 = new SqlParameter("@WorkStudyID", WorkStudyID);

                    using (reader = ado.ExecDataReaderProc("RNDGetLocation2", "RND", param1, param11))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TM.ddLocation2.Add(new SelectListItem
                                {
                                    Value = Convert.ToString(reader["Location2"]),
                                    Text = Convert.ToString(reader["Location2"]),
                                    Selected = (TM.Location2 == Convert.ToString(reader["Location2"])) ? true : false,
                                });
                            }
                        }
                    }
                       
                    // SoNum can be directly added in table - it not in UI.
                    //Hole Piece No and Loc 2 - need to be drop down menus.

                    // SqlParameter param1 = new SqlParameter("@testdesc", TestType);

                    SqlParameter param2 = new SqlParameter("@ProcessID", LotID);
                    using (reader = ado.ExecDataReaderProc("RNDGetHole", "RND", param2))
                    {
                        
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TM.ddHole.Add(new SelectListItem
                                {
                                    Value = Convert.ToString(reader["Hole"]),
                                    Text = Convert.ToString(reader["Hole"]),
                                    Selected = (TM.Hole == Convert.ToString(reader["Hole"])) ? true : false,
                                });
                            }
                        }                        
                    }
                    SqlParameter param3 = new SqlParameter("@ProcessID", LotID);
                    using (reader = ado.ExecDataReaderProc("RNDGetPieceNo", "RND", param3))
                    {

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TM.ddPieceNo.Add(new SelectListItem
                                {
                                    Value = Convert.ToString(reader["PieceNo"]),
                                    Text = Convert.ToString(reader["PieceNo"]),
                                    Selected = (TM.PieceNo == Convert.ToString(reader["PieceNo"])) ? true : false,
                                });
                            }
                        }
                    }
                    //using (reader = ado.ExecDataReaderProc("RNDsubTestType_READ", "RND", param1))
                    //{
                    //    if (reader.HasRows)
                    //    {
                    //        while (reader.Read())
                    //        {
                    //            TM.ddSubTestType.Add(new SelectListItem
                    //            {
                    //                Value = Convert.ToString(reader["SubTestType"]),
                    //                Text = Convert.ToString(reader["SubTestType"]),
                    //                Selected = (TM.SubTestType == Convert.ToString(reader["SubTestType"])) ? true : false,
                    //            });
                    //            //TM.ddLocation2.Add(new SelectListItem
                    //            //{
                    //            //    Value = Convert.ToString(reader["Location2"]),
                    //            //    Text = Convert.ToString(reader["Location2"]),
                    //            //    Selected = (TM.Location2 == Convert.ToString(reader["Location2"])) ? true : false,
                    //            //});
                    //        }
                    //        if (reader.Read())
                    //        {
                    //            TM.UACPart = Convert.ToDecimal(reader["UACPart"]);
                    //            TM.Alloy = Convert.ToString(reader["Alloy"]);
                    //            TM.Temper = Convert.ToString(reader["Temper"]);
                    //        }
                    //    }
                    //}
                }
                return Serializer.ReturnContent(TM, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
            
        /// <summary>
        /// send TestType as parameter and return SubTestype - list - for dropdown
        /// </summary>
        /// <param name="TestType"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(int flag, int recID, string TestType)
        {            
            _logger.Debug("Testing Get with TestType Called");
            SqlDataReader reader = null;
            RNDTesting TM = null;

            CurrentUser user = ApiUser;
            TM = new RNDTesting();
            AdoHelper ado = new AdoHelper();

            TM.ddSubTestType = new List<SelectListItem>() { GetInitialSelectItem() };
            try
            {
                if (!string.IsNullOrEmpty(TestType))
                {
                    SqlParameter param0 = new SqlParameter("@TestType", TestType);
                    using (reader = ado.ExecDataReaderProc("RNDSubTestType_READ", "RND", param0))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TM.ddSubTestType.Add(new SelectListItem
                                {
                                    Value = Convert.ToString(reader["SubTestType"]),
                                    Text = Convert.ToString(reader["SubTestType"]),
                                    Selected = (TM.SubTestType == Convert.ToString(reader["SubTestType"])) ? true : false,
                                });
                            }
                        }
                    }
                }
             
                return Serializer.ReturnContent(TM, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

         //   public HttpResponseMessage Post(string SelectedTests)
        public HttpResponseMessage Get(string SelectedTests, string WorkStudyID)
        {
            AdoHelper ado = new AdoHelper();
            SqlDataReader reader = null;

            List<RNDTesting> lstTests = new List<RNDTesting>();
            List<SqlParameter> lstSqlParameter = new List<SqlParameter>();

            //if (SelectedTests == null)
            //    SelectedTests = "ALL";

            SqlParameter param0 = new SqlParameter("@TestingNos", SelectedTests);
            SqlParameter param1 = new SqlParameter("@WorkStudyID", @WorkStudyID);

            using (reader = ado.ExecDataReaderProc("RNDPrintTesting", "RND", param0, param1))
            {
                if (reader.HasRows)
                {
                    RNDTesting TM = null;
                    while (reader.Read())
                    {
                        TM = new RNDTesting();
                        TM.total = Convert.ToInt32(reader["total"]);
                        TM.TestingNo = Convert.ToInt32(reader["TestingNo"]);
                        TM.Alloy = Convert.ToString(reader["Alloy"]);
                        TM.GageThickness = Convert.ToString(reader["GageThickness"]);
                        TM.Hole = Convert.ToString(reader["Hole"]);
                        TM.Location1 = Convert.ToString(reader["Location1"]);
                        TM.Location2 = Convert.ToString(reader["Location2"]);
                        TM.Location3 = Convert.ToString(reader["Location3"]);
                        TM.LotID = Convert.ToString(reader["LotID"]);
                        TM.Orientation = Convert.ToString(reader["Orientation"]);
                        TM.PieceNo = Convert.ToString(reader["PieceNo"]);
                        TM.SpeciComment = Convert.ToString(reader["SpeciComment"]);
                        TM.TestType = Convert.ToString(reader["TestType"]);
                        TM.SubTestType = Convert.ToString(reader["SubTestType"]);
                        TM.Temper = Convert.ToString(reader["Temper"]);
                        TM.TestLab = Convert.ToString(reader["TestLab"]);
                        TM.UACPart = Convert.ToInt32(reader["UACPart"]);
                        TM.WorkStudyID = Convert.ToString(reader["WorkStudyID"]);
                        TM.Printed = Convert.ToChar(reader["Printed"]);
                        lstTests.Add(TM);
                    }
                }
            }
                DataSearch<RNDTesting> ds = new DataSearch<RNDTesting>
                {
                    items = lstTests,
                    total = (lstTests != null && lstTests.Count > 0) ? lstTests[0].total : 0
                };
            
            return Serializer.ReturnContent(ds, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);

        }

        public HttpResponseMessage Post(RNDTesting TestingMaterial)
        {
            SqlDataReader reader = null;
            try
            {
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();

                int MillLotNo = findMillLotNo(TestingMaterial.LotID);

                SqlParameter param31 = new SqlParameter("@MillLotNo", MillLotNo);
                using (reader = ado.ExecDataReaderProc("RNDGetAlloyPartTemper", "RND", param31))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            TestingMaterial.UACPart = Convert.ToDecimal(reader["UACPart"]);
                            TestingMaterial.CustPart = Convert.ToString(reader["CustPart"]);
                            TestingMaterial.Alloy = Convert.ToString(reader["Alloy"]);
                            TestingMaterial.Temper = Convert.ToString(reader["Temper"]);
                        }
                    }
                }

                SqlParameter param32 = new SqlParameter("@ProcessID", TestingMaterial.LotID);
                using (reader = ado.ExecDataReaderProc("RNDGetSoNumByProcessID", "RND", param32))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            TestingMaterial.SoNum = Convert.ToString(reader["Sonum"]);
                        }
                    }
                }

                if (TestingMaterial.Location2 != null)
                {
                    if (TestingMaterial.Location2.Trim() == "-1")
                        TestingMaterial.Location2 = "";
                }
                else
                    TestingMaterial.Location2 = "";

                if (TestingMaterial.GageThickness == null)
                        TestingMaterial.GageThickness = "";

                if (TestingMaterial.SpeciComment == null)
                        TestingMaterial.SpeciComment = "";

                SqlParameter param1 = new SqlParameter("@WorkStudyID", TestingMaterial.WorkStudyID);
                SqlParameter param2 = new SqlParameter("@LotID", TestingMaterial.LotID);
                SqlParameter param3 = new SqlParameter("@MillLotNo", MillLotNo);
                SqlParameter param4 = new SqlParameter("@SoNum", TestingMaterial.SoNum);
                SqlParameter param5 = new SqlParameter("@Hole", TestingMaterial.Hole);
                SqlParameter param6 = new SqlParameter("@PieceNo", TestingMaterial.PieceNo);
                SqlParameter param7 = new SqlParameter("@Alloy", TestingMaterial.Alloy);
                SqlParameter param8 = new SqlParameter("@Temper", TestingMaterial.Temper);
                SqlParameter param9 = new SqlParameter("@CustPart", TestingMaterial.CustPart);
                SqlParameter param10 = new SqlParameter("@UACPart", TestingMaterial.UACPart);
                SqlParameter param11 = new SqlParameter("@GageThickness", TestingMaterial.GageThickness);
                SqlParameter param12 = new SqlParameter("@Orientation", TestingMaterial.Orientation);
                SqlParameter param13 = new SqlParameter("@Location1", TestingMaterial.Location1);
                SqlParameter param14 = new SqlParameter("@Location2", TestingMaterial.Location2);
                SqlParameter param15 = new SqlParameter("@Location3", TestingMaterial.Location3);
                SqlParameter param16 = new SqlParameter("@SpeciComment", TestingMaterial.SpeciComment);
                SqlParameter param17 = new SqlParameter("@TestType", TestingMaterial.TestType);
                SqlParameter param18 = new SqlParameter("@SubTestType", TestingMaterial.SubTestType);
                SqlParameter param19 = new SqlParameter("@Status", TestingMaterial.Status);
                SqlParameter param20 = new SqlParameter("@Selected", TestingMaterial.Selected);
                SqlParameter param21 = new SqlParameter("@EntryDate", DateTime.Now);
                SqlParameter param22 = new SqlParameter("@EntryBy", user.UserId);
                SqlParameter param23 = new SqlParameter("@TestLab", TestingMaterial.TestLab);
                SqlParameter param24 = new SqlParameter("@Printed", TestingMaterial.Printed);
                SqlParameter param25 = new SqlParameter("@Replica", TestingMaterial.Replica);
                //SqlParameter param26 = new SqlParameter("@RCS", TestingMaterial.RCS);

                int ReplicaCount = 1;
                if (TestingMaterial.Replica != null)
                {
                    ReplicaCount = Convert.ToInt32(TestingMaterial.Replica);
                }
               
               
                SqlParameter param27 = new SqlParameter("@ReplicaCount", ReplicaCount);
                if (TestingMaterial.TestingNo > 0)
                {
                    SqlParameter param30 = new SqlParameter("TestingNo", TestingMaterial.TestingNo);

                    ado.ExecScalarProc("RNDTestingMaterial_Update", "RND", new object[] { param1, param2, param3,
                        param4, param5, param6, param7, param8, param9, param10, param11, param12,
                        param13, param14, param15, param16, param17,param18, param19, param20,
                        param21, param22, param23, param24, param30});
                }
                else
                {
                    ado.ExecScalarProc("RNDTestingMaterial_Insert", "RND", new object[] { param1, param2, param3,
                        param4, param5, param6, param7, param8, param9, param10, param11, param12,
                        param13, param14, param15, param16, param17,param18, param19, param20,
                        param21, param22, param23, param24, param25, param27});
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Serializer.ReturnContent(TestingMaterial, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }

        public HttpResponseMessage Delete(int id)
        {
            _logger.Debug("Testing Material Delete Called");
            try
            {
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                SqlParameter param1 = new SqlParameter("@TestingNo", id);
                ado.ExecScalarProc("RNDTesting_Delete", "RND", new object[] { param1 });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Serializer.ReturnContent(HttpStatusCode.OK, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }

        protected int findMillLotNo(string LotID)
        {
            string[] separatingChars = { "P", "-P" };
            string[] MillLotID = LotID.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
            int millLotNo = Convert.ToInt32(MillLotID[0]);

            return millLotNo;
        }



    }
}