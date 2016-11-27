﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Base;
using DAL.DB;
using System.Threading;
namespace DAL.DAO
{
    public class MeetingDAO : DAOBase
    {
       //本地静态字段
        static int IDMax;

        //本地静态字段
        static string TableName;

        static MeetingDAO()
        {
            TableName = "meeting";
            IDMax = getIDMax(TableName);
        }

        public MeetingDAO()
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

        public int increateDuration(int meetingID, int agendaDuration)
        {
            string commandText = "update " + TableName + " set meetingDuration=(meetingDuration+@agendaDuration) where meetingID=@meetingID";
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(
                new Parameter
                {
                    name="agendaDuration",
                    value=agendaDuration
                });
            parameters.Add(
                new Parameter
                {
                    name = "meetingID",
                    value = meetingID
                });
            return DBFactory.GetInstance().ExecuteNonQuery(commandText, parameters);
        }
    }
}
