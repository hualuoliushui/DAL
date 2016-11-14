using System.Collections.Generic;
using System.Data;
using System;

using DAL.DAOVO;
using DAL.DB;
using DAL.Base;

using System.Reflection;

namespace DAL.DAO
{
    public class PersonDAO : DAOBase
    {
        /// <summary>
        /// 构造函数中，将表名赋值为person(因为user为access数据库中的关键字，使用时需加中括号[]，不符合mysql数据库的要求，故将其换成person
        /// </summary>
        public PersonDAO()
        {
            databaseTableName = "person";
            IDMax = getIDMax();

        }
    }
}
