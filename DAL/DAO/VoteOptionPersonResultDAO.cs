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
        static string TableName = "voteOptionPersonResult";

        static VoteOptionPersonResultDAO()
        {
            IDMax = getIDMax(TableName);
        }

        public VoteOptionPersonResultDAO()
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
