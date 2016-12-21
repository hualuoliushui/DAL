
using DAL.Base;
using System;
using System.Threading;

namespace DAL.DAO
{
    public class PersonDAO : DAOBase
    {
        //本地静态字段
        static int IDMax;

        //本地静态字段
        static string TableName;

        static PersonDAO()
        {
            TableName = "person";
            IDMax = getIDMax(TableName);
        }

        public PersonDAO()
        {
            //赋值父类动态字段
            databaseTableName = TableName;
        }

        public static int getID()
        {
            int id = 0;
            Mutex mutex = null;
            try
            {
                mutex = new Mutex(false, TableName);
                mutex.WaitOne();
                Interlocked.Increment(ref IDMax);
                id = IDMax;
            }
            catch (Exception e)
            {
                Log.LogInfo("获取" + TableName + "ID", e);
                return -1;
            }
            finally
            {
                //释放锁
                while (mutex != null)
                {
                    mutex.ReleaseMutex();
                    mutex = null;
                }
            }
            return id;
        }
    }
}
