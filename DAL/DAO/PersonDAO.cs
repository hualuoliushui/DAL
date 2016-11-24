﻿
using DAL.Base;

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

        public static int getID() //应该在返回之前都应该在临界区。。。
        {
            int id = 0;
            Mutex mutex = new Mutex(false, TableName);

            mutex.WaitOne();
            Interlocked.Increment(ref IDMax);
            id = IDMax;
            mutex.ReleaseMutex();

            return id;
        }
    }
}
