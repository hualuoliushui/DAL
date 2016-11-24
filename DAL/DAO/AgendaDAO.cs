using DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.DB;

namespace DAL.DAO
{
    public class AgendaDAO : DAOBase
    {
         //本地静态字段
        static int IDMax;

        //本地静态字段
        static string TableName;

        static AgendaDAO()
        {
            TableName = "agenda";
            IDMax = getIDMax(TableName);
        }

        public AgendaDAO()
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

        //在删除议程之前，修改会议中，大于指定议程序号，减1
        public int updateIndex(int meetingID,int agendaIndex)
        {
            string commandText = "update "+ TableName +" set agendaIndex=agendaIndex-1 where meetingID=@meetingID and agendaIndex > @agendaIndex;";
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter { name = "meetingID", value = meetingID });
            parameters.Add(new Parameter { name = "agendaIndex", value = agendaIndex });
            return DBFactory.GetInstance().ExecuteNonQuery(commandText, parameters);
        }
    }
}
