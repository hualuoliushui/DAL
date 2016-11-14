
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using DAL.Base;

namespace DAL.DB
{
    public abstract class DB
    {
        protected static string connectionString = GetConnectionString();

        private static string GetConnectionString()
        {
            if (ConfigurationManager.ConnectionStrings["DBConnString"] != null)
            {
                return ConfigurationManager.ConnectionStrings["DBConnString"].ConnectionString;
            }
            
            return null;
        }
   
        abstract public int ExecuteNonQuery(string cmdText, List<Parameter> parameters);

        abstract public DataTable ExecuteQuery(string cmdText, List<Parameter> parameters);

        abstract public int ExecuteTransation(List<string> transationsCmdText, List<List<Parameter>> transationsParameters);
    }
}