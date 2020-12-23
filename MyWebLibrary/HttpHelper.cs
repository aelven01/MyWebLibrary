using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace MyWebLibrary
{
    public class HttpHelper
    {
        public static string HttpPost(string url, string senderData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] buffer = Encoding.UTF8.GetBytes(senderData); // 转化
            request.ContentLength = buffer.Length;

            Stream myRequestStream = request.GetRequestStream();
            myRequestStream.Write(buffer, 0, buffer.Length);
            myRequestStream.Flush();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string resultString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return resultString;
        }

        /// <summary>
        /// 不同编码的重载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="senderData"></param>
        /// <returns></returns>
        public static string HttpPost(string url, string senderData,string encode)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] buffer = Encoding.UTF8.GetBytes(senderData); // 转化
            request.ContentLength = buffer.Length;

            Stream myRequestStream = request.GetRequestStream();
            myRequestStream.Write(buffer, 0, buffer.Length);
            myRequestStream.Flush();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(encode));
            string resultString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return resultString;
        }
    }
}
