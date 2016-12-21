using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Base;
using System.Threading;
using DAL.DB;
using System.Data;
namespace DAL.DAO
{
    public class VoteDAO : DAOBase
    {
       //本地静态字段
        static int IDMax;

        //本地静态字段
        static string TableName;

        static VoteDAO()
        {
            TableName = "vote";
            IDMax = getIDMax(TableName);
        }

        public VoteDAO()
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

        //在删除投票之前，修改议程中，大于指定投票序号，减1
        public int updateIndex(int agendaID, int voteIndex)
        {
            string commandText = "update " + TableName + " set voteIndex=voteIndex-1 where agendaID=@agendaID and voteIndex > @voteIndex;";
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter { name = "agendaID", value = agendaID });
            parameters.Add(new Parameter { name = "voteIndex", value = voteIndex });
            return DBFactory.GetInstance().ExecuteNonQuery(commandText, parameters);
        }

        public int getMaxIndex(int agendaID)
        {
            StringBuilder commandText = new StringBuilder();
            commandText.Append("select max(voteIndex) from ");
            commandText.Append(databaseTableName);
            commandText.Append(" where ");
            commandText.Append("agendaID = @agendaID;");

            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter { name = "agendaID", value = agendaID });
            DataTable dt = DBFactory.GetInstance().ExecuteQuery(commandText.ToString(), parameters);

            if (dt != null && dt.Rows.Count != 0)
            {
                DataRow row = dt.Rows[0];
                return Int32.Parse(row[0].ToString());
            }
            return 0;
        }
    }
}
