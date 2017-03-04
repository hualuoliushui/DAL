using DAL.Base;
using DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class Person_RoleDAO : DAOBase
    {
        //本地静态字段
        static int IDMax;

        //本地静态字段
        static string TableName;

        static Person_RoleDAO()
        {
            TableName = "person_role";
            IDMax = getIDMax(TableName);
        }

        public Person_RoleDAO()
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

        //删除 除管理员之外的全部人员
        public int deleteAll()
        {
            string commandText = "delete from " + TableName + " where personID!=1";
            return DBFactory.GetInstance().ExecuteNonQuery(commandText, null);
        }
    }
}
