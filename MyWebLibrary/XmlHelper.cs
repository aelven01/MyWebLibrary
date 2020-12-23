using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;

namespace MyWebLibrary
{
    public class XmlHelper
    {
        /// <summary>
        /// 将XML字符串中的指定节点转换为DataTable
        /// </summary>
        /// <param name="xmlSource">可以是XML字符串也可以是xml文件路径</param>
        /// <param name="xpath">xmlNode节点名称</param>
        /// <returns>转换后的DataTable</returns>
        public static DataTable XmlSingleNodeToTable(string xmlSource, string xpath)
        {
            XmlDocument doc = new XmlDocument();
            //is path
            if (xmlSource.Substring(xmlSource.Length - 4).ToLower() == ".xml")
            {
                doc.Load(xmlSource);
            }
            //is xmlString
            else
            {
                doc.LoadXml(xmlSource);
            }
            XmlNode node = doc.SelectSingleNode(xpath);
            XmlNodeReader xnr = new XmlNodeReader(node);
            DataSet ds = new DataSet();
            ds.ReadXml(xnr);
            return ds.Tables[0];
        }

        /// <summary>
        /// 判断XmlNode是否为null,不是则返回InnerText值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string GetNodeInnerText(XmlNode node)
        {
            return node == null ? "" : node.InnerText;
        }

        /// <summary>
        /// node为null返回DBNull.Value否则返回InnerText值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static object NodeIsNullToDBNull(XmlNode node)
        {
            return node == null ? DBNull.Value : (object)node.InnerText;
        }
    }
}
