using DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class AgendaDAO : DAOBase
    {
         //本地静态字段
        static int IDMax;

        //本地静态字段
        static string TableName = "agenda";

        static AgendaDAO()
        {
            IDMax = getIDMax(TableName);
        }

        public AgendaDAO()
        {
            //赋值父类动态字段
            databaseTableName = TableName;
        }

        public static int getID()
        {
            Interlocked.Increment(ref IDMax);
            return IDMax;
        }
    }
}
