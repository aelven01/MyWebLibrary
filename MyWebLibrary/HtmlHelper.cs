using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;

namespace MyWebLibrary
{
    public class HtmlHelper
    {
        /// <summary>
        /// 适用于web,直接在页面输出整个DataTable的HTML Table
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dt"></param>
        public static void ResponseHtmlTable(HttpContext context,DataTable dt)
        {
            context.Response.Write("<table>");
            //thead
            context.Response.Write("<thead>");                
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                context.Response.Write("<th>" + dt.Columns[i].ColumnName+ "</th>");
            }
            context.Response.Write("</thead>");                

            //tbody
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                context.Response.Write("<tr>");
                //tds
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    context.Response.Write("<td>"+dt.Rows[i][c]+"</td>");
                }
                context.Response.Write("</tr>");
                
            }
            context.Response.Write("</table>");
        }

        public static string DataTableToTableString(string className,string thTtiles,DataTable dt)
        {
            if (dt.Rows.Count < 1)
            {
                return "";
            }
            string[] ths = thTtiles.Split(',');
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class='" + className + "'><thead>");
            for (int i = 0; i < ths.Length; i++)
            {
                sb.Append("<th>" + ths[i] + "</th>");
            }
            sb.Append("</thead>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<tr>");
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    string value = dt.Rows[i][c].ToString();
                    if (dt.Columns[c].ColumnName == "date" || dt.Columns[c].ColumnName == "date2")
                    {
                        value = Convert.ToDateTime(dt.Rows[i][c]).ToString("yyyy-M-d");
                    }
                    else if (dt.Columns[c].ColumnName == "dateTime")
                    {
                        value = Convert.ToDateTime(dt.Rows[i][c]).ToString("yyyy-M-d HH:mm:ss");
                    }
                    else if (dt.Columns[c].ColumnName == "price")
                    {
                        if (value.Contains("."))
                        {
                            value = Convert.ToDecimal(dt.Rows[i][c]).ToString();
                        }
                        else
                        {
                            value = Convert.ToInt16(dt.Rows[i][c]).ToString();
                        }
                    }
                    sb.Append("<td>" +value+ "</td>");   
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }
    }
}
