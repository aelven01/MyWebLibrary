using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace MyWebLibrary
{
   public class WordHelper
    {
       public DataTable ExcelToDataTable(string filePath,string excelTableName)
       {
           string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filePath + ";" + "Extended Properties=Excel 8.0;";
           OleDbConnection conn = new OleDbConnection(strConn);
           conn.Open();
           string strExcel = "";
           OleDbDataAdapter myCommand = null;
           DataSet ds = null;
           strExcel = "select * from [" + excelTableName + "$]";
           myCommand = new OleDbDataAdapter(strExcel, strConn);
           ds = new DataSet();
           myCommand.Fill(ds, "table1");
           return ds.Tables[0];
       }
    }
}
