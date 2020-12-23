using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MyWebLibrary
{
    public class LocalDbHelper
    {
        /// <summary>
        /// 创建一个DataTable,直接传入列名数组即可
        /// </summary>
        /// <param name="columnNames">要创建的列名数组</param>
        /// <returns>DataTable</returns>
        public static DataTable CreatDataTable(string[] columnNames)
        {
            DataTable t = new DataTable();
            for (int i = 0; i < columnNames.Length; i++)
            {
                t.Columns.Add(columnNames[i]);   
            }
            return t;
        }
    }
}
