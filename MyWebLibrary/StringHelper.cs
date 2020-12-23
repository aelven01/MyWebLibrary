using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace MyWebLibrary
{
    public class StringHelper
    {
        /// <summary>
        /// 替换掉一个以上的空格,原本空格处仍保留一个空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimSpaces(string str)
        {
            return Regex.Replace(str, @"\s+", " ");
        }

        /// <summary>
        /// 替换掉所有空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimAllSpaces(string str)
        {
            return Regex.Replace(str, @"\s+", "");
        }

        public static string TrimChineseBetweenSpaces(string str)
        {
            return Regex.Replace(str,@"(?<=[\u4e00-\u9fa5])\s+?(?=[\u4e00-\u9fa5])","");
        }

        /// <summary>
        /// 判断字符串是否为英文字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns>bool</returns>
        public static bool IsEnglish(string str)
        {
            Regex pattern = new Regex("^[a-zA-Z]*$");
            return pattern.IsMatch(str);
        }

        /// <summary>
        /// //验证单组或多组英文字符串,每组之间只能是空格符
        /// </summary>
        /// <param name="str"></param>
        /// <returns>bool</returns>
        public static bool IsEnglishWords(string str)
        {
            Regex pattern = new Regex("^([a-zA-Z]+[\\s]*)+[a-zA-Z]+$");
            return pattern.IsMatch(str);
        }

        /// <summary>
        /// /// 替换每组英文词首字母为大写,每组跟随首字后的字母为小写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToFirstUpperWords(string s)
        {
            s = s.ToLower();
            return Regex.Replace(s, @"\b[a-z]\w+", delegate(Match match)
            {
                string v = match.ToString();
                return char.ToUpper(v[0]) + v.Substring(1);
            });
        }

        /// <summary>
        /// 替换英文首字母为大写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToFirstUpper(string s)
        {
            return Regex.Replace(s, @"\b[a-z]\w+", delegate(Match match)
            {
                string v = match.ToString();
                return char.ToUpper(v[0]) + v.Substring(1);
            });
        }

        /// <summary>
        /// 判断字符串是否为中文,包含简体繁体
        /// </summary>
        /// <param name="str"></param>
        /// <returns>bool</returns>
        public static bool IsChinese(string str)
        {
            Regex pattern = new Regex("^[\u4e00-\u9fa5]*$");
            return pattern.IsMatch(str);
        }

        /// <summary>
        /// 简体字符串转繁体
        /// </summary>
        /// <param name="strSimplified">简体字</param>
        /// <returns></returns>
        public static string ToTraditionalChinese(string strSimplified)
        {
            return Microsoft.VisualBasic.Strings.StrConv(strSimplified, Microsoft.VisualBasic.VbStrConv.TraditionalChinese, 0);
        }

        /// <summary>
        /// 繁体字符串转简体
        /// </summary>
        /// <param name="strTraditional">繁体字</param>
        /// <returns></returns>
        public static string ToSimplifiedChinese(string strTraditional)
        {
            return Microsoft.VisualBasic.Strings.StrConv(strTraditional, VbStrConv.SimplifiedChinese, 0);
        }

        public static string StringToBase64(string str)
        {
            System.Text.Encoding encode = System.Text.Encoding.UTF8;
            byte[] bytedata = encode.GetBytes(str);
            return Convert.ToBase64String(bytedata, 0, bytedata.Length);
        }

        public static string Base64ToString(string base64str)
        {
            byte[] bytes = Convert.FromBase64String(base64str);
            return Encoding.GetEncoding("utf-8").GetString(bytes);
        }

    }
}
