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

        private static MysqlDB mysqlDB = new MysqlDB();

        private static OleDB oleDB = new OleDB();

        private static int GetDBTYPE()
        {
            return Int32.Parse(ConfigurationManager.AppSettings["DBTYPE"]);
        }

        public static DB GetInstance(){
            switch (DBTYPE)
            {
                case 0://oleDB
                    //while (oleDB==null)
                    //{
                    //    oleDB = new OleDB();
                    //}
                    //return oleDB;
                    return new OleDB();
                case 1://mysqlDB
                    while (mysqlDB == null)
                    {
                        mysqlDB = new MysqlDB();
                    }
                    return mysqlDB;
                default:
                    return null;
            }
        }
    }
}