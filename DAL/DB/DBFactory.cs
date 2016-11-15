using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace DAL.DB
{
    public class DBFactory
    {
        private static int DBTYPE = GetDBTYPE();

        private static int GetDBTYPE()
        {
            return Int32.Parse(ConfigurationManager.AppSettings["DBTYPE"]);
        }

        public static DB GetInstance(){
            switch (DBTYPE)
            {
                case 0://oleDB
                    return new OleDB();
                case 1://mysqlDB
                    return new MysqlDB();
                default:
                    return null;
            }
        }
    }
}