using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using DAL.Base;

namespace DAL.DB
{
    /// <summary>
    /// mysql数据库访问
    /// 参数化查询语句中，表名不能使用中括号:[]，时间不用做特殊处理。
    /// </summary>
    public class MysqlDB : DB
    {
        public override int ExecuteNonQuery(string commandText,List<Parameter> parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand com = new MySqlCommand(commandText, conn);
                    if (parameters != null)
                    {
                        foreach (Parameter parameter in parameters)
                        {
                            com.Parameters.AddWithValue(parameter.name,(parameter.value));
                        }
                    }
                   
                    return com.ExecuteNonQuery();
                }
                //catch (MySqlException mysqlException)
                //{
                //    return -1;
                //}
                finally{

                }
            }
        }

        public override DataTable ExecuteQuery(string commandText, List<Parameter> parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(commandText, conn);
                    da.SelectCommand.Parameters.Clear();
                    if (parameters != null)
                    {
                        foreach (Parameter parameter in parameters)
                        {
                            da.SelectCommand.Parameters.AddWithValue(parameter.name, (parameter.value));
                        }
                    }
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count == 1)
                    {
                        return ds.Tables[0];
                    }
                    return null;
                }
                //catch (MySqlException mysqlException)
                //{
                //    return null;
                //}
                finally
                {

                }
            }
        }

        public override int ExecuteTransation(List<string> transationsCommandText, List<List<Parameter>> transationsParameters)
        {
            throw new NotImplementedException();
        }
    }
}