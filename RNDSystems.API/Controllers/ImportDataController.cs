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
using RNDSystems.Models.TestViewModels;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Text;

namespace RNDSystems.API.Controllers
{
    public class ImportDataController : UnSecuredController
    {
        // GET: ImportData
        public HttpResponseMessage Get(string WorkStudyId)
        {
            _logger.Debug("ImportData Get Called");
            SqlDataReader reader = null;
            ImportDataViewModel ID = null;

            try
            {
                if (WorkStudyId == "none")
                {
                    CurrentUser user = ApiUser;
                    ID = new ImportDataViewModel();
                    AdoHelper ado = new AdoHelper();

                    ID.ddWorkStudyID = new List<SelectListItem>() { GetInitialSelectItem() };

                    // for Release 2 - to fill the drop down menu
                    //check if store proc - [RNDTestType_READ] can be used

                    //  ID.ddTestType = new List<SelectListItem>() { GetInitialSelectItem() };

                    // SqlParameter param1 = new SqlParameter("@WorkStudyId", PM.WorkStudyID);

                    //using (reader = ado.ExecDataReaderProc("RNDTestTypes_READfromRNDTesting", "RND"))
                    //{
                    //    if (reader.HasRows)
                    //    {
                    //        while (reader.Read())
                    //        {
                    //            string TestType = Convert.ToString(reader["TestType"]);
                    //            if ((TestType != " ") && (TestType != "") && (TestType != null))
                    //            {
                    //                ID.ddTestType.Add(new SelectListItem
                    //                {
                    //                    Value = TestType,
                    //                    Text = TestType,
                    //                    Selected = (ID.TestType == TestType) ? true : false,
                    //                });
                    //            }
                    //        }
                    //    }
                    using (reader = ado.ExecDataReaderProc("RNDTestWorkStudy_READ", "RND"))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string WorkStudyID = Convert.ToString(reader["WorkStudyID"]);
                                if ((WorkStudyID != " ") && (WorkStudyID != "") && (WorkStudyID != null))
                                {
                                    ID.ddWorkStudyID.Add(new SelectListItem
                                    {
                                        Value = WorkStudyID,
                                        Text = WorkStudyID,
                                        Selected = (ID.WorkStudyID == WorkStudyID) ? true : false,
                                    });
                                }
                            }
                        }
                    }
                }
                return Serializer.ReturnContent(ID, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        //public HttpResponseMessage Get (int TestNo)
        //{
        //    //for manual entry- Get values from RNDTesting based on the selected TestNo.    
        //Fill in the Data in ImportDataViewModel to send Web

        //}
        public HttpResponseMessage Post(ApiViewModel selectedTestType)
        {            
            bool isSuccess = true;
            string data = string.Empty;
            string TestType = selectedTestType.Message;
            string filePath = selectedTestType.Message1;
            //string custom = selectedTestType.Custom;

            //custom = "custom";
            //filePath = "s:";
            try
            {
                switch (TestType)
                {
                    case "Tension":
                        {
                            //implemented
                            if (filePath == "none")
                                filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Tension.csv";
                           // filePath = "\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\Modulus Tension.csv";
                            isSuccess = ImportTensionData(filePath);
                            break;
                        }
                    case "Compression":
                        {                           
                            if (filePath == "none")
                                filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Compression.csv";

                            //if (custom == "custom")
                            //    filePath = filePath + "\\TestImport.csv";

                            //  filePath = "S:\\TestImport.csv";
                            // filePath = "\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\Compression.csv";
                            isSuccess = ImportCompressionData(filePath);
                            break;
                        }
                    case "Bearing":
                        {
                            if (filePath == "none")
                                filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Bearing.csv";
                            //  filePath = "\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\Bearing.csv";
                            isSuccess = ImportBearingData(filePath);
                            break;
                        }
                    case "Shear":
                        {
                            if (filePath == "none")
                                filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Shear.csv";
                          //  filePath = "\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\Shear.csv";

                            isSuccess = ImportShearData(filePath);
                            break;
                        }
                    case "Notch Yield":
                        {
                            //implemented
                            isSuccess = ImportNotchYieldData(filePath);
                            break;
                        }
                    case "Residual Strength":
                        {
                            //implemented
                            isSuccess = ImportResidualStrengthData(filePath);
                            break;
                        }
                    case "Fracture Toughness":
                        {
                            if (filePath == "none")
                                filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Fracture Toughness.csv";
                            //filePath = "\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\Fracture Toughness.csv";

                            //implemented
                            isSuccess = ImportFractureToughnessData(filePath);
                            break;
                        }
                    case "Modulus Tension":
                        {
                            if (filePath == "none")
                                filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Fracture Toughness.csv";
                            // filePath = "\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\Fracture Toughness.csv";

                            isSuccess = ImportModulusTensionData(filePath);
                            break;
                        }
                    case "Modulus Compression":
                        {
                            if (filePath == "none")
                                filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Modulus Compression.csv";
                            //  filePath = "\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\Modulus Compression.csv";

                            isSuccess = ImportModulusCompressionData(filePath);
                            break;
                        }
                    case "Fatigue Testing":
                        {
                            if (filePath == "none")
                                filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Fatigue Testing.csv";
                            //  filePath = "\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\Fatigue Testing.csv";

                            isSuccess = ImportFatigueTestingData(filePath);
                            break;
                        }
                    default:
                        break;

                }
                return Serializer.ReturnContent(isSuccess, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
       }

        public List<TensionViewModel> listTensionData { get; set; }

        bool ImportTensionData(string filePath)
        {
            try
            {
                listTensionData = new List<TensionViewModel>();
                //tensionData 
                
                var textFieldParser = new TextFieldParser(new StringReader(File.ReadAllText(filePath)))
                {
                    Delimiters = new string[] { "," }
                };

                while (!textFieldParser.EndOfData)
                {
                    var entry = textFieldParser.ReadFields();
                    listTensionData.Add(new TensionViewModel()
                    {
                       // RecID = Convert.ToInt32(entry[0]),
                        WorkStudyID = Convert.ToString(entry[0]),
                        TestNo = Convert.ToInt32(entry[1]),
                        SubConduct = Convert.ToDecimal(entry[2]),
                        SurfConduct = Convert.ToDecimal(entry[3]),
                        FtuKsi = Convert.ToDecimal(entry[4]),
                        FtyKsi = Convert.ToDecimal(entry[5]),
                        eElongation = Convert.ToDecimal(entry[6]),
                        EModulusMpsi = Convert.ToDecimal(entry[7]),
                        SpeciComment = Convert.ToString(entry[8]),
                        Operator = Convert.ToString(entry[9]),
                        TestDate = Convert.ToString(entry[10]),
                        TestTime = Convert.ToString(entry[11])                       
                });
                }
                textFieldParser.Close();

                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                int introw = 0;
                while (introw<listTensionData.Count)
                {
                    SqlParameter param2 = new SqlParameter("@WorkStudyID", Convert.ToString(listTensionData[introw].WorkStudyID));
                    SqlParameter param3 = new SqlParameter("@TestNo", listTensionData[introw].TestNo);
                    SqlParameter param4 = new SqlParameter("@SubConduct", listTensionData[introw].SubConduct);
                    SqlParameter param5 = new SqlParameter("@SurfConduct", listTensionData[introw].SurfConduct);
                    SqlParameter param6 = new SqlParameter("@FtuKsi", listTensionData[introw].FtuKsi);
                    SqlParameter param7 = new SqlParameter("@FtyKsi", listTensionData[introw].FtyKsi);
                    SqlParameter param8 = new SqlParameter("@eElongation", listTensionData[introw].eElongation);
                    SqlParameter param9 = new SqlParameter("@EModulusMpsi", listTensionData[introw].EModulusMpsi);
                    SqlParameter param10 = new SqlParameter("@SpeciComment", listTensionData[introw].SpeciComment);
                    SqlParameter param11 = new SqlParameter("@Operator", listTensionData[introw].Operator);
                    SqlParameter param12 = new SqlParameter("@TestDate", listTensionData[introw].TestDate);
                    SqlParameter param13 = new SqlParameter("@TestTime", listTensionData[introw].TestTime);
                    SqlParameter param14 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param15 = new SqlParameter("@EntryDate", DateTime.Now);
                    SqlParameter param16 = new SqlParameter("@Completed", '1');

                    using (SqlDataReader reader = ado.ExecDataReaderProc("RNDTension_Insert", "RND", new object[] 
                    {  param2,param3, param4, param5, param6, param7, param8, param9, param10,
                        param11, param12, param13, param14, param15, param16}))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listTensionData[introw].RecID = Convert.ToInt32(reader["RecId"].ToString());
                            }
                        }
                    }
                    introw++;
                }
                              
                return true;
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }           
        }

        public List<CompressionViewModel> listCompressionData { get; set; }

        bool ImportCompressionData(string filePath)
        {
            try
            {
                listCompressionData = new List<CompressionViewModel>();
                // CompressionData 
               
                string fileValue = File.ReadAllText(filePath);


                var textFieldParser = new TextFieldParser(new StringReader(File.ReadAllText(filePath)))
                {
                    Delimiters = new string[] { "," }
                };

                while (!textFieldParser.EndOfData)
                {
                    var entry = textFieldParser.ReadFields();
                    listCompressionData.Add(new CompressionViewModel()
                    {
                        // RecID = Convert.ToInt32(entry[0]),
                        WorkStudyID = Convert.ToString(entry[0]),
                        TestNo = Convert.ToInt32(entry[1]),
                        SubConduct = Convert.ToDecimal(entry[2]),
                        SurfConduct = Convert.ToDecimal(entry[3]),
                        FcyKsi = Convert.ToDecimal(entry[4]),
                        EcModulusMpsi = Convert.ToDecimal(entry[5]),
                        SpeciComment = Convert.ToString(entry[6]),
                        Operator = Convert.ToString(entry[7]),
                        TestDate = Convert.ToString(entry[8]),
                        TestTime = Convert.ToString(entry[9])
                    });
                }
                textFieldParser.Close();

                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                int introw = 0;
                while (introw < listCompressionData.Count)
                {
                    SqlParameter param2 = new SqlParameter("@WorkStudyID", listCompressionData[introw].WorkStudyID);
                    SqlParameter param3 = new SqlParameter("@TestNo", listCompressionData[introw].TestNo);
                    SqlParameter param4 = new SqlParameter("@SubConduct", listCompressionData[introw].SubConduct);
                    SqlParameter param5 = new SqlParameter("@SurfConduct", listCompressionData[introw].SurfConduct);
                    SqlParameter param6 = new SqlParameter("@FcyKsi", listCompressionData[introw].FcyKsi);
                    SqlParameter param7 = new SqlParameter("@EcModulusMpsi", listCompressionData[introw].EcModulusMpsi);
                    SqlParameter param8 = new SqlParameter("@SpeciComment", listCompressionData[introw].SpeciComment);
                    SqlParameter param9 = new SqlParameter("@Operator", listCompressionData[introw].Operator);
                    SqlParameter param10 = new SqlParameter("@TestDate", listCompressionData[introw].TestDate);
                    SqlParameter param11 = new SqlParameter("@TestTime", listCompressionData[introw].TestTime);
                    SqlParameter param12 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param13 = new SqlParameter("@EntryDate", DateTime.Now);
                    SqlParameter param14 = new SqlParameter("@Completed", '1');

                    using (SqlDataReader reader = ado.ExecDataReaderProc("RNDCompression_Insert", "RND", new object[]
                    {  param2,param3, param4, param5, param6, param7, param8, param9, param10,
                        param11, param12, param13, param14}))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listCompressionData[introw].RecID = Convert.ToInt32(reader["RecId"].ToString());
                            }
                        }
                    }
                    introw++;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }
        }

      
        public List<BearingViewModel> listBearingData { get; set; }

        bool ImportBearingData(string filePath)
        {
            try
            {
                listBearingData = new List<BearingViewModel>();
                // Bearing Data 
         
                var textFieldParser = new TextFieldParser(new StringReader(File.ReadAllText(filePath)))
                {
                    Delimiters = new string[] { "," }
                };

                while (!textFieldParser.EndOfData)
                {
                    var entry = textFieldParser.ReadFields();
                    listBearingData.Add(new BearingViewModel()
                    {
                        // RecID = Convert.ToInt32(entry[0]),
                        WorkStudyID = Convert.ToString(entry[0]),
                        TestNo = Convert.ToInt32(entry[1]),
                        SubConduct = Convert.ToDecimal(entry[2]),
                        SurfConduct = Convert.ToDecimal(entry[3]),
                        eDCalc = Convert.ToDecimal(entry[4]),
                        FbruKsi = Convert.ToDecimal(entry[5]),
                        FbryKsi = Convert.ToDecimal(entry[6]),
                        SpeciComment = Convert.ToString(entry[7]),
                        Operator = Convert.ToString(entry[8]),
                        TestDate = Convert.ToString(entry[9]),
                        TestTime = Convert.ToString(entry[10])
                    });
                }
                textFieldParser.Close();

                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                int introw = 0;
                while (introw < listBearingData.Count)
                {
                    SqlParameter param2 = new SqlParameter("@WorkStudyID", listBearingData[introw].WorkStudyID);
                    SqlParameter param3 = new SqlParameter("@TestNo", listBearingData[introw].TestNo);
                    SqlParameter param4 = new SqlParameter("@SubConduct", listBearingData[introw].SubConduct);
                    SqlParameter param5 = new SqlParameter("@SurfConduct", listBearingData[introw].SurfConduct);
                    SqlParameter param6 = new SqlParameter("@eDCalc", listBearingData[introw].eDCalc);
                    SqlParameter param7 = new SqlParameter("@FbruKsi", listBearingData[introw].FbruKsi);
                    SqlParameter param8 = new SqlParameter("@FbryKsi", listBearingData[introw].FbryKsi);
                    SqlParameter param9 = new SqlParameter("@SpeciComment", listBearingData[introw].SpeciComment);                  
                    SqlParameter param10 = new SqlParameter("@Operator", listBearingData[introw].Operator);
                    SqlParameter param11 = new SqlParameter("@TestDate", listBearingData[introw].TestDate);
                    SqlParameter param12 = new SqlParameter("@TestTime", listBearingData[introw].TestTime);
                    SqlParameter param13 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param14 = new SqlParameter("@EntryDate", DateTime.Now);
                    SqlParameter param15 = new SqlParameter("@Completed", '1');

                    using (SqlDataReader reader = ado.ExecDataReaderProc("RNDBearing_Insert", "RND", new object[]
                    {  param2,param3, param4, param5, param6, param7, param8, param9, param10,
                        param11, param12, param13, param14, param15}))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listBearingData[introw].RecID = Convert.ToInt32(reader["RecId"].ToString());
                            }
                        }
                    }
                    introw++;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }
        }
    
        public List<ShearViewModel> listShearData { get; set; }

        bool ImportShearData(string filePath)
        {
            try
            {
                listShearData = new List<ShearViewModel>();
                // Shear Data 
                
                var textFieldParser = new TextFieldParser(new StringReader(File.ReadAllText(filePath)))
                {
                    Delimiters = new string[] { "," }
                };

                while (!textFieldParser.EndOfData)
                {
                    var entry = textFieldParser.ReadFields();
                    listShearData.Add(new ShearViewModel()
                    {
                        // RecID = Convert.ToInt32(entry[0]),
                        WorkStudyID = Convert.ToString(entry[0]),
                        TestNo = Convert.ToInt32(entry[1]),
                        SubConduct = Convert.ToDecimal(entry[2]),
                        SurfConduct = Convert.ToDecimal(entry[3]),
                        FsuKsi = Convert.ToDecimal(entry[4]),                       
                        SpeciComment = Convert.ToString(entry[5]),
                        Operator = Convert.ToString(entry[6]),
                        TestDate = Convert.ToString(entry[7]),
                        TestTime = Convert.ToString(entry[8])
                    });
                }
                textFieldParser.Close();

                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                int introw = 0;
                while (introw < listShearData.Count)
                {
                    SqlParameter param2 = new SqlParameter("@WorkStudyID", listShearData[introw].WorkStudyID);
                    SqlParameter param3 = new SqlParameter("@TestNo", listShearData[introw].TestNo);
                    SqlParameter param4 = new SqlParameter("@SubConduct", listShearData[introw].SubConduct);
                    SqlParameter param5 = new SqlParameter("@SurfConduct", listShearData[introw].SurfConduct);
                    SqlParameter param6 = new SqlParameter("@FsuKsi", listShearData[introw].FsuKsi);                   
                    SqlParameter param7 = new SqlParameter("@SpeciComment", listShearData[introw].SpeciComment);
                    SqlParameter param8 = new SqlParameter("@Operator", listShearData[introw].Operator);
                    SqlParameter param9 = new SqlParameter("@TestDate", listShearData[introw].TestDate);
                    SqlParameter param10 = new SqlParameter("@TestTime", listShearData[introw].TestTime);
                    SqlParameter param11 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param12 = new SqlParameter("@EntryDate", DateTime.Now);
                    SqlParameter param13 = new SqlParameter("@Completed", '1');

                    using (SqlDataReader reader = ado.ExecDataReaderProc("RNDShear_Insert", "RND", new object[]
                    {  param2,param3, param4, param5, param6, param7, param8, param9, param10,
                        param11, param12, param13}))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listShearData[introw].RecID = Convert.ToInt32(reader["RecId"].ToString());
                            }
                        }
                    }
                    introw++;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }
        }



        public List<NotchYieldViewModel> listNotchYieldData { get; set; }
        bool ImportNotchYieldData(string filePath)
        {
            try
            {
                listNotchYieldData = new List<NotchYieldViewModel>();
                // FractureToughness
                var textFieldParser = new TextFieldParser(new StringReader(File.ReadAllText(filePath)))
                {
                    Delimiters = new string[] { "," }
                };

                while (!textFieldParser.EndOfData)
                {
                    var entry = textFieldParser.ReadFields();
                    if (entry[0] != "WorkStudyID" && entry[0] != "")
                    {
                        listNotchYieldData.Add(new NotchYieldViewModel()
                        {
                            // RecID = Convert.ToInt32(entry[0]),
                            WorkStudyID = Convert.ToString(entry[0]),
                            TestNo = Convert.ToInt32(entry[1]),
                            SubConduct = Convert.ToDecimal(entry[2]),
                            SurfConduct = Convert.ToDecimal(entry[3]),
                            NotchStrengthKsi = Convert.ToDecimal(entry[4]),
                            YieldStrengthKsi = Convert.ToDecimal(entry[5]),
                            NotchYieldRatio = Convert.ToDecimal(entry[6]),                            
                            SpeciComment = Convert.ToString(entry[7]),
                            Operator = Convert.ToString(entry[8]),
                            TestDate = Convert.ToString(entry[9]),
                            TestTime = Convert.ToString(entry[10])
                        });
                    }
                }
                textFieldParser.Close();

                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                int introw = 0;
                while (introw < listNotchYieldData.Count)
                {
                    SqlParameter param2 = new SqlParameter("@WorkStudyID", listNotchYieldData[introw].WorkStudyID);
                    SqlParameter param3 = new SqlParameter("@TestNo", listNotchYieldData[introw].TestNo);
                    SqlParameter param4 = new SqlParameter("@SubConduct", listNotchYieldData[introw].SubConduct);
                    SqlParameter param5 = new SqlParameter("@SurfConduct", listNotchYieldData[introw].SurfConduct);
                    SqlParameter param6 = new SqlParameter("@NotchStrengthKsi", listNotchYieldData[introw].NotchStrengthKsi);
                    SqlParameter param7 = new SqlParameter("@YieldStrengthKsi", listNotchYieldData[introw].YieldStrengthKsi);
                    SqlParameter param8 = new SqlParameter("@NotchYieldRatio", listNotchYieldData[introw].NotchYieldRatio);                   
                    SqlParameter param9 = new SqlParameter("@SpeciComment", listNotchYieldData[introw].SpeciComment);
                    SqlParameter param10 = new SqlParameter("@Operator", listNotchYieldData[introw].Operator);
                    SqlParameter param11 = new SqlParameter("@TestDate", listNotchYieldData[introw].TestDate);
                    SqlParameter param12 = new SqlParameter("@TestTime", listNotchYieldData[introw].TestTime);
                    SqlParameter param13 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param14 = new SqlParameter("@EntryDate", DateTime.Now);
                    SqlParameter param15 = new SqlParameter("@Completed", '1');

                    using (SqlDataReader reader = ado.ExecDataReaderProc("RNDNotchYield_Insert", "RND", new object[]
                    {  param2,param3, param4, param5, param6, param7, param8, param9, param10,
                        param11, param12, param13, param14, param15}))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listNotchYieldData[introw].RecID = Convert.ToInt32(reader["RecId"].ToString());
                            }
                        }
                    }
                    introw++;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// ResidualStrength
        /// </summary>
        /// <returns></returns>
       public List<ResidualStrengthViewModel> listResidualStrengthData { get; set; }

        bool ImportResidualStrengthData(string filePath)
        {
            try
            {
                listResidualStrengthData = new List<ResidualStrengthViewModel>();
                // ResidualStrength
               
                var textFieldParser = new TextFieldParser(new StringReader(File.ReadAllText(filePath)))
                {
                    Delimiters = new string[] { "," }
                };

                while (!textFieldParser.EndOfData)
                {
                    var entry = textFieldParser.ReadFields();
                    if (entry[0] != "WorkStudyID" && entry[0] != "")
                        {
                        listResidualStrengthData.Add(new ResidualStrengthViewModel()
                        {
                            // RecID = Convert.ToInt32(entry[0]),
                            WorkStudyID = Convert.ToString(entry[0]),
                            TestNo = Convert.ToInt32(entry[1]),
                            SubConduct = Convert.ToDecimal(entry[2]),
                            SurfConduct = Convert.ToDecimal(entry[3]),
                            Validity = Convert.ToString(entry[4]),
                            ResidualStrength = Convert.ToDecimal(entry[5]),
                            PmaxLBS = Convert.ToDecimal(entry[6]),
                            WIn = Convert.ToDecimal(entry[7]),
                            BIn = Convert.ToDecimal(entry[8]),
                            AvgFinalPreCrack = Convert.ToDecimal(entry[9]),                            
                            SpeciComment = Convert.ToString(entry[10]),
                            Operator = Convert.ToString(entry[11]),
                            TestDate = Convert.ToString(entry[12]),
                            TestTime = Convert.ToString(entry[13])                          
                        });
                    }
                }
                textFieldParser.Close();

                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                int introw = 0;
                while (introw < listResidualStrengthData.Count)
                {
                    SqlParameter param2 = new SqlParameter("@WorkStudyID", listResidualStrengthData[introw].WorkStudyID);
                    SqlParameter param3 = new SqlParameter("@TestNo", listResidualStrengthData[introw].TestNo);
                    SqlParameter param4 = new SqlParameter("@SubConduct", listResidualStrengthData[introw].SubConduct);
                    SqlParameter param5 = new SqlParameter("@SurfConduct", listResidualStrengthData[introw].SurfConduct);
                    SqlParameter param6 = new SqlParameter("@Validity", listResidualStrengthData[introw].Validity);
                    SqlParameter param7 = new SqlParameter("@ResidualStrength", listResidualStrengthData[introw].ResidualStrength);
                    SqlParameter param8 = new SqlParameter("@PmaxLBS", listResidualStrengthData[introw].PmaxLBS);
                    SqlParameter param9 = new SqlParameter("@WIn", listResidualStrengthData[introw].WIn);
                    SqlParameter param10 = new SqlParameter("@BIn", listResidualStrengthData[introw].BIn);
                    SqlParameter param11 = new SqlParameter("@AvgFinalPreCrack", listResidualStrengthData[introw].AvgFinalPreCrack);                    
                    SqlParameter param12 = new SqlParameter("@SpeciComment", listResidualStrengthData[introw].SpeciComment);
                    SqlParameter param13 = new SqlParameter("@Operator", listResidualStrengthData[introw].Operator);
                    SqlParameter param14 = new SqlParameter("@TestDate", listResidualStrengthData[introw].TestDate);
                    SqlParameter param15 = new SqlParameter("@TestTime", listResidualStrengthData[introw].TestDate);
                    SqlParameter param16 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param17 = new SqlParameter("@EntryDate", DateTime.Now);
                    SqlParameter param18 = new SqlParameter("@Completed", '1');

                    using (SqlDataReader reader = ado.ExecDataReaderProc("RNDResidualStrength_Insert", "RND", new object[]
                    {  param2,param3, param4, param5, param6, param7, param8, param9, param10,
                        param11, param12, param13, param14, param15, param16, param17, param18
                       }))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listResidualStrengthData[introw].RecID = Convert.ToInt32(reader["RecId"].ToString());
                            }
                        }
                    }
                    introw++;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Fracture Toughness
        /// </summary>
        public List<FractureToughnessViewModel> listFractureToughnessData { get; set; }
       
        bool ImportFractureToughnessData(string filePath)
        {
            try
            {
                listFractureToughnessData = new List<FractureToughnessViewModel>();
                // FractureToughness
           
                var textFieldParser = new TextFieldParser(new StringReader(File.ReadAllText(filePath)))
                {
                    Delimiters = new string[] { "," }
                };

                while (!textFieldParser.EndOfData)
                {
                    var entry = textFieldParser.ReadFields();
                    if (entry[0]!="")
                    {
                        listFractureToughnessData.Add(new FractureToughnessViewModel()
                        {
                            // RecID = Convert.ToInt32(entry[0]),
                            WorkStudyID = Convert.ToString(entry[0]),
                            TestNo = Convert.ToInt32(entry[1]),
                            SubConduct = Convert.ToDecimal(entry[2]),
                            SurfConduct = Convert.ToDecimal(entry[3]),
                            Validity = Convert.ToString(entry[4]),
                            KKsiIn = Convert.ToDecimal(entry[5]),
                            KmaxKsiIn = Convert.ToDecimal(entry[6]),
                            PqLBS = Convert.ToDecimal(entry[7]),
                            PmaxLBS = Convert.ToDecimal(entry[8]),
                            aOIn = Convert.ToDecimal(entry[9]),
                            WIn = Convert.ToDecimal(entry[10]),
                            BIn = Convert.ToDecimal(entry[11]),
                            BnIn = Convert.ToDecimal(entry[12]),
                            AvgFinalPreCrack = Convert.ToDecimal(entry[13]),
                            SpeciComment = Convert.ToString(entry[14]),
                            Operator = Convert.ToString(entry[15]),
                            TestDate = Convert.ToString(entry[16]),
                            EntryDate = Convert.ToString(entry[17]),
                            blank1 = Convert.ToString(entry[18]),
                            blank2 = Convert.ToString(entry[19]),
                            blank3 = Convert.ToString(entry[20]),
                            blank4 = Convert.ToString(entry[21]),
                        });
                    }                    
                }
                textFieldParser.Close();

                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                int introw = 0;
                while (introw < listFractureToughnessData.Count)
                {
                    SqlParameter param2 = new SqlParameter("@WorkStudyID", listFractureToughnessData[introw].WorkStudyID);
                    SqlParameter param3 = new SqlParameter("@TestNo", listFractureToughnessData[introw].TestNo);
                    SqlParameter param4 = new SqlParameter("@SubConduct", listFractureToughnessData[introw].SubConduct);
                    SqlParameter param5 = new SqlParameter("@SurfConduct", listFractureToughnessData[introw].SurfConduct);
                    SqlParameter param6 = new SqlParameter("@Validity", listFractureToughnessData[introw].Validity);
                    SqlParameter param7 = new SqlParameter("@KKsiIn", listFractureToughnessData[introw].KKsiIn);
                    SqlParameter param8 = new SqlParameter("@KmaxKsiIn", listFractureToughnessData[introw].KmaxKsiIn);
                    SqlParameter param9 = new SqlParameter("@PqLBS", listFractureToughnessData[introw].PqLBS);
                    SqlParameter param10 = new SqlParameter("@PmaxLBS", listFractureToughnessData[introw].PmaxLBS);
                    SqlParameter param11 = new SqlParameter("@aOIn", listFractureToughnessData[introw].aOIn);
                    SqlParameter param12 = new SqlParameter("@WIn", listFractureToughnessData[introw].WIn);
                    SqlParameter param13 = new SqlParameter("@BIn", listFractureToughnessData[introw].BIn);
                    SqlParameter param14 = new SqlParameter("@BnIn", listFractureToughnessData[introw].BnIn);
                    SqlParameter param15 = new SqlParameter("@AvgFinalPreCrack", listFractureToughnessData[introw].AvgFinalPreCrack);                   
                    SqlParameter param16 = new SqlParameter("@SpeciComment", listFractureToughnessData[introw].SpeciComment);
                    SqlParameter param17 = new SqlParameter("@Operator", listFractureToughnessData[introw].Operator);
                    SqlParameter param18 = new SqlParameter("@TestDate", listFractureToughnessData[introw].TestDate);
                    SqlParameter param19 = new SqlParameter("@TestTime", "");
                    SqlParameter param20 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param21 = new SqlParameter("@EntryDate", DateTime.Now);
                    SqlParameter param22 = new SqlParameter("@Completed", '1');

                    using (SqlDataReader reader = ado.ExecDataReaderProc("RNDFractureToughness_Insert", "RND", new object[]
                    {  param2,param3, param4, param5, param6, param7, param8, param9, param10,
                        param11, param12, param13, param14, param15, param16, param17, param18,
                        param19, param20, param21, param22}))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listFractureToughnessData[introw].RecID = Convert.ToInt32(reader["RecId"].ToString());
                            }
                        }
                    }
                    introw++;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }
        }
       

        public List<ModulusTensionDataViewModel> listModulusTensionData { get; set; }

        bool ImportModulusTensionData(string filePath)
        {
            try
            {
                listModulusTensionData = new List<ModulusTensionDataViewModel>();
                // ModulusTensionData
           
                var textFieldParser = new TextFieldParser(new StringReader(File.ReadAllText(filePath)))
                {
                    Delimiters = new string[] { "," }
                };

                while (!textFieldParser.EndOfData)
                {
                    var entry = textFieldParser.ReadFields();
                    if (entry[0] != "")
                    {
                        listModulusTensionData.Add(new ModulusTensionDataViewModel()
                        {
                            // RecID = Convert.ToInt32(entry[0]),
                            WorkStudyID = Convert.ToString(entry[0]),
                            TestNo = Convert.ToInt32(entry[1]),
                            SubConduct = Convert.ToDecimal(entry[2]),
                            SurfConduct = Convert.ToDecimal(entry[3]),
                            EModulusTension = Convert.ToDecimal(entry[4]),                            
                            SpeciComment = Convert.ToString(entry[5]),
                            Operator = Convert.ToString(entry[6]),
                            TestDate = Convert.ToString(entry[7]),
                            EntryDate = Convert.ToString(entry[8])
                        });
                    }
                }
                textFieldParser.Close();

                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                int introw = 0;
                while (introw < listModulusTensionData.Count)
                {
                    SqlParameter param2 = new SqlParameter("@WorkStudyID", listModulusTensionData[introw].WorkStudyID);
                    SqlParameter param3 = new SqlParameter("@TestNo", listModulusTensionData[introw].TestNo);
                    SqlParameter param4 = new SqlParameter("@SubConduct", listModulusTensionData[introw].SubConduct);
                    SqlParameter param5 = new SqlParameter("@SurfConduct", listModulusTensionData[introw].SurfConduct);
                    SqlParameter param6 = new SqlParameter("@EModulusTension", listModulusTensionData[introw].EModulusTension);                   
                    SqlParameter param7 = new SqlParameter("@SpeciComment", listModulusTensionData[introw].SpeciComment);
                    SqlParameter param8 = new SqlParameter("@Operator", listModulusTensionData[introw].Operator);
                    SqlParameter param9 = new SqlParameter("@TestDate", listModulusTensionData[introw].TestDate);
                    SqlParameter param10 = new SqlParameter("@TestTime", "");
                    SqlParameter param11 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param12 = new SqlParameter("@EntryDate", DateTime.Now);
                    SqlParameter param13 = new SqlParameter("@Completed", '1');

                    using (SqlDataReader reader = ado.ExecDataReaderProc("RNDModulusTension_Insert", "RND", new object[]
                    {  param2,param3, param4, param5, param6, param7, param8, param9, param10,
                        param11, param12, param13}))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listModulusTensionData[introw].RecID = Convert.ToInt32(reader["RecId"].ToString());
                            }
                        }
                    }
                    introw++;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }
        }


        public List<ModulusCompressionDataViewModel> listModulusCompressionData { get; set; }

        bool ImportModulusCompressionData(string filePath)
        {
            try
            {
                listModulusCompressionData = new List<ModulusCompressionDataViewModel>();
                // ModulusCompressionData
              //  string filePath = "C:\\New Development\\RND\\New Development\\CSV\\Modulus Compression.csv";

                var textFieldParser = new TextFieldParser(new StringReader(File.ReadAllText(filePath)))
                {
                    Delimiters = new string[] { "," }
                };

                while (!textFieldParser.EndOfData)
                {
                    var entry = textFieldParser.ReadFields();
                    if (entry[0] != "")
                    {
                        if (entry[4] == "****")
                        {
                            entry[4] = "0.0";
                        } 
                            listModulusCompressionData.Add(new ModulusCompressionDataViewModel()
                        {
                            // RecID = Convert.ToInt32(entry[0]),
                            WorkStudyID = Convert.ToString(entry[0]),
                            TestNo = Convert.ToInt32(entry[1]),
                            SubConduct = Convert.ToDecimal(entry[2]),
                            SurfConduct = Convert.ToDecimal(entry[3]),                          
                             EModulusCompression = Convert.ToDecimal(entry[4]),                              
                            SpeciComment = Convert.ToString(entry[5]),
                            Operator = Convert.ToString(entry[6]),
                            TestDate = Convert.ToString(entry[7]),
                            EntryDate = Convert.ToString(entry[8])
                        });
                    }
                }
                textFieldParser.Close();

                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                int introw = 0;
                while (introw < listModulusCompressionData.Count)
                {
                    SqlParameter param2 = new SqlParameter("@WorkStudyID", listModulusCompressionData[introw].WorkStudyID);
                    SqlParameter param3 = new SqlParameter("@TestNo", listModulusCompressionData[introw].TestNo);
                    SqlParameter param4 = new SqlParameter("@SubConduct", listModulusCompressionData[introw].SubConduct);
                    SqlParameter param5 = new SqlParameter("@SurfConduct", listModulusCompressionData[introw].SurfConduct);
                    SqlParameter param6 = new SqlParameter("@EModulusCompression", listModulusCompressionData[introw].EModulusCompression);
                    SqlParameter param7 = new SqlParameter("@SpeciComment", listModulusCompressionData[introw].SpeciComment);
                    SqlParameter param8 = new SqlParameter("@Operator", listModulusCompressionData[introw].Operator);
                    SqlParameter param9 = new SqlParameter("@TestDate", listModulusCompressionData[introw].TestDate);
                    SqlParameter param10 = new SqlParameter("@TestTime", "");
                    SqlParameter param11 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param12 = new SqlParameter("@EntryDate", DateTime.Now);
                    SqlParameter param13 = new SqlParameter("@Completed", '1');

                    using (SqlDataReader reader = ado.ExecDataReaderProc("RNDModulusCompression_Insert", "RND", new object[]
                    {  param2,param3, param4, param5, param6, param7, param8, param9, param10,
                        param11, param12, param13}))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listModulusCompressionData[introw].RecID = Convert.ToInt32(reader["RecId"].ToString());
                            }
                        }
                    }
                    introw++;
                }
                //SqlParameter param1 = new SqlParameter("@RecID", tension.RecID);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }
        }

        public List<FatigueTestingDataViewModel> listFatigueTestingData { get; set; }

        bool ImportFatigueTestingData(string filePath)
        {
            try
            {
                listFatigueTestingData = new List<FatigueTestingDataViewModel>();
                // FatigueTestingData
           
                var textFieldParser = new TextFieldParser(new StringReader(File.ReadAllText(filePath)))
                {
                    Delimiters = new string[] { "," }
                };

                while (!textFieldParser.EndOfData)
                {
                    var entry = textFieldParser.ReadFields();
                    if (entry[0] != "")
                    {
                        if (entry[13] == "")
                        {
                            entry[13] = "0.0";
                        }

                        listFatigueTestingData.Add(new FatigueTestingDataViewModel()
                        {
                            WorkStudyID = Convert.ToString(entry[0]),
                            TestNo = Convert.ToInt32(entry[1]),
                            SpecimenDrawing = Convert.ToString(entry[2]),
                            MinStress = Convert.ToDecimal(entry[3]),
                            MaxStress = Convert.ToDecimal(entry[4]),
                            MinLoad = Convert.ToDecimal(entry[5]),
                            MaxLoad = Convert.ToDecimal(entry[6]),
                            WidthOrDia = Convert.ToDecimal(entry[7]),
                            Thickness = Convert.ToDecimal(entry[8]),
                            HoleDia = Convert.ToDecimal(entry[9]),
                            AvgChamferDepth = Convert.ToDecimal(entry[10]),
                            Frequency = Convert.ToString(entry[11]),
                            CyclesToFailure = Convert.ToDecimal(entry[12]),
                            Roughness = Convert.ToDecimal(entry[13]),
                            TestFrame = Convert.ToString(entry[14]),
                            Comment = Convert.ToString(entry[15]),
                            FractureLocation = Convert.ToString(entry[16]),
                            Operator = Convert.ToString(entry[17]),
                            TestDate = Convert.ToString(entry[18]),
                            EntryDate = Convert.ToString(entry[19]),
                            field1 = Convert.ToString(entry[20])
                        });
                    }
                }
                textFieldParser.Close();

                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                int introw = 0;
                while (introw < listFatigueTestingData.Count)
                {
                    SqlParameter param2 = new SqlParameter("@WorkStudyID", listFatigueTestingData[introw].WorkStudyID);
                    SqlParameter param3 = new SqlParameter("@TestNo", listFatigueTestingData[introw].TestNo);
                    SqlParameter param4 = new SqlParameter("@SpecimenDrawing", listFatigueTestingData[introw].SpecimenDrawing);
                    SqlParameter param5 = new SqlParameter("@MinStress", listFatigueTestingData[introw].MinStress);
                    SqlParameter param6 = new SqlParameter("@MaxStress", listFatigueTestingData[introw].MaxStress);
                    SqlParameter param7 = new SqlParameter("@MinLoad", listFatigueTestingData[introw].MinLoad);
                    SqlParameter param8 = new SqlParameter("@MaxLoad", listFatigueTestingData[introw].MaxLoad);
                    SqlParameter param9 = new SqlParameter("@WidthOrDia", listFatigueTestingData[introw].WidthOrDia);
                    SqlParameter param10 = new SqlParameter("@Thickness", listFatigueTestingData[introw].Thickness);
                    SqlParameter param11 = new SqlParameter("@HoleDia", listFatigueTestingData[introw].HoleDia);
                    SqlParameter param12 = new SqlParameter("@AvgChamferDepth", listFatigueTestingData[introw].AvgChamferDepth);
                    SqlParameter param13 = new SqlParameter("@Frequency", listFatigueTestingData[introw].Frequency);
                    SqlParameter param14 = new SqlParameter("@CyclesToFailure", listFatigueTestingData[introw].CyclesToFailure);
                    SqlParameter param15 = new SqlParameter("@Roughness", listFatigueTestingData[introw].Roughness);
                    SqlParameter param16 = new SqlParameter("@TestFrame", listFatigueTestingData[introw].TestFrame);
                    SqlParameter param17 = new SqlParameter("@Comment", listFatigueTestingData[introw].Comment);
                    SqlParameter param18 = new SqlParameter("@FractureLocation", listFatigueTestingData[introw].FractureLocation);
                    SqlParameter param19 = new SqlParameter("@Operator", listFatigueTestingData[introw].Operator);
                    SqlParameter param20 = new SqlParameter("@TestDate", listFatigueTestingData[introw].TestDate);
                    SqlParameter param21 = new SqlParameter("@TestTime", "");
                    SqlParameter param22 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param23 = new SqlParameter("@EntryDate", DateTime.Now);
                    SqlParameter param24 = new SqlParameter("@Completed", '1');


                    using (SqlDataReader reader = ado.ExecDataReaderProc("RNDFatigueTesting_Insert", "RND", new object[]
                    {  param2,param3, param4, param5, param6, param7, param8, param9, param10,
                        param11, param12, param13, param14, param15, param16, param17, param18, param19,
                        param20, param21, param22, param23, param24}))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listFatigueTestingData[introw].RecID = Convert.ToInt32(reader["RecId"].ToString());
                            }
                        }
                    }
                    introw++;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }
        }

    }
}