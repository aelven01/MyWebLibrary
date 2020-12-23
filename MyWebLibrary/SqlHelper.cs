using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;

namespace MyWebLibrary
{
    public class SqlHelper
    {
        //public static string conStr = "Data Source=120.76.234.240,15585;Initial Catalog=Ekding_HotelDistribution;User Id=sa;Password=Hengaokjsql~pwd999.^@#admin_aelven.Ekding;";
        public static readonly string conStr = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;
        
        ///<summary>
        ///批量插入数据库,直接获取datatable的列名
        ///</summary>
        ///<param name="dbTableName">数据库表名称</param>
        public static bool BulkCopy(DataTable dt, string dbTableName)
        {
            if (dt == null || dt.Rows.Count == 0)
                return false;
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conStr))
            {
                bulkCopy.DestinationTableName = dbTableName;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    bulkCopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                }
                bulkCopy.WriteToServer(dt);
            }
            return true;
        }

        ///<summary>
        ///批量插入数据库,传入数据库对应的列名称
        ///</summary>
        ///<param name="dbColumnNames">数据库表中的对应列名</param>
        ///<param name="dbTableName">数据库表名称</param>
        public static bool BulkCopy(DataTable dt,string[] dbColumnNames, string dbTableName)
        {
            if (dt == null || dt.Rows.Count == 0)
                return false;
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conStr))
            {
                bulkCopy.DestinationTableName = dbTableName;
                for (int i = 0; i < dbColumnNames.Length; i++)
                {
                    bulkCopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dbColumnNames[i]);
                }
                bulkCopy.WriteToServer(dt);
            }
            return true;
        }

        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="ds">装载需要更新的DataTable表</param>
        /// <param name="QueryUpdateDataSql">用于查询需要更新对应的目标表数据字符串</param>
        /// <param name="dsTableNameStr">需要更新的DataTableName</param>
        /// <returns>返回更新成功数量</returns>
        public static int BtchUpdateDataSet(DataSet ds, string QueryUpdateDataSql, string dsTableNameStr)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                SqlDataAdapter command = new SqlDataAdapter(QueryUpdateDataSql, connection);
                SqlCommandBuilder cb = new SqlCommandBuilder(command);//此命令自动生成相关新增、删除、更新command
                cb.ConflictOption = ConflictOption.OverwriteChanges;
                int result = command.Update(ds, dsTableNameStr);
                connection.Close();
                return result;
            }
        }

        /// <summary>
        /// 与BtchUpdateDataSet方法相同,只不过直接传入DataTable
        /// </summary>
        /// <param name="dt">更新后的DataTable</param>
        /// <param name="QueryUpdateDataSql">用于查询需要更新对应的目标表数据字符串</param>
        /// <returns>返回更新成功数量</returns>
        public static int BtchUpdateDataTable(DataTable dt, string QueryUpdateDataSql)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                SqlDataAdapter command = new SqlDataAdapter(QueryUpdateDataSql, connection);
                SqlCommandBuilder cb = new SqlCommandBuilder(command);//此命令自动生成相关新增、删除、更新command
                cb.ConflictOption = ConflictOption.OverwriteChanges;
                int result = command.Update(dt.DataSet, dt.TableName);
                connection.Close();
                return result;
            }
        }

        /// <summary>
        /// 执行增删改查方法
        /// </summary>
        /// <param name="sql">sql字符串</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 执行增删改查方法
        /// </summary>
        /// <param name="sql">sql字符串</param>
        /// <param name="parameters">SqlParameter参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    return ds.Tables[0];
                }
            }
        }

        //只有1个参数重载
        public static DataTable ExecuteDataTable(string sql)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    return ds.Tables[0];
                }
            }
        }

        //parameter类型集合参数重载
        public static DataTable ExecuteDataTable(string sql, List<SqlParameter> parameters)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    return ds.Tables[0];
                }
            }
        }

        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }

                    return cmd.ExecuteScalar();
                }
            }
        }

        //parameter类型集合参数重载
        public static object ExecuteScalar(string sql, List<SqlParameter> parameters)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }

                    return cmd.ExecuteScalar();
                }
            }
        }

        //只有1个参数重载
        public static object ExecuteScalar(string sql)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;

                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 获取查询Sql字符串
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="tableName"></param>
        /// <param name="ColumnFieldStr"></param>
        /// <param name="queryConditions"></param>
        /// <param name="orderByStr"></param>
        /// <returns></returns>
        public static string GetQuerySql(int pageSize, string ColumnFieldStr, string tableName, string queryConditions, string orderByStr)
        {
            string sql1 = "SELECT TOP " + pageSize + " " + ColumnFieldStr + " FROM " + tableName;
            string sql2 = string.IsNullOrEmpty(queryConditions) ? " " : " WHERE " + queryConditions + " ";
            return sql1 + sql2 + orderByStr;
        }

        public static string GetQuerySql(string ColumnFieldStr, string tableName, string queryConditions, string orderByStr)
        {
            string sql1 = "SELECT " + ColumnFieldStr + " FROM " + tableName;
            string sql2 = string.IsNullOrEmpty(queryConditions) ? " " : " WHERE " + queryConditions + " ";
            return sql1 + sql2 + orderByStr;
        }

        /// <summary>
        /// 获取分页查询的Sql字符串
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="tableName"></param>
        /// <param name="ColumnFieldStr">查询全部字段使用*,其他如id,name这样,字段中必须包含id</param>
        /// <param name="queryConditions">查询条件如id=1 AND name='bill'</param>
        /// <param name="orderByStr"></param>
        /// <returns></returns>
        public static string GetQueryPageSql(int page, int pageSize, string tableName, string ColumnFieldStr, string queryConditions, string orderByStr)
        {
            ColumnFieldStr = ColumnFieldStr == "" ? "*" : ColumnFieldStr;
            string pageConditionsSql = queryConditions == "" ? "" : "WHERE " + queryConditions + " ";
            string queryPagePartialSql = "id NOT IN(SELECT TOP " + page * pageSize + " id FROM " + tableName + " " + pageConditionsSql + orderByStr + ")";
            string queryConditionsSql = queryConditions == "" ? "" : "AND " + queryConditions + " ";
            return "SELECT TOP " + pageSize + " " + ColumnFieldStr + " FROM " + tableName + " WHERE " + queryPagePartialSql + " " + queryConditionsSql + orderByStr;
        }

        /// <summary>
        /// 获取对应分页查询的数据总数字符串
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="queryConditions"></param>
        /// <returns></returns>
        public static string GetSameQueryPageDataCountSql(string tableName, string queryConditions)
        {
            string conditionsSql = queryConditions == "" ? "" : " WHERE " + queryConditions;
            return "SELECT COUNT(id) FROM " + tableName + conditionsSql;
        }

        /// <summary>
        /// 获取sql查询条件部分字符串
        /// </summary>
        /// <param name="context">HttpContext 对象</param>
        /// <param name="paramKeyTempletes">传入数组格式如 new string[]{"id:id={0}","country:country='{0}'","keyword:nameCn like '%{0}%'"}</param>
        /// <returns>返回结果如 id=1 AND country='china' AND name like '%bill%'</returns>
        public static string GetQueryConditionSql(HttpContext context, string[] paramKeyTempletes)
        {
            List<string> conditions = new List<string>();
            for (int i = 0; i < paramKeyTempletes.Length; i++)
            {
                string[] templetes = paramKeyTempletes[i].Split(':');
                string key = templetes[0];
                string tmp = templetes[1];
                if (!string.IsNullOrEmpty(context.Request[key]))
                {
                    conditions.Add(string.Format(tmp, context.Request[key]));
                }
            }
            return string.Join(" AND ", conditions);
        }
    }
}
