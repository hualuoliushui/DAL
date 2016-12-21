﻿using DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class Role_PermissionDAO : DAOBase
    {
       //本地静态字段
        static int IDMax;

        //本地静态字段
        static string TableName;

        static Role_PermissionDAO()
        {
            TableName = "role_permission";
            IDMax = getIDMax(TableName);
        }

        public Role_PermissionDAO()
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
