using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using eT.Dal;
using eT.Model;
namespace eT.Common
{
    public class DataHelper
    {
        public static DbQueryResult GetPageData(Pagination query, string tableName, StringBuilder builder, string keyFieldID, string selectFields = "*")
        {
            return PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, tableName, keyFieldID, builder.ToString(), selectFields);
        }

        public static DbQueryResult PagingByRownumber(int pageIndex, int pageSize, string sortBy, SortAction sortOrder, bool isCount, string table, string pk, string filter, string selectFields)
        {
            return PagingByRownumber(pageIndex, pageSize, sortBy, sortOrder, isCount, table, pk, filter, selectFields, 0);
        }

        public static DbQueryResult PagingByRownumber(int pageIndex, int pageSize, string sortBy, SortAction sortOrder, bool isCount, string table, string pk, string filter, string selectFields, int partitionSize)
        {
            if (string.IsNullOrEmpty(table))
            {
                return null;
            }
            if (string.IsNullOrEmpty(sortBy) && string.IsNullOrEmpty(pk))
            {
                return null;
            }
            if (string.IsNullOrEmpty(selectFields))
            {
                selectFields = "*";
            }
            string query = BuildRownumberQuery(sortBy, sortOrder, isCount, table, pk, filter, selectFields, partitionSize);
            int num = ((pageIndex - 1) * pageSize) + 1;
            int num2 = (num + pageSize) - 1;
            DbQueryResult result = new DbQueryResult();
            SqlConnection cn = new SqlConnection(Init.GetConnectionString());
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.Add("@StartNumber", num);
            cmd.Parameters.Add("@EndNumber", num2);
            cn.Open();
            using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                result.Data = ConverDataReaderToDataTable(reader);
                if ((isCount && (partitionSize == 0)) && reader.NextResult())
                {
                    reader.Read();
                    result.TotalRecords = reader.GetInt32(0);
                }
            }
            return result;
        }

        private static string BuildRownumberQuery(string sortBy, SortAction sortOrder, bool isCount, string table, string pk, string filter, string selectFields, int partitionSize)
        {
            StringBuilder builder = new StringBuilder();
           
            string str = string.IsNullOrEmpty(filter) ? "" : ("WHERE " + filter);

                if (partitionSize > 0)
                {
                    builder.AppendFormat("SELECT TOP {0} {1}, ROW_NUMBER() OVER (ORDER BY ", partitionSize.ToString(CultureInfo.InvariantCulture), selectFields);
                }
                else
                {
                    builder.AppendFormat("SELECT {0} , ROW_NUMBER() OVER (ORDER BY ", selectFields);
                }
                builder.AppendFormat("{0} {1}", string.IsNullOrEmpty(sortBy) ? pk : sortBy, sortOrder.ToString());
                builder.AppendFormat(") AS RowNumber FROM {0} {1}", table, str);
                builder.Insert(0, "SELECT * FROM (").Append(") T WHERE T.RowNumber BETWEEN @StartNumber AND @EndNumber");
                if (isCount && (partitionSize == 0))
                {
                    builder.AppendFormat(";SELECT COUNT(*) FROM {0} {1}",  table, str);
                }
            return builder.ToString();
        }

        private static string BuildTopQuery(int pageIndex, int pageSize, string sortBy, SortAction sortOrder, bool isCount, string table, string pk, string filter, string selectFields)
        {
            string str = string.IsNullOrEmpty(sortBy) ? pk : sortBy;
            string str2 = string.IsNullOrEmpty(filter) ? "" : ("WHERE " + filter);
            string str3 = string.IsNullOrEmpty(filter) ? "" : ("AND " + filter);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT TOP {0} {1} FROM {2} ", pageSize.ToString(CultureInfo.InvariantCulture), selectFields, table);
            if (pageIndex == 1)
            {
                builder.AppendFormat("{0} ORDER BY {1} {2}", str2, str, sortOrder.ToString());
            }
            else
            {
                int num = (pageIndex - 1) * pageSize;
                if (sortOrder == SortAction.Asc)
                {
                    builder.AppendFormat("WHERE {0} > (SELECT MAX({0}) FROM (SELECT TOP {1} {0} FROM {2} {3} ORDER BY {0} ASC) AS TMP) {4} ORDER BY {0} ASC", new object[] { str, num, table, str2, str3 });
                }
                else
                {
                    builder.AppendFormat("WHERE {0} < (SELECT MIN({0}) FROM (SELECT TOP {1} {0} FROM {2} {3} ORDER BY {0} DESC) AS TMP) {4} ORDER BY {0} DESC", new object[] { str, num, table, str2, str3 });
                }
            }
            if (isCount)
            {
                builder.AppendFormat(";SELECT COUNT({0}) FROM {1} {2}", str, table, str2);
            }
            return builder.ToString();
        }


        public static DataTable ConverDataReaderToDataTable(IDataReader reader)
        {
            if (null == reader)
            {
                return null;
            }
            DataTable table = new DataTable
            {
                Locale = CultureInfo.InvariantCulture
            };
            int fieldCount = reader.FieldCount;
            for (int i = 0; i < fieldCount; i++)
            {
                table.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
            }
            table.BeginLoadData();
            object[] values = new object[fieldCount];
            while (reader.Read())
            {
                reader.GetValues(values);
                table.LoadDataRow(values, true);
            }
            table.EndLoadData();
            return table;
        }

        public static string CleanSearchString(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return null;
            }
            searchString = searchString.Replace("*", "%");
            searchString = Regex.Replace(searchString, "<[^>]+>", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            searchString = Regex.Replace(searchString, "--|;|'|\"", " ", RegexOptions.Compiled | RegexOptions.Multiline);
            searchString = Regex.Replace(searchString, " {1,}", " ", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return searchString;
        }

        /// <summary>
        /// 将json转换为DataTable
        /// </summary>
        /// <param name="strJson">得到的json</param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string strJson)
        {
            //转换json格式
            strJson = strJson.Replace(",\"", "*\"").Replace("\":", "\"#").ToString();
            //取出表名   
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;
            //去除表名   
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));

            //获取数据   
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split('*');

                //创建表   
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split('#');

                        if (strCell[0].Substring(0, 1) == "\"")
                        {
                            int a = strCell[0].Length;
                            dc.ColumnName = strCell[0].Substring(1, a - 2);
                        }
                        else
                        {
                            dc.ColumnName = strCell[0];
                        }
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }

            return tb;
        }
    }
}
