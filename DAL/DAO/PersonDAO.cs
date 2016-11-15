
using DAL.Base;

using System.Threading;

namespace DAL.DAO
{
    public class PersonDAO : DAOBase
    {
        //本地静态字段
        static int IDMax;

        //本地静态字段
        static string TableName = "person";

        static PersonDAO()
        {
            IDMax = getIDMax(TableName);
        }

        public PersonDAO()
        {
            //赋值父类动态字段
            databaseTableName = TableName;
        }

        public static int getID()
        {
            Interlocked.Increment(ref IDMax);
            return IDMax;
        }

        public static void init()
        {

        }
    }
}
