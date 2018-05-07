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
using System.Data;
using System.Text.RegularExpressions;

namespace RNDSystems.API.Controllers
{
    public class ImportDataController : UnSecuredController
    {
        // GET: ImportData
        // public HttpResponseMessage Get(string WorkStudyId)
        public HttpResponseMessage Get(string Active, string SelectedTests = "none")
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


        //ApiViewModel ImportData(string tt, string filePath)
        public HttpResponseMessage Post(ApiViewModel selectedTestType)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = true;
            string filePath = "none";

         //   string[] token = selectedTestType.MessageList.ToString().Split(',');
            
            DataTableReturn dataTable = new DataTableReturn();
         //   List<string> errorMessage = new List<string>();
            string successMsg = "";
            string errorMessage = "";
                    
            foreach (var tt in selectedTestType.MessageList)
            {
             if (tt.Trim() != "-1")
                {
                    #region switch
                    switch (tt.Trim())
                    {
                        case "Tension":
                            {
                                if (filePath == "none")
                                    filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Tension.csv";

                                dataTable = Common.Parser.CsvToDataTable(filePath, false);
                                if (dataTable.Success)
                                    sendMessage = UploadTension(dataTable.data);
                                else
                                    sendMessage.Message = dataTable.Message;
                                //  sendMessage = UploadTension(Common.Parser.CsvToDataTable(filePath, false));
                                break;
                            }
                        case "Compression":
                            {
                                if (filePath == "none")
                                    filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Compression.csv";
                                dataTable = Common.Parser.CsvToDataTable(filePath, false);
                                if (dataTable.Success)
                                    sendMessage = UploadCompression(dataTable.data);
                                else
                                    sendMessage.Message = dataTable.Message;
                                //  sendMessage = UploadCompression(Common.Parser.CsvToDataTable(filePath, false));
                                break;
                            }
                        case "Bearing":
                            {
                                if (filePath == "none")
                                    filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Bearing.csv";
                                dataTable = Common.Parser.CsvToDataTable(filePath, false);
                                if (dataTable.Success)
                                    sendMessage = UploadBearing(dataTable.data);
                                else
                                    sendMessage.Message = dataTable.Message;
                                //sendMessage = UploadBearing(Common.Parser.CsvToDataTable(filePath, false));
                                break;
                            }
                        case "Shear":
                            {
                                if (filePath == "none")
                                    filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Shear.csv";

                                dataTable = Common.Parser.CsvToDataTable(filePath, false);
                                if (dataTable.Success)
                                    sendMessage = UploadShear(dataTable.data);
                                else
                                    sendMessage.Message = dataTable.Message;
                                // sendMessage = UploadShear(Common.Parser.CsvToDataTable(filePath, false));
                                break;
                            }
                        case "Notch Yield":
                            {
                                if (filePath == "none")
                                    filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Notch Yield.csv";

                                dataTable = Common.Parser.CsvToDataTable(filePath, true);
                                if (dataTable.Success)
                                    sendMessage = UploadNotchYield(dataTable.data);
                                else
                                    sendMessage.Message = dataTable.Message;
                                // sendMessage = UploadNotchYield(Common.Parser.CsvToDataTable(filePath, true));
                                break;
                            }
                        case "Residual Strength":
                            {
                                if (filePath == "none")
                                    filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Residual Strength.csv";

                                dataTable = Common.Parser.CsvToDataTable(filePath, true);
                                if (dataTable.Success)
                                    sendMessage = UploadResidualStrength(dataTable.data);
                                else
                                    sendMessage.Message = dataTable.Message;
                                //   sendMessage = UploadResidualStrength(Common.Parser.CsvToDataTable(filePath, true));
                                break;
                            }
                        case "Fracture Toughness":
                            {
                                if (filePath == "none")
                                    filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Fracture Toughness.csv";
                                dataTable = Common.Parser.CsvToDataTable(filePath, false);
                                if (dataTable.Success)
                                    sendMessage = UploadFractureToughness(dataTable.data);
                                else
                                    sendMessage.Message = dataTable.Message;
                                //   sendMessage = UploadFractureToughness(Common.Parser.CsvToDataTable(filePath, false));
                                break;
                            }
                        case "Modulus Tension":
                            {
                                if (filePath == "none")
                                    filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Modulus Tension.csv";
                                dataTable = Common.Parser.CsvToDataTable(filePath, false);
                                if (dataTable.Success)
                                    sendMessage = UploadModulusTension(dataTable.data);
                                else
                                    sendMessage.Message = dataTable.Message;
                                //   sendMessage = UploadModulusTension(Common.Parser.CsvToDataTable(filePath, false));
                                break;
                            }
                        case "Modulus Compression":
                            {
                                if (filePath == "none")
                                    filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Modulus Compression.csv";
                                dataTable = Common.Parser.CsvToDataTable(filePath, false);
                                if (dataTable.Success)
                                    sendMessage = UploadModulusCompression(dataTable.data);
                                else
                                    sendMessage.Message = dataTable.Message;
                                //  sendMessage = UploadModulusCompression(Common.Parser.CsvToDataTable(filePath, false));
                                break;
                            }
                        case "Fatigue":
                            {
                                if (filePath == "none")
                                    filePath = "S:\\RD\\Database\\Export\\ForNewDataBase\\Fatigue Testing.csv";
                                dataTable = Common.Parser.CsvToDataTable(filePath, false);
                                if (dataTable.Success)
                                    sendMessage = UploadFatigue(dataTable.data);
                                else
                                    sendMessage.Message = dataTable.Message;
                                // sendMessage = UploadFatigue(Common.Parser.CsvToDataTable(filePath, false));

                                break;
                            }
                        default:
                            break;

                    }
                    filePath = "none";
                    #endregion                    
                    if (dataTable.Success && sendMessage.Success)
                        if (successMsg == "")
                            successMsg = "Data Saved: " + tt.Trim();
                        else
                            successMsg += ", " + tt.Trim();                   
                    else
                    {
                        //  errorMessage.Add(sendMessage.Message);
                        if (errorMessage == "")
                            errorMessage = "Import Error: " + tt.Trim();
                        else
                            errorMessage += ", "+ tt.Trim();
                    }
                       
                }                
            }
            sendMessage.Message1 = errorMessage;            
            sendMessage.Message = successMsg;
            //  return sendMessage;

            return Serializer.ReturnContent(sendMessage, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }

        //Import file
        public HttpResponseMessage Get(int id, string path, string selectedTestType)
        {
            //var data= Common.Parser.CsvToDataTable(path,false);
            ApiViewModel importData = new ApiViewModel();
            DataTableReturn dataTable = new DataTableReturn();
          //  importData = ImportData(selectedTestType, path);

            #region manual entry switch
            switch (selectedTestType)
            {
                case "Tension":
                    {
                        dataTable = Common.Parser.CsvToDataTable(path, false);
                        if (dataTable.Success)
                            importData = UploadTension(dataTable.data);
                        else
                        {
                            importData.Message = dataTable.Message;
                            importData.Success = dataTable.Success;
                        }
                          
                      //  importData = UploadTension(Common.Parser.CsvToDataTable(path, false));
                        break;
                    }
                case "Compression":
                    {
                        dataTable = Common.Parser.CsvToDataTable(path, false);
                        if (dataTable.Success)
                            importData = UploadCompression(dataTable.data);
                        else
                        {
                            importData.Message = dataTable.Message;
                            importData.Success = dataTable.Success;
                        }
                        // importData = UploadCompression(Common.Parser.CsvToDataTable(path, false));
                        break;
                    }
                case "Bearing":
                    {
                        dataTable = Common.Parser.CsvToDataTable(path, false);
                        if (dataTable.Success)
                            importData = UploadBearing(dataTable.data);
                        else
                        {
                            importData.Message = dataTable.Message;
                            importData.Success = dataTable.Success;
                        }
                        //importData = UploadBearing(Common.Parser.CsvToDataTable(path, false));
                        break;
                    }
                case "Shear":
                    {
                        dataTable = Common.Parser.CsvToDataTable(path, false);
                        if (dataTable.Success)
                            importData = UploadShear(dataTable.data);
                        else
                        {
                            importData.Message = dataTable.Message;
                            importData.Success = dataTable.Success;
                        }
                        //importData = UploadShear(Common.Parser.CsvToDataTable(path, false));
                        break;
                    }
                case "Notch Yield":
                    {
                        dataTable = Common.Parser.CsvToDataTable(path, true);
                        if (dataTable.Success)
                            importData = UploadNotchYield(dataTable.data);
                        else
                        {
                            importData.Message = dataTable.Message;
                            importData.Success = dataTable.Success;
                        }
                       // importData = UploadNotchYield(Common.Parser.CsvToDataTable(path, true));
                        break;
                    }
                case "Residual Strength":
                    {
                        dataTable = Common.Parser.CsvToDataTable(path, true);
                        if (dataTable.Success)
                            importData = UploadResidualStrength(dataTable.data);
                        else
                        {
                            importData.Message = dataTable.Message;
                            importData.Success = dataTable.Success;
                        }
                        //importData = UploadResidualStrength(Common.Parser.CsvToDataTable(path, true));

                        break;
                    }
                case "Fracture Toughness":
                    {
                        dataTable = Common.Parser.CsvToDataTable(path, false);
                        if (dataTable.Success)
                            importData = UploadFractureToughness(dataTable.data);
                        else
                        {
                            importData.Message = dataTable.Message;
                            importData.Success = dataTable.Success;
                        }
                     //   importData = UploadFractureToughness(Common.Parser.CsvToDataTable(path, false));
                        break;
                    }
                case "Modulus Tension":
                    {
                        dataTable = Common.Parser.CsvToDataTable(path, false);
                        if (dataTable.Success)
                            importData = UploadModulusTension(dataTable.data);
                        else
                        {
                            importData.Message = dataTable.Message;
                            importData.Success = dataTable.Success;
                        }
                     //   importData = UploadModulusTension(Common.Parser.CsvToDataTable(path, false));
                        break;
                    }
                case "Modulus Compression":
                    {
                        dataTable = Common.Parser.CsvToDataTable(path, false);
                        if (dataTable.Success)
                            importData = UploadModulusCompression(dataTable.data);
                        else
                        {
                            importData.Message = dataTable.Message;
                            importData.Success = dataTable.Success;
                        }
                       // importData = UploadModulusCompression(Common.Parser.CsvToDataTable(path, false));
                        break;
                    }
                case "Fatigue":
                    {
                        dataTable = Common.Parser.CsvToDataTable(path, false);
                        if (dataTable.Success)
                            importData = UploadFatigue(dataTable.data);
                        else
                        {
                            importData.Message = dataTable.Message;
                            importData.Success = dataTable.Success;
                        }
                        //importData = UploadFatigue(Common.Parser.CsvToDataTable(path, false));
                        break;
                    }
                default:
                    break;
            }
            #endregion         
            return Serializer.ReturnContent(importData, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);

        }

        #region Tension
        private ApiViewModel UploadTension(DataTable data)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            var listTensionData = new List<TensionViewModel>();
            sendMessage.Message = "";
            sendMessage.Success = false;
            try
            {
                var tensionData = new List<TensionViewModel>();
                foreach (DataRow row in data.Rows)
                {
                    if (row[0] == null || row[0].ToString().Trim() == "" || new Regex("[^ -~]+").IsMatch(row[0].ToString().Trim()))
                    {
                        continue;
                    }
                    listTensionData.Add(new TensionViewModel()
                    {
                        WorkStudyID = Convert.ToString(row[0]),
                        TestNo = Convert.ToInt32(row[1]),
                        SubConduct = Convert.ToDecimal(row[2]),
                        SurfConduct = Convert.ToDecimal(row[3]),
                        FtuKsi = Convert.ToDecimal(row[4]),
                        FtyKsi = Convert.ToDecimal(row[5]),
                        eElongation = Convert.ToDecimal(row[6]),
                        EModulusMpsi = Convert.ToDecimal(row[7]),
                        SpeciComment = Convert.ToString(row[8]),
                        Operator = Convert.ToString(row[9]),
                        TestDate = Convert.ToString(row[10]),
                        TestTime = Convert.ToString(row[11])
                    });

                }
                return SaveTensionData(listTensionData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();

            }
            return sendMessage;
        }
        private ApiViewModel SaveTensionData(List<TensionViewModel> listTensionData)
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
                sendMessage.Message = "Tension data saved";
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
        private ApiViewModel UploadCompression(DataTable data)
        {
            ApiViewModel importData = new ApiViewModel();
            List<CompressionViewModel> CompressionList = new List<CompressionViewModel>();
            try
            {
                foreach (DataRow row in data.Rows)
                {
                    if (row[0] == null || row[0].ToString().Trim() == "" || new Regex("[^ -~]+").IsMatch(row[0].ToString().Trim()))
                    {
                        continue;
                    }
                    CompressionViewModel CompressionData = new CompressionViewModel();
                    CompressionData.WorkStudyID = Convert.ToString(row[0]);
                    CompressionData.TestNo = Convert.ToInt32(row[1]);
                    CompressionData.SubConduct = Convert.ToDecimal(row[2]);
                    CompressionData.SurfConduct = Convert.ToDecimal(row[3]);
                    CompressionData.FcyKsi = Convert.ToDecimal(row[4]);
                    CompressionData.EcModulusMpsi = Convert.ToDecimal(row[5]);
                    CompressionData.SpeciComment = Convert.ToString(row[6]);
                    CompressionData.Operator = Convert.ToString(row[7]);
                    CompressionData.TestDate = Convert.ToString(row[8]);
                    CompressionData.TestTime = Convert.ToString(row[9]);
                    CompressionList.Add(CompressionData);
                }
                return SaveCompressionData(CompressionList);

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
        ApiViewModel UploadBearing(DataTable data)
        {
            ApiViewModel sendMessage = new ApiViewModel();
            sendMessage.Message = "";
            sendMessage.Success = false;
            try
            {
                var listBearingData = new List<BearingViewModel>();

                foreach (DataRow row in data.Rows)
                {
                    if (row[0] == null || row[0].ToString().Trim() == "" || new Regex("[^ -~]+").IsMatch(row[0].ToString().Trim()))
                    {
                        continue;
                    }
                    listBearingData.Add(new BearingViewModel()
                    {
                        WorkStudyID = Convert.ToString(row[0]),
                        TestNo = Convert.ToInt32(row[1]),
                        SubConduct = Convert.ToDecimal(row[2]),
                        SurfConduct = Convert.ToDecimal(row[3]),
                        eDCalc = Convert.ToDecimal(row[4]),
                        FbruKsi = Convert.ToDecimal(row[5]),
                        FbryKsi = Convert.ToDecimal(row[6]),
                        SpeciComment = Convert.ToString(row[7]),
                        Operator = Convert.ToString(row[8]),
                        TestDate = Convert.ToString(row[9]),
                        TestTime = Convert.ToString(row[10])
                    });
                }
                return SaveBearingData(listBearingData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                sendMessage.Success = false;
                sendMessage.Message = ex.Message.ToString();
            }
            return sendMessage;
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
        private ApiViewModel UploadShear(DataTable data)
        {
            ApiViewModel importData = new ApiViewModel();
            List<ShearViewModel> ShearList = new List<ShearViewModel>();
            try
            {
                foreach (DataRow row in data.Rows)
                {
                    if (row[0] == null || row[0].ToString().Trim() == "" || new Regex("[^ -~]+").IsMatch(row[0].ToString().Trim()))
                    {
                        continue;
                    }
                    ShearList.Add(new ShearViewModel()
                    {
                        WorkStudyID = Convert.ToString(row[0]),
                        TestNo = Convert.ToInt32(row[1]),
                        SubConduct = Convert.ToDecimal(row[2]),
                        SurfConduct = Convert.ToDecimal(row[3]),
                        FsuKsi = Convert.ToDecimal(row[4]),
                        SpeciComment = Convert.ToString(row[5]),
                        Operator = Convert.ToString(row[6]),
                        TestDate = Convert.ToString(row[7]),
                        TestTime = Convert.ToString(row[8])
                    });
                }
                return SaveShearData(ShearList);
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
        #region NotchYield
        private ApiViewModel UploadNotchYield(DataTable data)
        {
            ApiViewModel importData = new ApiViewModel();
            List<NotchYieldViewModel> NotchYieldList = new List<NotchYieldViewModel>();
            try
            {
                foreach (DataRow row in data.Rows)
                {
                    if (row[0] == null || row[0].ToString().Trim() == "" || new Regex("[^ -~]+").IsMatch(row[0].ToString().Trim()))
                    {
                        continue;
                    }
                    NotchYieldList.Add(
                        new NotchYieldViewModel()
                        {
                            WorkStudyID = Convert.ToString(row[0]),
                            TestNo = Convert.ToInt32(row[1]),
                            SubConduct = Convert.ToDecimal(row[2]),
                            SurfConduct = Convert.ToDecimal(row[3]),
                            NotchStrengthKsi = Convert.ToDecimal(row[4]),
                            YieldStrengthKsi = Convert.ToDecimal(row[5]),
                            NotchYieldRatio = Convert.ToDecimal(row[6]),
                            SpeciComment = Convert.ToString(row[7]),
                            Operator = Convert.ToString(row[8]),
                            TestDate = Convert.ToString(row[9]),
                            TestTime = Convert.ToString(row[10])
                        }
                        );
                }
                return SaveNotchYieldData(NotchYieldList);
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
        #region ResidualStrength
        private ApiViewModel UploadResidualStrength(DataTable data)
        {
            ApiViewModel importData = new ApiViewModel();
            List<ResidualStrengthViewModel> ResidualStrengthList = new List<ResidualStrengthViewModel>();
            try
            {
                foreach (DataRow row in data.Rows)
                {
                    if (row[0] == null || row[0].ToString().Trim() == "" || new Regex("[^ -~]+").IsMatch(row[0].ToString().Trim()))
                    {
                        continue;
                    }
                    ResidualStrengthList.Add(new ResidualStrengthViewModel()
                    {
                        WorkStudyID = Convert.ToString(row[0]),
                        TestNo = Convert.ToInt32(row[1]),
                        SubConduct = Convert.ToDecimal(row[2]),
                        SurfConduct = Convert.ToDecimal(row[3]),
                        Validity = Convert.ToString(row[4]),
                        ResidualStrength = Convert.ToDecimal(row[5]),
                        PmaxLBS = Convert.ToDecimal(row[6]),
                        WIn = Convert.ToDecimal(row[7]),
                        BIn = Convert.ToDecimal(row[8]),
                        AvgFinalPreCrack = Convert.ToDecimal(row[9]),
                        SpeciComment = Convert.ToString(row[10]),
                        Operator = Convert.ToString(row[11]),
                        TestDate = Convert.ToString(row[12]),
                        TestTime = Convert.ToString(row[13])
                    });
                }
                return SaveResidualStrengthData(ResidualStrengthList);
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
        private ApiViewModel UploadFractureToughness(DataTable data)
        {
            ApiViewModel importData = new ApiViewModel();
            List<FractureToughnessViewModel> FractureToughnessList = new List<FractureToughnessViewModel>();
            try
            {
                foreach (DataRow row in data.Rows)
                {

                    if (row[0] == null || row[0].ToString().Trim() == "" || new Regex("[^ -~]+").IsMatch(row[0].ToString().Trim()))
                    {
                        continue;
                    }
                    FractureToughnessList.Add(new FractureToughnessViewModel()
                    {
                        WorkStudyID = Convert.ToString(row[0]),
                        TestNo = Convert.ToInt32(row[1]),
                        SubConduct = Convert.ToDecimal(row[2]),
                        SurfConduct = Convert.ToDecimal(row[3]),
                        Validity = Convert.ToString(row[4]),
                        KKsiIn = Convert.ToDecimal(row[5]),
                        KmaxKsiIn = Convert.ToDecimal(row[6]),
                        PqLBS = Convert.ToDecimal(row[7]),
                        PmaxLBS = Convert.ToDecimal(row[8]),
                        aOIn = Convert.ToDecimal(row[9]),
                        WIn = Convert.ToDecimal(row[10]),
                        BIn = Convert.ToDecimal(row[11]),
                        BnIn = Convert.ToDecimal(row[12]),
                        AvgFinalPreCrack = Convert.ToDecimal(row[13]),
                        SpeciComment = Convert.ToString(row[14]),
                        Operator = Convert.ToString(row[15]),
                        TestDate = Convert.ToString(row[16]),
                        EntryDate = Convert.ToString(row[17]),
                        blank1 = row[18] == null ? "" : Convert.ToString(row[18]),
                        blank2 = row[19] == null ? "" : Convert.ToString(row[19]),
                        blank3 = row[20] == null ? "" : Convert.ToString(row[20]),
                        // blank4 = Convert.ToString(row[21]),
                    });
                }
                return SaveFractureToughnessData(FractureToughnessList);
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
        private ApiViewModel UploadModulusTension(DataTable data)
        {
            ApiViewModel importData = new ApiViewModel();
            List<ModulusTensionDataViewModel> ModulusTensionList = new List<ModulusTensionDataViewModel>();
            try
            {
                foreach (DataRow row in data.Rows)
                {
                    if (row[0] == null || row[0].ToString().Trim() == "" || new Regex("[^ -~]+").IsMatch(row[0].ToString().Trim()))
                    {
                        continue;
                    }
                    ModulusTensionList.Add(new ModulusTensionDataViewModel()
                    {
                        WorkStudyID = Convert.ToString(row[0]),
                        TestNo = Convert.ToInt32(row[1]),
                        SubConduct = Convert.ToDecimal(row[2]),
                        SurfConduct = Convert.ToDecimal(row[3]),
                        EModulusTension = Convert.ToDecimal(row[4]),
                        SpeciComment = Convert.ToString(row[5]),
                        Operator = Convert.ToString(row[6]),
                        TestDate = Convert.ToString(row[7]),
                        TestTime = Convert.ToString(row[8])


                    });
                }
                return SaveModulusTensionData(ModulusTensionList);
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
        private ApiViewModel UploadModulusCompression(DataTable data)
        {
            ApiViewModel importData = new ApiViewModel();
            List<ModulusCompressionDataViewModel> ModulusCompressionList = new List<ModulusCompressionDataViewModel>();
            try
            {
                foreach (DataRow row in data.Rows)
                {
                    if (row[0] == null || row[0].ToString().Trim() == "" || new Regex("[^ -~]+").IsMatch(row[0].ToString().Trim()))
                    {
                        continue;
                    }
                    ModulusCompressionList.Add(new ModulusCompressionDataViewModel()
                    {
                        WorkStudyID = Convert.ToString(row[0]),
                        TestNo = Convert.ToInt32(row[1]),
                        SubConduct = Convert.ToDecimal(row[2]),
                        SurfConduct = Convert.ToDecimal(row[3]),
                        EModulusCompression = Convert.ToDecimal(row[4]),
                        SpeciComment = Convert.ToString(row[5]),
                        Operator = Convert.ToString(row[6]),
                        TestDate = Convert.ToString(row[7]),
                        TestTime = Convert.ToString(row[8])

                    });

                }
                return SaveModulusCompressionData(ModulusCompressionList);
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
        #region Fatigue
        private ApiViewModel UploadFatigue(DataTable data)
        {
            ApiViewModel importData = new ApiViewModel();
            List<FatigueTestingDataViewModel> FatigueTestingList = new List<FatigueTestingDataViewModel>();
            try
            {
                foreach (DataRow row in data.Rows)
                {
                    if (row[0] == null || row[0].ToString().Trim() == "" || new Regex("[^ -~]+").IsMatch(row[0].ToString().Trim()))
                    {
                        continue;
                    }
                    FatigueTestingList.Add(
                        new FatigueTestingDataViewModel()
                        {

                            WorkStudyID = Convert.ToString(row[0]),
                            TestNo = Convert.ToInt32(row[1]),
                            SpecimenDrawing = Convert.ToString(row[2]),
                            MinStress = Convert.ToDecimal(row[3]),
                            MaxStress = Convert.ToDecimal(row[4]),
                            MinLoad = Convert.ToDecimal(row[5]),
                            MaxLoad = Convert.ToDecimal(row[6]),
                            WidthOrDia = Convert.ToDecimal(row[7]),
                            Thickness = Convert.ToDecimal(row[8]),
                            HoleDia = Convert.ToDecimal(row[9]),
                            AvgChamferDepth = Convert.ToDecimal(row[10]),
                            Frequency = Convert.ToString(row[11]),
                            CyclesToFailure = Convert.ToDecimal(row[12]),
                            Roughness = Convert.ToDecimal(row[13]),
                            TestFrame = Convert.ToString(row[14]),
                            Comment = Convert.ToString(row[15]),
                            FractureLocation = Convert.ToString(row[16]),
                            Operator = Convert.ToString(row[17]),
                            TestDate = Convert.ToString(row[18]),
                            EntryDate = Convert.ToString(row[19]),
                            TestTime = Convert.ToString(row[20]),
                            field1 = Convert.ToString(row[21])
                        }
                        );
                }
                return SaveFatigueTestingData(FatigueTestingList);
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