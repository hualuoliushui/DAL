using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Base;
using System.Threading;
namespace DAL.DAO
{
    public class VoteOptionPersonResultDAO : DAOBase
    {
        //本地静态字段
        static int IDMax;

        //本地静态字段
        static string TableName;

        static VoteOptionPersonResultDAO()
        {
            TableName = "voteOptionPersonResult";
            IDMax = getIDMax(TableName);
        }

        public VoteOptionPersonResultDAO()
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
