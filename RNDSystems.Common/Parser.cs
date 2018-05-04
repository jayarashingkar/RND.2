using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RNDSystems.Models.ViewModels;

namespace RNDSystems.Common
{
    public class Parser
    {
        public static DataTableReturn CsvToDataTable(string path, bool isFirstRowHeader)
        {
            DataTableReturn returnData = new DataTableReturn();
            try
            {
                string header = isFirstRowHeader ? "Yes" : "No";
                string pathOnly = Path.GetDirectoryName(path);
                string fileName = Path.GetFileName(path);

                string sql = @"SELECT * FROM [" + fileName + "]";

                using (OleDbConnection connection = new OleDbConnection(
                          @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
                          ";Extended Properties=\"Text;HDR=" + header + "\""))
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = CultureInfo.CurrentCulture;
                    adapter.Fill(dataTable);
                    //  return dataTable;
                    returnData.data = dataTable;
                    returnData.Message = "data saved";
                    returnData.Success = true;
                }
            }
            catch (Exception ex)
            {
                returnData.Message = ex.Message.ToString();
                returnData.Success = false;
            }
            return returnData;
        }

        //public static DataTable CsvToDataTable(string path, bool isFirstRowHeader)
        //{
        //    string header = isFirstRowHeader ? "Yes" : "No";
        //    string pathOnly = Path.GetDirectoryName(path);
        //    string fileName = Path.GetFileName(path);

        //    string sql = @"SELECT * FROM [" + fileName + "]";

        //    using (OleDbConnection connection = new OleDbConnection(
        //              @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
        //              ";Extended Properties=\"Text;HDR=" + header + "\""))
        //    using (OleDbCommand command = new OleDbCommand(sql, connection))
        //    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
        //    {
        //        DataTable dataTable = new DataTable();
        //        dataTable.Locale = CultureInfo.CurrentCulture;
        //        adapter.Fill(dataTable);
        //        return dataTable;
        //    }
        //}
    }


    //public  class Parser
    //{
    //    public static DataTable CsvToDataTable(string filePath, bool isfile)
    //    {
    //        //   DataTable data = new DataTable();

    //        // string csv_file_path = @"C:\Users\Administrator\Desktop\test.csv";

    //        DataTable csvData = GetDataTabletFromCSVFile(filePath);

    //        //   Console.WriteLine("Rows count:" + csvData.Rows.Count);

    //        // Console.ReadLine();

    //        return csvData;
    //    }

    //    private static DataTable GetDataTabletFromCSVFile(string csv_file_path)

    //    {

    //        DataTable csvData = new DataTable();

    //        try

    //        {

    //            using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))

    //            {

    //                csvReader.SetDelimiters(new string[] { "," });

    //                csvReader.HasFieldsEnclosedInQuotes = true;

    //                string[] colFields = csvReader.ReadFields();

    //                foreach (string column in colFields)

    //                {

    //                    DataColumn datecolumn = new DataColumn(column);

    //                    datecolumn.AllowDBNull = true;

    //                    csvData.Columns.Add(datecolumn);

    //                }

    //                while (!csvReader.EndOfData)

    //                {

    //                    string[] fieldData = csvReader.ReadFields();

    //                    //Making empty value as null

    //                    for (int i = 0; i < fieldData.Length; i++)

    //                    {

    //                        if (fieldData[i] == "")

    //                        {

    //                            fieldData[i] = null;

    //                        }

    //                    }

    //                    csvData.Rows.Add(fieldData);

    //                }

    //            }

    //        }

    //        catch (Exception ex)

    //        {

    //        }

    //        return csvData;

    //    }
    //}

}
