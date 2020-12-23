using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MyWebLibrary
{
    public class JsonHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">-1表示未登录</param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetResultStr(int code, string msg, string data)
        {
            string dataStr = data == "" ? "\"data\":\"\"" : "\"data\":" + data;
            return "{\"code\":" + code + ",\"msg\":\"" + msg + "\"," + dataStr + "}";
        }

        public static string GetLayuiJsonResultStr(int code, string msg, int count,string data)
        {
            return "{\"code\":" + code + ",\"msg\":\"" + msg + "\",\"count\":" + count + ","+"\"data\":"+data+"}";
        }

        public static string GetResultHtmlContent(string state, string msg, string htmlContentString)
        {
            return "{\"state\":\"" + state + "\",\"msg\":\"" + msg + "\",\"htmlContent\":\"" + htmlContentString + "\"}";
        }

        public static string GetMyViewListResultDataRowJson(string state, string msg, int dataCount, string dataRowArrayStr)
        {
            if (dataRowArrayStr == "")
                dataRowArrayStr = "\"\"";
            return "{\"state\":\"" + state + "\",\"msg\":\"" + msg + "\",\"dataCount\":\"" + dataCount + "\",\"dataRows\":" + dataRowArrayStr + "}";
        }

        public static string GetDataRowsJson(DataTable dt)
        {
            string jsonTmp = "\"{0}\":\"{1}\"";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j == 0)
                    {
                        sb.Append("{");
                        sb.Append("\"rowId\":\"" + dt.Rows[i][0] + "\"");
                        continue;
                    }
                    sb.Append(",").Append(string.Format(jsonTmp, dt.Columns[j], dt.Rows[i][dt.Columns[j]]));
                }
                sb.Append("}");
                if (i + 1 < dt.Rows.Count)
                {
                    sb.Append(",");
                }
            }
            return "[" + sb.ToString() + "]";
        }
    }
}
