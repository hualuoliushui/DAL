using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using DAL.Base;

namespace DAL.DB
{
    /// <summary>
    /// access数据库访问
    /// 查询语句中不允许存在access数据库的关键字，譬如user，所以不应该以user作为表名。
    /// 时间需做特殊处理。
    /// </summary>
    public class OleDB : DB
    {
        public override int ExecuteNonQuery(string commandText, List<Parameter> parameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    OleDbCommand com = new OleDbCommand(commandText, conn);
                    if (parameters != null)
                    {
                        foreach (Parameter parameter in parameters)
                        {
                            if (parameter.value!= null && string.Compare(parameter.value.GetType().Name, "DateTime") == 0)
                            {
                                DateTime dt = (DateTime)parameter.value;
                                com.Parameters.AddWithValue(parameter.name, new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second));
                                continue;
                            }
                            //排除null
                            if (parameter.value == null) parameter.value = "";
                            com.Parameters.AddWithValue(parameter.name,(object)(parameter.value));
                            
                        }
                    }
                    return com.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public override DataTable ExecuteQuery(string commandText, List<Parameter> parameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    OleDbDataAdapter da = new OleDbDataAdapter(commandText, conn);
                    da.SelectCommand.Parameters.Clear();
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            if (string.Compare(parameter.value.GetType().Name, "DateTime") == 0)
                            {
                                DateTime dt = (DateTime)parameter.value;
                                da.SelectCommand.Parameters.AddWithValue(parameter.name, new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second));
                                continue;
                            }
                            da.SelectCommand.Parameters.AddWithValue(parameter.name, (object)(parameter.value));
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
                finally
                {
                    conn.Close();
                }
            }
        }

        public override int ExecuteTransation(List<string> transationsCommandText, List<List<Parameter>> transationsParameters)
        {
            throw new NotImplementedException();
        }
    }
}