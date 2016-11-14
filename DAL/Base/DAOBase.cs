﻿/*
 *请阅读 书写规则：
 *表名与字段前缀一致，譬如：表名：person，字段：personID、personName、personPassword等，联合表：表名为两表表名以”_“分离，譬如：person_role.
 *VO值对象的属性及其顺序 与 表中的字段名及其顺序 一致；
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.DB;
using System.Data;
using System.Reflection;

namespace DAL.Base
{
    /// <summary>
    /// 用于传递参数的名称及值 到DB中
    /// name:sql语句中紧跟着@的字符串
    /// value：name相对应的值
    /// 参数列表的顺序必须按照sql语句中，@name出现的顺序添加。
    /// </summary>
    public class Parameter
    {
        public string name { set; get; }
        public Object value { set; get; }
    }

    /// <summary>
    /// 抽象类
    /// </summary>
    public  abstract class DAOBase
    {
        /// <summary>
        /// databaseTableName：数据库中的相对应的表名。应注意：access数据库中的关键字，在此不能作为表名或表中字段。
        /// 测试access关键字：sql语句中，表名或字段不使用中括号[]，如果成功，则表明其中没有access数据库的关键字。
        /// </summary>
        protected string databaseTableName;

        /// <summary>
        /// 应在子类的构造函数中赋值
        /// </summary>
        public int IDMax;

        /// <summary>
        /// 返回比当前IDmax大1的ID值
        /// </summary>
        /// <returns></returns>
        public int getID()
        {
            //原子加
            System.Threading.Interlocked.Increment(ref IDMax);
            return IDMax;
        }

        /// <summary>
        /// 返回当前数据库中以databaseTableName+ID命名的主键的最大值。用于插入下一条数据。
        /// </summary>
        /// <returns></returns>
        protected int getIDMax()
        {
            Object lockObject = new object();
            lock (lockObject)
            {
                string commandText = "select max(" + databaseTableName + "ID) from " + databaseTableName + " ;";
                DataTable dt = DBFactory.GetInstance().ExecuteQuery(commandText, null);
                string id = dt.Rows[0][0].ToString();
                if (string.IsNullOrWhiteSpace(id))
                {
                    return 0;
                }
                return Int32.Parse(id);
            }
        }

        //=========================================================

        /// <summary>
        /// 辅助函数
        /// 为T值对象赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="row"></param>
        private void setT<T>(ref T t,DataRow row){
            int index = 0;
            foreach (PropertyInfo p in t.GetType().GetProperties())
            {
                string typeName = p.PropertyType.Name;
                if (string.Compare(typeName, "String") == 0)
                {
                    p.SetValue(t, row[index].ToString());
                }
                else if (string.Compare(typeName, "Int32") == 0)
                {
                    p.SetValue(t, Int32.Parse(row[index].ToString()));
                }
                else if (string.Compare(typeName, "DateTime") == 0)
                {
                    p.SetValue(t, DateTime.Parse(row[index].ToString()));
                }

                index++;
            }
        }

        //========================================================

        /// <summary>
        /// 获取表中所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> getAll<T>() where T : new()
        {
            List<T> list = new List<T>();

            string commandText = "select * from " + databaseTableName + " order by " + databaseTableName + "ID asc;";

            DataTable dt = DBFactory.GetInstance().ExecuteQuery(commandText, null);
            
            //检查表中是否有数据，
            //第一行，第一列为主键，不可能为空
            if (dt == null && string.Compare(dt.Rows[0][0].ToString(), "") == 0)
            {
                return null;
            }

            foreach (DataRow row in dt.Rows)
            {
                T t = new T();
                this.setT<T>(ref t, row);
                list.Add(t);
            }
            //成功返回
            return list;
        }

        /// <summary>
        /// 使用外键,获取多行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> getAll<T>(Dictionary<string, object> where) where T : new()
        {
            if (where == null || where.Count == 0)
            {
                return null;
            }

            List<T> list = new List<T>();

            string commandText = "select * from " + databaseTableName + " where ";
            List<Parameter> parameters = new List<Parameter>();

            //构造参数化sql语句，parameter.key与表中字段一致，
            bool first = true;
            foreach (var parameter in where)
            {
                if (!first)
                {
                    commandText += " and " + parameter.Key + "=@" + parameter.Key;
                }
                else
                {
                    commandText +=  parameter.Key + "=@" + parameter.Key;
                    first = false;
                }

                parameters.Add(
                    new Parameter
                    {
                        name = parameter.Key,
                        value = parameter.Value
                    });
            }

            commandText += " ;";

            DataTable dt = DBFactory.GetInstance().ExecuteQuery(commandText, parameters);

            //检查表中是否有数据，
            //第一行，第一列为主键，不可能为空
            if (dt == null && string.Compare(dt.Rows[0][0].ToString(), "") == 0)
            {
                return null;
            }


            foreach (DataRow row in dt.Rows)
            {
                T t = new T();
                this.setT<T>(ref t, row);
                list.Add(t);
            }
            //成功返回
            return list;
        }

        /// <summary>
        /// 使用主键获取表中一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T getOne<T>(int id) where T : new()
        {
            T t = new T();

            string commandText = "select * from " + databaseTableName + " where " + databaseTableName + "ID=@id;";
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter { name = "id", value = id });
            DataTable dt = DBFactory.GetInstance().ExecuteQuery(commandText, parameters);

            //检查表中是否有数据，
            //第一行，第一列为主键，不可能为空
            if (dt == null && string.Compare(dt.Rows[0][0].ToString(), "") == 0)
            {
                return default(T);
            }

            this.setT<T>(ref t, dt.Rows[0]);

            return t;    
        }


        /// <summary>
        /// 数据库中的插入操作
        /// 这里使用T的反射来赋值sql语句中的参数，
        /// 所以：
        ///     sql语句中的@name，name必须与T对象中的属性同名；
        ///     sql语句中的参数顺序 必须 与 T对象中声明的属性的顺序 一致；
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int insert<T>(T t)
        {
            if (t == null)
            {
                return -1;
            }

            //注意，紧跟着@之后的字符串，要与vo中的属性名一致，sql语句与vo中属性的声明顺序一致
            string commandText = " insert into " + databaseTableName + " values  (";
            List<Parameter> parameters = new List<Parameter>();

            bool first = true;
            foreach (PropertyInfo p in t.GetType().GetProperties())
            {
                if (!first){
                    commandText += ",@" + p.Name;
                }
                else
                {
                    commandText += "@" + p.Name;
                    first = false;
                } 

                parameters.Add(new Parameter { name = p.Name, value = p.GetValue(t) });
            }
            commandText += ");";

            return DBFactory.GetInstance().ExecuteNonQuery(commandText, parameters);
        }

        /// <summary>
        ///  删除 （具有唯一主键）表中的一条数据
        ///  数据库重新设计才能使用，设计成int类型的唯一主键,databaseTableName+ID为主键字段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int delete(int id)
        {
            string commandText = "delete from " + databaseTableName + " where " + databaseTableName + "ID=@id;";
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter { name = "id", value = id });
            return (int)DBFactory.GetInstance().ExecuteNonQuery(commandText, parameters);
        }

        /// <summary>
        /// 删除一条或多条数据
        /// 根据外键，删除数据
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int delete(Dictionary<string, object> where)
        {
            if (where == null || where.Count==0)
            {
                return -1;
            }

            string commandText = "delete from " + databaseTableName + " where ";
            List<Parameter> parameters = new List<Parameter>();

            //构造参数化sql语句，parameter.key与表中字段一致，
            bool first = true;
            foreach (var parameter in where)
            {
                if (!first)
                {
                    commandText += " and " + parameter.Key + "=@" + parameter.Key;
                }
                else
                {
                    commandText += parameter.Key + "=@" + parameter.Key;
                    first = false;
                }

                parameters.Add(
                    new Parameter
                    {
                        name = parameter.Key,
                        value = parameter.Value
                    });
            }

            commandText += " ;";

            return DBFactory.GetInstance().ExecuteNonQuery(commandText, parameters);
        }

        /// <summary>
        /// 删除某个表中的所有数据，，测试时使用
        /// </summary>
        /// <returns></returns>
        public int deleteAll()
        {
            string commandText = "delete from " + databaseTableName + ";";
            return DBFactory.GetInstance().ExecuteNonQuery(commandText, null);
        }
    }
}
