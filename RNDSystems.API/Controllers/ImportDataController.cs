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
using Newtonsoft.Json;
using System.Web;

namespace RNDSystems.API.Controllers
{
    public class ImportDataController : UnSecuredController
    {
        // GET: ImportData
        // public HttpResponseMessage Get(string WorkStudyId)
        public HttpResponseMessage Get(string Active, string SelectedTests="none")
        {
            _logger.Debug("ImportData Get Called");
          //  SqlDataReader reader = null;
            ImportDataViewModel ID = null;

            try
            {
                 CurrentUser user = ApiUser;
                  ID = new ImportDataViewModel();
                AdoHelper ado = new AdoHelper();

                 ID.ddTestType = new List<SelectListItem>() { GetInitialSelectItem() };

                //Active = 2 for Import and Active = 3 for Manual Entry
                if (Active == "2")
                {
                    #region  IMPORT
                    SqlParameter param1 = new SqlParameter("@Active", Active);

                    using (SqlDataReader reader = ado.ExecDataReaderProc("RNDImportTestList_READ", "RND", new object[] { param1 }))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string TestType = Convert.ToString(reader["TestDesc"]);
                                if ((TestType != " ") && (TestType != "") && (TestType != null))
                                {
                                    ID.ddTestType.Add(new SelectListItem
                                    {
                                        Value = TestType,
                                        Text = TestType,
                                        Selected = (ID.TestType == TestType) ? true : false,
                                    });
                                }
                            }
                        }
                        if (ado._conn != null && ado._conn.State == System.Data.ConnectionState.Open)
                        {
                            ado._conn.Close(); ado._conn.Dispose();
                        }
                    }
                    #endregion
                }
                return Serializer.ReturnContent(ID, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        ApiViewModel ImportData(string tt, string filePath)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;
            switch (tt)
            {
                case "Tension":
                    {
                        if (filePath == "none")
                            filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Tension.csv";
                        // filePath = "\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\Modulus Tension.csv";

                        sendMessage = ImportTensionData(filePath);
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
                        sendMessage = ImportCompressionData(filePath);
                        break;
                    }
                case "Bearing":
                    {
                        if (filePath == "none")
                            filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Bearing.csv";
                        //  filePath = "\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\Bearing.csv";
                        sendMessage = ImportBearingData(filePath);
                        break;
                    }
                case "Shear":
                    {
                        if (filePath == "none")
                            filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Shear.csv";
                        //  filePath = "\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\Shear.csv";

                        sendMessage = ImportShearData(filePath);
                        break;
                    }
                case "Notch Yield":
                    {
                        sendMessage = ImportNotchYieldData(filePath);
                        break;
                    }
                case "Residual Strength":
                    {
                        sendMessage = ImportResidualStrengthData(filePath);
                        break;
                    }
                case "Fracture Toughness":
                    {
                        if (filePath == "none")
                            filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Fracture Toughness.csv";
                        //filePath = "\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\Fracture Toughness.csv";
                        sendMessage = ImportFractureToughnessData(filePath);
                        break;
                    }
                case "Modulus Tension":
                    {
                        if (filePath == "none")
                            filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Modulus Tension.csv";
                        // filePath = "\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\Fracture Toughness.csv";

                        sendMessage = ImportModulusTensionData(filePath);
                        break;
                    }
                case "Modulus Compression":
                    {
                        if (filePath == "none")
                            filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Modulus Compression.csv";
                        //  filePath = "\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\Modulus Compression.csv";

                        sendMessage = ImportModulusCompressionData(filePath);
                        break;
                    }
                case "Fatigue":
                    {
                        if (filePath == "none")
                            filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Fatigue Testing.csv";
                        //  filePath = "\\USCTRD01\\RDServer\\RD\\Database\\Export\\ForNewDataBase\\Fatigue Testing.csv";

                        sendMessage = ImportFatigueTestingData(filePath);
                        break;
                    }
                default:
                    break;

            }
            return sendMessage;

        }


        // public HttpResponseMessage Get(HttpPostedFileBase file, string selectedTestType)
        public HttpResponseMessage Get(int id , string results, string selectedTestType)
        {
            string[] token = results.Split(',');
           // ImportDataViewModel ID = null;
            ApiViewModel importData = new ApiViewModel();
            if (token != null )
            {              
                #region switch TestType
                switch (selectedTestType)
                {
                    case "Tension":
                        {
                            importData = UploadTension(token);

                            break;
                        }
                    case "Compression":
                        {
                            importData = UploadCompression(token);
                            break;
                        }
                    case "Bearing":
                        {
                            importData = UploadBearing(token);
                            break;
                        }
                    case "Shear":
                        {
                            importData = UploadShear(token);
                            break;
                        }
                    case "Notch Yield":
                        {
                            importData = UploadNotchYield(token);
                            break;
                        }
                    case "Residual Strength":
                        {
                            importData = UploadResidualStrength(token);
                            
                            break;
                        }
                    case "Fracture Toughness":
                        {
                            importData = UploadFractureToughness(token);
                            break;
                        }
                    case "Modulus Tension":
                        {
                            importData = UploadModulusTension(token);
                            break;
                        }
                    case "Modulus Compression":
                        {
                            importData = UploadModulusCompression(token);
                            break;
                        }
                    case "Fatigue":
                        {
                            importData = UploadFatigue(token);
                            break;
                        }
                    default:
                        break;

                }

                #endregion
            }
            return Serializer.ReturnContent(importData, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);

        }

        #region Tension
        //public HttpResponseMessage Post(DataSearch<TensionViewModel> ds)
        //{
        //    ApiViewModel importData = new ApiViewModel();
        //    try
        //    {
        //        importData = SaveTensionData(ds.items);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message);
        //        // return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        //        importData.Message = ex.Message.ToString();
        //    }
        //    return Serializer.ReturnContent(importData, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);

        //}

        public List<TensionViewModel> listTensionData { get; set; }

        ApiViewModel ImportTensionData(string filePath)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;

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

                sendMessage = SaveTensionData(listTensionData);

                //CurrentUser user = ApiUser;
                //AdoHelper ado = new AdoHelper();
                //int introw = 0;
                //while (introw<listTensionData.Count)
                //{
                //    SqlParameter param2 = new SqlParameter("@WorkStudyID", Convert.ToString(listTensionData[introw].WorkStudyID));
                //    SqlParameter param3 = new SqlParameter("@TestNo", listTensionData[introw].TestNo);
                //    SqlParameter param4 = new SqlParameter("@SubConduct", listTensionData[introw].SubConduct);
                //    SqlParameter param5 = new SqlParameter("@SurfConduct", listTensionData[introw].SurfConduct);
                //    SqlParameter param6 = new SqlParameter("@FtuKsi", listTensionData[introw].FtuKsi);
                //    SqlParameter param7 = new SqlParameter("@FtyKsi", listTensionData[introw].FtyKsi);
                //    SqlParameter param8 = new SqlParameter("@eElongation", listTensionData[introw].eElongation);
                //    SqlParameter param9 = new SqlParameter("@EModulusMpsi", listTensionData[introw].EModulusMpsi);
                //    SqlParameter param10 = new SqlParameter("@SpeciComment", listTensionData[introw].SpeciComment);
                //    SqlParameter param11 = new SqlParameter("@Operator", listTensionData[introw].Operator);
                //    SqlParameter param12 = new SqlParameter("@TestDate", listTensionData[introw].TestDate);
                //    SqlParameter param13 = new SqlParameter("@TestTime", listTensionData[introw].TestTime);
                //    // SqlParameter param14 = new SqlParameter("@EntryBy", user.UserId);
                //    SqlParameter param14 = new SqlParameter("@EntryBy", user.UserName);
                //    SqlParameter param15 = new SqlParameter("@EntryDate", DateTime.Now);
                //    SqlParameter param16 = new SqlParameter("@Completed", '1');

                //    using (SqlDataReader reader = ado.ExecDataReaderProc("RNDTension_Insert", "RND", new object[] 
                //    {  param2,param3, param4, param5, param6, param7, param8, param9, param10,
                //        param11, param12, param13, param14, param15, param16}))
                //    {
                //        if (reader.HasRows)
                //        {
                //            while (reader.Read())
                //            {
                //                listTensionData[introw].RecID = Convert.ToInt32(reader["RecId"].ToString());
                //            }
                //        }
                //    }
                //    introw++;
                //}
                sendMessage.Success = true;
               
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();
                
            }
            return sendMessage;
        }
        private ApiViewModel UploadTension(string[] token)
        {
            
           ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;

            // var client = GetHttpClient();
            List<TensionViewModel> tensionList = new List<TensionViewModel>();
            string[] splitnew = null;
            int i = 0;
            try
            {
                while (i < token.Length - 1)
                {
                    TensionViewModel tensionData = new TensionViewModel();
                    //i = 0;
                    if (token[i].Contains("\n") && splitnew[1] != null)
                    {
                        tensionData.WorkStudyID = splitnew[1]; i++;
                    }
                    else
                    {
                        tensionData.WorkStudyID = Convert.ToString(token[i]); i++;
                    }

                    tensionData.TestNo = Convert.ToInt32(token[i]); i++;
                    tensionData.SubConduct = Convert.ToDecimal(token[i]); i++;
                    tensionData.SurfConduct = Convert.ToDecimal(token[i]); i++;
                    tensionData.FtuKsi = Convert.ToDecimal(token[i]); i++;
                    tensionData.FtyKsi = Convert.ToDecimal(token[i]); i++; ;
                    tensionData.eElongation = Convert.ToDecimal(token[i]); i++;
                    tensionData.EModulusMpsi = Convert.ToDecimal(token[i]); i++;
                    tensionData.SpeciComment = Convert.ToString(token[i]); i++;
                    tensionData.Operator = Convert.ToString(token[i]); i++;
                    tensionData.TestDate = Convert.ToString(token[i]); i++;

                    if ((token[i]).Contains("\n"))
                        splitnew = token[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                    tensionData.TestTime = Convert.ToString(splitnew[0]);

                    tensionList.Add(tensionData);
                }
                SaveTensionData(tensionList);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;
        }
        ApiViewModel SaveTensionData(List<TensionViewModel> listTensionData)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;

            try
            {
                CurrentUser user = ApiUser;
                AdoHelper ado = new AdoHelper();
                int introw = 0;
                while (introw < listTensionData.Count)
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
                    // SqlParameter param14 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param14 = new SqlParameter("@EntryBy", user.UserName);
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
                        if (ado._conn != null && ado._conn.State == System.Data.ConnectionState.Open)
                        {
                            ado._conn.Close(); ado._conn.Dispose();
                        }
                    }
                    introw++;
                }
                sendMessage.Success = true;

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;
        }
        #endregion

        #region Compression
        //public HttpResponseMessage Post(DataSearch<CompressionViewModel> ds)
        //{
        //    ApiViewModel importData = new ApiViewModel();
        //    try
        //    {
        //        importData = SaveCompressionData(ds.items);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message);
        //        // return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        //        importData.Message = ex.Message.ToString();
        //    }
        //    return Serializer.ReturnContent(importData, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);

        //}
        //public List<CompressionViewModel> listCompressionData { get; set; }

        ApiViewModel ImportCompressionData(string filePath)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;

            try
            {
                List<CompressionViewModel> listCompressionData = new List<CompressionViewModel>();
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

                sendMessage = SaveCompressionData(listCompressionData);
                sendMessage.Success = true;

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;
        }

        private ApiViewModel UploadCompression(string[] token)
        {
            ApiViewModel importData = new ApiViewModel();
            //var client = GetHttpClient();
            List<CompressionViewModel> CompressionList = new List<CompressionViewModel>();
            string[] splitnew = null;
            int i = 0;

            try
            {
                i = 0;
                while (i < token.Length - 1)
                {
                    token[i] = token[i].Replace("\"", "");
                    token[i] = token[i].Replace("\"/", "");
                    i++;
                }

                i = 0;
                while (i < token.Length - 1)
                {
                    CompressionViewModel CompressionData = new CompressionViewModel();
                    //i = 0;
                    if (token[i].Contains("\n") && splitnew[1] != null)
                    {
                        CompressionData.WorkStudyID = splitnew[1]; i++;
                    }
                    else
                    {

                        CompressionData.WorkStudyID = Convert.ToString(token[i]); i++;
                    }
                    CompressionData.TestNo = Convert.ToInt32(token[i]); i++;
                    CompressionData.SubConduct = Convert.ToDecimal(token[i]); i++;
                    CompressionData.SurfConduct = Convert.ToDecimal(token[i]); i++;
                    CompressionData.FcyKsi = Convert.ToDecimal(token[i]); i++;

                    CompressionData.EcModulusMpsi = Convert.ToDecimal(token[i]); i++;
                    CompressionData.SpeciComment = Convert.ToString(token[i]); i++;
                    CompressionData.Operator = Convert.ToString(token[i]); i++;
                    CompressionData.TestDate = Convert.ToString(token[i]); i++;

                    if ((token[i]).Contains("\n"))
                        splitnew = token[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                    CompressionData.TestTime = Convert.ToString(splitnew[0]);

                    CompressionList.Add(CompressionData);
                }
                importData = SaveCompressionData(CompressionList);

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                importData.Message = ex.Message;
            }

            return importData;
        }
        private ApiViewModel SaveCompressionData(List<CompressionViewModel> listCompressionData)
        {
               ApiViewModel sendMessage = new ApiViewModel();
                sendMessage.Message = "";
                sendMessage.Success = false;

                try
                {
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
                            //SqlParameter param12 = new SqlParameter("@EntryBy", user.UserId);
                            SqlParameter param12 = new SqlParameter("@EntryBy", user.UserName);
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

                        if (ado._conn != null && ado._conn.State == System.Data.ConnectionState.Open)
                        {
                            ado._conn.Close(); ado._conn.Dispose();
                        }
                    }
                            introw++;

                        }

                sendMessage.Success = true;
                sendMessage.Message = "Compression data saved";
            }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    sendMessage.Success = false;
                    sendMessage.Message = ex.Message.ToString();

                }
                return sendMessage;

        }
        #endregion

        #region Bearing

        public List<BearingViewModel> listBearingData { get; set; }

        ApiViewModel ImportBearingData(string filePath)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;
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

                sendMessage = SaveBearingData(listBearingData);

                sendMessage.Success = true;

            }
               
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;
        }
        private ApiViewModel UploadBearing(string[] token)
        {
            ApiViewModel importData = new ApiViewModel();
            //var client = GetHttpClient();
            List<BearingViewModel> BearingList = new List<BearingViewModel>();
            string[] splitnew = null;
            int i = 0;

            try
            {
                i = 0;
                while (i < token.Length - 1)
                {
                    token[i] = token[i].Replace("\"", "");
                    token[i] = token[i].Replace("\"/", "");
                    i++;
                }

                i = 0;
                while (i < token.Length - 1)
                {
                    BearingViewModel BearingData = new BearingViewModel();
                    //i = 0;
                    if (token[i].Contains("\n") && splitnew[1] != null)
                    {
                        BearingData.WorkStudyID = splitnew[1]; i++;
                    }
                    else
                    {
                        BearingData.WorkStudyID = Convert.ToString(token[i]); i++;
                    }

                    BearingData.TestNo = Convert.ToInt32(token[i]); i++;
                    BearingData.SubConduct = Convert.ToDecimal(token[i]); i++;
                    BearingData.SurfConduct = Convert.ToDecimal(token[i]); i++;
                    BearingData.eDCalc = Convert.ToDecimal(token[i]); i++;
                    BearingData.FbruKsi = Convert.ToDecimal(token[i]); i++;
                    BearingData.FbryKsi = Convert.ToDecimal(token[i]); i++;
                    BearingData.SpeciComment = Convert.ToString(token[i]); i++;
                    BearingData.Operator = Convert.ToString(token[i]); i++;
                    BearingData.TestDate = Convert.ToString(token[i]); i++;

                    if ((token[i]).Contains("\n"))
                        splitnew = token[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                    BearingData.TestTime = Convert.ToString(splitnew[0]);

                    BearingList.Add(BearingData);
                }
                importData = SaveBearingData(BearingList);

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                importData.Message = ex.Message;
            }

            return importData;
        }

        private ApiViewModel SaveBearingData(List<BearingViewModel> listBearingData)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;

            try
            {
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
                    //SqlParameter param13 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param13 = new SqlParameter("@EntryBy", user.UserName);
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
                        if (ado._conn != null && ado._conn.State == System.Data.ConnectionState.Open)
                        {
                            ado._conn.Close(); ado._conn.Dispose();
                        }
                    }
                    introw++;
                }
                sendMessage.Success = true;
                sendMessage.Message = "Bearing data saved";
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;
       
        }
    
        #endregion
        #region Shear
        public List<ShearViewModel> listShearData { get; set; }

        ApiViewModel ImportShearData(string filePath)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false; 

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

                sendMessage = SaveShearData(listShearData);

                sendMessage.Success = true;


            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;
        }

        private ApiViewModel UploadShear(string[] token)
        {
            ApiViewModel importData = new ApiViewModel();
            //var client = GetHttpClient();
            List<ShearViewModel> ShearList = new List<ShearViewModel>();
            string[] splitnew = null;
            int i = 0;

            try
            {
                i = 0;
                while (i < token.Length - 1)
                {
                    token[i] = token[i].Replace("\"", "");
                    token[i] = token[i].Replace("\"/", "");
                    i++;
                }

                i = 0;
                while (i < token.Length - 1)
                {
                    ShearViewModel ShearData = new ShearViewModel();
                    //i = 0;
                    if (token[i].Contains("\n") && splitnew[1] != null)
                    {
                        ShearData.WorkStudyID = splitnew[1]; i++;
                    }
                    else
                    {
                        ShearData.WorkStudyID = Convert.ToString(token[i]); i++;
                    }
                                 
                    ShearData.TestNo = Convert.ToInt32(token[i]); i++;
                    ShearData.SubConduct = Convert.ToDecimal(token[i]); i++;
                    ShearData.SurfConduct = Convert.ToDecimal(token[i]); i++;
                    ShearData.FsuKsi = Convert.ToDecimal(token[i]); i++;
                    ShearData.SpeciComment = Convert.ToString(token[i]); i++;
                    ShearData.Operator = Convert.ToString(token[i]); i++;
                    ShearData.TestDate = Convert.ToString(token[i]); i++;

                    if ((token[i]).Contains("\n"))
                        splitnew = token[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                    ShearData.TestTime = Convert.ToString(splitnew[0]);

                    ShearList.Add(ShearData);
                }
                importData = SaveShearData(ShearList);

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                importData.Message = ex.Message;
            }

            return importData;
        }

        private ApiViewModel SaveShearData(List<ShearViewModel> listShearData)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;

            try
            {
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
                    //SqlParameter param11 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param11 = new SqlParameter("@EntryBy", user.UserName);
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
                        if (ado._conn != null && ado._conn.State == System.Data.ConnectionState.Open)
                        {
                            ado._conn.Close(); ado._conn.Dispose();
                        }
                    }
                    introw++;
                }
                sendMessage.Success = true;
                sendMessage.Message = "Shear data saved";
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;

        }

        #endregion
        #region NotchYield File with heading

        public List<NotchYieldViewModel> listNotchYieldData { get; set; }
        ApiViewModel ImportNotchYieldData(string filePath)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;
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
                sendMessage = SaveNotchYieldData(listNotchYieldData);              

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;
        }
        
        private ApiViewModel UploadNotchYield(string[] token)
        {
            ApiViewModel importData = new ApiViewModel();
            //var client = GetHttpClient();
            List<NotchYieldViewModel> NotchYieldList = new List<NotchYieldViewModel>();
            string[] splitnew = null;
            int i = 0;

            try
            {
                //i = 0;
                //while (i < token.Length - 1)
                //{
                //    token[i] = token[i].Replace("\"", "");
                //    token[i] = token[i].Replace("\"/", "");
                //    i++;
                //}

                i = 10;

                while (i < token.Length - 1)
                {
                    NotchYieldViewModel NotchYieldData = new NotchYieldViewModel();
                    //i = 0;
                    if (token[i].Contains("\n") && splitnew[1] != null)
                    {
                        NotchYieldData.WorkStudyID = splitnew[1]; i++;
                    }
                    else
                    {
                        NotchYieldData.WorkStudyID = Convert.ToString(token[i]); i++;
                    }
                    NotchYieldData.TestNo = Convert.ToInt32(token[i]); i++;
                    NotchYieldData.SubConduct = Convert.ToDecimal(token[i]); i++;
                    NotchYieldData.SurfConduct = Convert.ToDecimal(token[i]); i++;
                    NotchYieldData.NotchStrengthKsi = Convert.ToDecimal(token[i]); i++;
                    NotchYieldData.YieldStrengthKsi = Convert.ToDecimal(token[i]); i++;
                    NotchYieldData.NotchYieldRatio = Convert.ToDecimal(token[i]); i++;
                    NotchYieldData.SpeciComment = Convert.ToString(token[i]); i++;
                    NotchYieldData.Operator = Convert.ToString(token[i]); i++;
                    NotchYieldData.TestDate = Convert.ToString(token[i]); i++;

                    if ((token[i]).Contains("\n"))
                        splitnew = token[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                    NotchYieldData.TestTime = Convert.ToString(splitnew[0]);

                    NotchYieldList.Add(NotchYieldData);
                }
                importData = SaveNotchYieldData(NotchYieldList);

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                importData.Message = ex.Message;
            }

            return importData;
        }

        private ApiViewModel SaveNotchYieldData(List<NotchYieldViewModel> listNotchYieldData)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;

            try
            {
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
                    // SqlParameter param13 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param13 = new SqlParameter("@EntryBy", user.UserName);
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
                        if (ado._conn != null && ado._conn.State == System.Data.ConnectionState.Open)
                        {
                            ado._conn.Close(); ado._conn.Dispose();
                        }
                    }
                    introw++;
                }
                sendMessage.Success = true;
                sendMessage.Message = "NotchYield data saved";
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;

        }
        #endregion
        #region ResidualStrength  File with heading
        /// <summary>
        /// ResidualStrength
        /// </summary>
        /// <returns></returns>
        public List<ResidualStrengthViewModel> listResidualStrengthData { get; set; }

        ApiViewModel ImportResidualStrengthData(string filePath)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;
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

                sendMessage = SaveResidualStrengthData(listResidualStrengthData);
                sendMessage.Success = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;
        }

        private ApiViewModel UploadResidualStrength(string[] token)
        {
            ApiViewModel importData = new ApiViewModel();
            //var client = GetHttpClient();
            List<ResidualStrengthViewModel> ResidualStrengthList = new List<ResidualStrengthViewModel>();
            string[] splitnew = null;
            int i = 0;

            try
            {
                i = 0;
                while (i < token.Length - 1)
                {
                    token[i] = token[i].Replace("\"", "");
                    token[i] = token[i].Replace("\"/", "");
                    i++;
                }

                i = 0;
                while (i < token.Length - 1)
                {
                    ResidualStrengthViewModel ResidualStrengthData = new ResidualStrengthViewModel();
                    //i = 0;
                    if (token[i].Contains("\n") && splitnew[1] != null)
                    {
                        ResidualStrengthData.WorkStudyID = splitnew[1]; i++;
                    }
                    else
                    {
                        ResidualStrengthData.WorkStudyID = Convert.ToString(token[i]); i++;
                    }
                                    
                    ResidualStrengthData.TestNo = Convert.ToInt32(token[i]); i++;
                    ResidualStrengthData.SubConduct = Convert.ToDecimal(token[i]); i++;
                    ResidualStrengthData.SurfConduct = Convert.ToDecimal(token[i]); i++;
                    ResidualStrengthData.Validity = Convert.ToString(token[i]); i++;
                    ResidualStrengthData.ResidualStrength = Convert.ToDecimal(token[i]); i++;
                    ResidualStrengthData.PmaxLBS = Convert.ToDecimal(token[i]); i++;
                    ResidualStrengthData.WIn = Convert.ToDecimal(token[i]); i++;
                    ResidualStrengthData.BIn = Convert.ToDecimal(token[i]); i++;
                    ResidualStrengthData.AvgFinalPreCrack = Convert.ToDecimal(token[i]); i++;                    
                    ResidualStrengthData.SpeciComment = Convert.ToString(token[i]); i++;
                    ResidualStrengthData.Operator = Convert.ToString(token[i]); i++;
                    ResidualStrengthData.TestDate = Convert.ToString(token[i]); i++;

                    if ((token[i]).Contains("\n"))
                        splitnew = token[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                    ResidualStrengthData.TestTime = Convert.ToString(splitnew[0]);

                    ResidualStrengthList.Add(ResidualStrengthData);
                }
                importData = SaveResidualStrengthData(ResidualStrengthList);

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                importData.Message = ex.Message;
            }

            return importData;
        }

        private ApiViewModel SaveResidualStrengthData(List<ResidualStrengthViewModel> listResidualStrengthData)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;

            try
            {
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
                    // SqlParameter param16 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param16 = new SqlParameter("@EntryBy", user.UserName);
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
                        if (ado._conn != null && ado._conn.State == System.Data.ConnectionState.Open)
                        {
                            ado._conn.Close(); ado._conn.Dispose();
                        }
                    }
                    introw++;
                }
                sendMessage.Success = true;
                sendMessage.Message = "ResidualStrength data saved";
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;

        }

        #endregion 
        #region FractureToughness
        /// <summary>
        /// Fracture Toughness
        /// </summary>
        public List<FractureToughnessViewModel> listFractureToughnessData { get; set; }

        ApiViewModel ImportFractureToughnessData(string filePath)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;
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
                sendMessage = SaveFractureToughnessData(listFractureToughnessData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;
        }

        private ApiViewModel UploadFractureToughness(string[] token)
        {
            ApiViewModel importData = new ApiViewModel();
            //var client = GetHttpClient();
            List<FractureToughnessViewModel> FractureToughnessList = new List<FractureToughnessViewModel>();
            string[] splitnew = null;
            int i = 0;

            try
            {
                i = 0;
                while (i < token.Length - 1)
                {
                    token[i] = token[i].Replace("\"", "");
                    token[i] = token[i].Replace("\"/", "");
                    i++;
                }

                i = 0;
              
                while (i < token.Length - 1)
                {
                    FractureToughnessViewModel FractureToughnessData = new FractureToughnessViewModel();
                    //i = 0;
                    if ((token[i] != "")&& (token[i] != "\r\n"))
                    {
                        if (token[i].Contains("\n") && splitnew[1] != null && splitnew[1] != "")
                        {
                            FractureToughnessData.WorkStudyID = splitnew[1]; i++;
                        }
                        else
                        {                            
                            FractureToughnessData.WorkStudyID = Convert.ToString(token[i]); i++;
                        }
                        FractureToughnessData.TestNo = Convert.ToInt32(token[i]); i++;
                        FractureToughnessData.SubConduct = Convert.ToDecimal(token[i]); i++;
                        FractureToughnessData.SurfConduct = Convert.ToDecimal(token[i]); i++;
                        FractureToughnessData.Validity = Convert.ToString(token[i]); i++;
                        FractureToughnessData.KKsiIn = Convert.ToDecimal(token[i]); i++;
                        FractureToughnessData.KmaxKsiIn = Convert.ToDecimal(token[i]); i++;
                        FractureToughnessData.PqLBS = Convert.ToDecimal(token[i]); i++;
                        FractureToughnessData.PmaxLBS = Convert.ToDecimal(token[i]); i++;
                        FractureToughnessData.aOIn = Convert.ToDecimal(token[i]); i++;
                        FractureToughnessData.WIn = Convert.ToDecimal(token[i]); i++;
                        FractureToughnessData.BIn = Convert.ToDecimal(token[i]); i++;
                        FractureToughnessData.BnIn = Convert.ToDecimal(token[i]); i++;
                        FractureToughnessData.AvgFinalPreCrack = Convert.ToDecimal(token[i]); i++;
                        FractureToughnessData.SpeciComment = Convert.ToString(token[i]); i++;
                        FractureToughnessData.Operator = Convert.ToString(token[i]); i++;
                        FractureToughnessData.TestDate = Convert.ToString(token[i]); i++;
                        FractureToughnessData.EntryDate = Convert.ToString(token[i]); i++;

                        FractureToughnessData.blank1 = Convert.ToString(token[i]); i++;
                        FractureToughnessData.blank2 = Convert.ToString(token[i]); i++;
                        FractureToughnessData.blank3 = Convert.ToString(token[i]); i++;


                        if ((token[i]).Contains("\n"))
                            splitnew = token[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                       // FractureToughnessData.blank4 = Convert.ToString(token[i]); i++;

                        FractureToughnessList.Add(FractureToughnessData);
                    }
                    else
                    {
                       
                        if (token[i] == "\r\n")
                            i++;
                                              
                        while (!token[i].Contains("\n"))
                            i++;
                        if ((token[i]).Contains("\n"))
                            splitnew = token[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                    }
                }
                importData = SaveFractureToughnessData(FractureToughnessList);

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                importData.Message = ex.Message;
            }

            return importData;
        }

        private ApiViewModel SaveFractureToughnessData(List<FractureToughnessViewModel> listFractureToughnessData)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;

            try
            {
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
                    //SqlParameter param20 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param20 = new SqlParameter("@EntryBy", user.UserName);
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
                        if (ado._conn != null && ado._conn.State == System.Data.ConnectionState.Open)
                        {
                            ado._conn.Close(); ado._conn.Dispose();
                        }
                    }
                    introw++;
                }
                sendMessage.Success = true;
                sendMessage.Message = "FractureToughness data saved";
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;

        }


        #endregion
        #region ModulusTension
        public List<ModulusTensionDataViewModel> listModulusTensionData { get; set; }

        ApiViewModel ImportModulusTensionData(string filePath)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;
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
                sendMessage = SaveModulusTensionData(listModulusTensionData);
                sendMessage.Success = true;

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;
        }
        private ApiViewModel UploadModulusTension(string[] token)
        {
            ApiViewModel importData = new ApiViewModel();
            //var client = GetHttpClient();
            List<ModulusTensionDataViewModel> ModulusTensionList = new List<ModulusTensionDataViewModel>();
            string[] splitnew = null;
            int i = 0;

            try
            {
                i = 0;
                while (i < token.Length - 1)
                {
                    token[i] = token[i].Replace("\"", "");
                    token[i] = token[i].Replace("\"/", "");
                    i++;
                }

                i = 0;
                while (i < token.Length - 1)
                {
                    ModulusTensionDataViewModel ModulusTensionData = new ModulusTensionDataViewModel();
                    //i = 0;
                    if (token[i].Contains("\n") && splitnew[1] != null)
                    {
                        ModulusTensionData.WorkStudyID = splitnew[1]; i++;
                    }
                    else
                    {
                        ModulusTensionData.WorkStudyID = Convert.ToString(token[i]); i++;
                    }

                    ModulusTensionData.TestNo = Convert.ToInt32(token[i]); i++;
                    ModulusTensionData.SubConduct = Convert.ToDecimal(token[i]); i++;
                    ModulusTensionData.SurfConduct = Convert.ToDecimal(token[i]); i++;
                    ModulusTensionData.EModulusTension = Convert.ToDecimal(token[i]); i++;
                    ModulusTensionData.SpeciComment = Convert.ToString(token[i]); i++;
                    ModulusTensionData.Operator = Convert.ToString(token[i]); i++;
                    ModulusTensionData.TestDate = Convert.ToString(token[i]); i++;

                    if ((token[i]).Contains("\n"))
                        splitnew = token[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                    ModulusTensionData.TestTime = Convert.ToString(splitnew[0]);

                    ModulusTensionList.Add(ModulusTensionData);
                }
                importData = SaveModulusTensionData(ModulusTensionList);

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                importData.Message = ex.Message;
            }

            return importData;
        }

        private ApiViewModel SaveModulusTensionData(List<ModulusTensionDataViewModel> listModulusTensionData)

        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;

            try
            {
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
                    //SqlParameter param11 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param11 = new SqlParameter("@EntryBy", user.UserName);
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
                        if (ado._conn != null && ado._conn.State == System.Data.ConnectionState.Open)
                        {
                            ado._conn.Close(); ado._conn.Dispose();
                        }
                    }
                    introw++;
                }
                sendMessage.Success = true;
                sendMessage.Message = "ModulusTension data saved";
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;

        }

        #endregion
        #region ModulusCompression
        public List<ModulusCompressionDataViewModel> listModulusCompressionData { get; set; }

        ApiViewModel ImportModulusCompressionData(string filePath)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;
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
                sendMessage = SaveModulusCompressionData(listModulusCompressionData);                
                sendMessage.Success = true;

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;
        }
        private ApiViewModel UploadModulusCompression(string[] token)
        {
            ApiViewModel importData = new ApiViewModel();
            //var client = GetHttpClient();
            List<ModulusCompressionDataViewModel> ModulusCompressionList = new List<ModulusCompressionDataViewModel>();
            string[] splitnew = null;
            int i = 0;

            try
            {
                i = 0;
                while (i < token.Length - 1)
                {
                    token[i] = token[i].Replace("\"", "");
                    token[i] = token[i].Replace("\"/", "");
                    i++;
                }

                i = 0;
                while (i < token.Length - 1)
                {
                    ModulusCompressionDataViewModel ModulusCompressionData = new ModulusCompressionDataViewModel();
                    //i = 0;
                   
                    if (token[i].Contains("\n") && splitnew[1] != null)
                    {
                        ModulusCompressionData.WorkStudyID = splitnew[1]; i++;
                    }
                    else
                    {
                        ModulusCompressionData.WorkStudyID = Convert.ToString(token[i]); i++;
                    }
                    ModulusCompressionData.TestNo = Convert.ToInt32(token[i]); i++;
                    ModulusCompressionData.SubConduct = Convert.ToDecimal(token[i]); i++;
                    ModulusCompressionData.SurfConduct = Convert.ToDecimal(token[i]); i++;
                    if (token[i] == "****")
                    {
                        token[i] = "0.0";
                    }
                    ModulusCompressionData.EModulusCompression = Convert.ToDecimal(token[i]); i++;
                    ModulusCompressionData.SpeciComment = Convert.ToString(token[i]); i++;
                    ModulusCompressionData.Operator = Convert.ToString(token[i]); i++;
                    ModulusCompressionData.TestDate = Convert.ToString(token[i]); i++;

                    if ((token[i]).Contains("\n"))
                        splitnew = token[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                    ModulusCompressionData.TestTime = Convert.ToString(splitnew[0]);

                    ModulusCompressionList.Add(ModulusCompressionData);
                }
                importData = SaveModulusCompressionData(ModulusCompressionList);

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                importData.Message = ex.Message;
            }

            return importData;
        }

        private ApiViewModel SaveModulusCompressionData(List<ModulusCompressionDataViewModel> listModulusCompressionData)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;

            try
            {
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
                    //SqlParameter param11 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param11 = new SqlParameter("@EntryBy", user.UserName);
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
                        if (ado._conn != null && ado._conn.State == System.Data.ConnectionState.Open)
                        {
                            ado._conn.Close(); ado._conn.Dispose();
                        }
                    }
                    introw++;
                }
                sendMessage.Success = true;
                sendMessage.Message = "ModulusCompression data saved";
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;

        }
        #endregion

        #region FatigueTesting
        public List<FatigueTestingDataViewModel> listFatigueTestingData { get; set; }

        ApiViewModel ImportFatigueTestingData(string filePath)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;
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

                sendMessage.Success = true;

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;
        }
        private ApiViewModel UploadFatigue(string[] token)
        {
            ApiViewModel importData = new ApiViewModel();
            //var client = GetHttpClient();
            List<FatigueTestingDataViewModel> FatigueTestingList = new List<FatigueTestingDataViewModel>();
            string[] splitnew = null;
            int i = 0;

            try
            {
                i = 0;
                while (i < token.Length - 1)
                {
                    token[i] = token[i].Replace("\"", "");
                    token[i] = token[i].Replace("\"/", "");
                    i++;
                }

                i = 0;
                while (i < token.Length - 1)
                {
                    FatigueTestingDataViewModel FatigueTestingData = new FatigueTestingDataViewModel();
                    //i = 0;
                    if ((token[i] != "") && (token[i] != "\r\n"))
                    {
                        if (token[i].Contains("\n") && splitnew[1] != null && splitnew[1] != "")
                        {
                            FatigueTestingData.WorkStudyID = splitnew[1]; i++;
                        }
                        else
                        {
                            FatigueTestingData.WorkStudyID = Convert.ToString(token[i]); i++;
                        }

                        FatigueTestingData.TestNo = Convert.ToInt32(token[i]); i++;
                        FatigueTestingData.SpecimenDrawing = Convert.ToString(token[i]); i++;

                        FatigueTestingData.MinStress = Convert.ToDecimal(token[i]); i++;
                        FatigueTestingData.MaxStress = Convert.ToDecimal(token[i]); i++;
                        FatigueTestingData.MinLoad = Convert.ToDecimal(token[i]); i++;
                        FatigueTestingData.MaxLoad = Convert.ToDecimal(token[i]); i++;
                        FatigueTestingData.WidthOrDia = Convert.ToDecimal(token[i]); i++;
                        FatigueTestingData.Thickness = Convert.ToDecimal(token[i]); i++;
                        FatigueTestingData.HoleDia = Convert.ToDecimal(token[i]); i++;
                        FatigueTestingData.AvgChamferDepth = Convert.ToDecimal(token[i]); i++;
                        FatigueTestingData.Frequency = Convert.ToString(token[i]); i++;
                        FatigueTestingData.CyclesToFailure = Convert.ToDecimal(token[i]); i++;
                        if (token[i] == "")
                        {
                            token[i] = "0.0";
                        }
                        FatigueTestingData.Roughness = Convert.ToDecimal(token[i]); i++;
                        FatigueTestingData.TestFrame = Convert.ToString(token[i]); i++;
                        FatigueTestingData.Comment = Convert.ToString(token[i]); i++;
                        FatigueTestingData.FractureLocation = Convert.ToString(token[i]); i++;
                        FatigueTestingData.Operator = Convert.ToString(token[i]); i++;
                        FatigueTestingData.TestDate = Convert.ToString(token[i]); i++;
                        FatigueTestingData.EntryDate = Convert.ToString(token[i]); i++;
                        FatigueTestingData.field1 = Convert.ToString(token[i]); i++;

                        FatigueTestingData.Operator = Convert.ToString(token[i]); i++;
                        FatigueTestingData.TestDate = Convert.ToString(token[i]); i++;

                        if ((token[i]).Contains("\n"))
                            splitnew = token[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                        FatigueTestingData.TestTime = Convert.ToString(splitnew[0]);

                        FatigueTestingList.Add(FatigueTestingData);
                    }
                    else
                    {
                        if (token[i] == "\r\n")
                            i++;
                        while (!token[i].Contains("\n"))
                            i++;
                        if ((token[i]).Contains("\n"))
                            splitnew = token[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                    }
                }
                importData = SaveFatigueTestingData(FatigueTestingList);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                importData.Message = ex.Message;
            }

            return importData;
        }

        private ApiViewModel SaveFatigueTestingData(List<FatigueTestingDataViewModel> listFatigueTestingData)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;

            try
            {
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
                    //SqlParameter param22 = new SqlParameter("@EntryBy", user.UserId);
                    SqlParameter param22 = new SqlParameter("@EntryBy", user.UserName);
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
                        if (ado._conn != null && ado._conn.State == System.Data.ConnectionState.Open)
                        {
                            ado._conn.Close(); ado._conn.Dispose();
                        }
                    }
                    introw++;
                }
                sendMessage.Success = true;
                sendMessage.Message = "FatigueTesting data saved";
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;

        }
        #endregion

    }
}