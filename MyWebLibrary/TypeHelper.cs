using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyWebLibrary
{
    public class TypeHelper
    {
        /// <summary>
        /// 将数字类型字符串转为同等整数数值类型,如果传入为其他字符或负数字符串则返回数值0
        /// </summary>
        /// <param name="numStr"></param>
        /// <returns></returns>
        public static int ParseToInteger(string numStr)
        {
            int i = int.TryParse(numStr, out i) ? i : 0;
            return i < 0 ? 0 : i;
        }

        /// <summary>
        /// 将数字类型字符串转为同等数值类型
        /// </summary>
        /// <param name="numStr"></param>
        /// <returns></returns>
        public static int ParseToNumerical(string numStr)
        {
            int n = int.TryParse(numStr, out n) ? n : 0;
            return n;
        }
    }
}
