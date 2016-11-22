using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Base;
using System.Threading;
using DAL.DB;
namespace DAL.DAO
{
    public class FileDAO : DAOBase
    {
       //本地静态字段
        static int IDMax;

        //本地静态字段
        static string TableName;

        static FileDAO()
        {
            TableName = "file";
            IDMax = getIDMax(TableName);
        }

        public FileDAO()
        {
            //赋值父类动态字段
            databaseTableName = TableName;
        }

        public static int getID()
        {
            Interlocked.Increment(ref IDMax);
            return IDMax;
        }

        //在删除附件之前，修改议程中，大于指定附件序号，减1
        public int updateIndex(int agendaID, int fileIndex)
        {
            string commandText = "update " + TableName + " set fileIndex=fileIndex-1 where agendaID=@agendaID and fileIndex > @fileIndex;";
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter { name = "agendaID", value = agendaID });
            parameters.Add(new Parameter { name = "fileIndex", value = fileIndex });
            return DBFactory.GetInstance().ExecuteNonQuery(commandText, parameters);
        }
    }
}
