using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Base;
using System.Threading;
namespace DAL.DAO
{
    public class MeetingDAO : DAOBase
    {
       //本地静态字段
        static int IDMax;

        //本地静态字段
        static string TableName = "meeting";

        static MeetingDAO()
        {
            IDMax = getIDMax(TableName);
        }

        public MeetingDAO()
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
