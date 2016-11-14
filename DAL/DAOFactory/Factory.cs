using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using DAL.DB;
using System.Data;
using System.Reflection;

using DAL.DAO;

using DAL.Base;

namespace DAL.DAOFactory
{
    public class Factory
    {
        /// <summary>
        /// 使用单例
        /// 保证同一个表，同一时刻只能调用一次 getIDMax()
        /// </summary>
        /// 
        private static Dictionary<Type, object> DAOS;

        static Factory()
        {
            Mutex mutex = new Mutex(false, "FactoryMutex");

            mutex.WaitOne();

            if (DAOS == null)
            {
                DAOS = new Dictionary<Type, object>();
            }

            mutex.ReleaseMutex();
        }

        public static T getInstance<T>() where T : DAOBase, new()
        {
            Mutex mutex = new Mutex(false, "FactoryInstanceMutex");

            mutex.WaitOne();

            //如果没有对应的对象类型
            if (!DAOS.ContainsKey(typeof(T)))
            {
                DAOS.Add(typeof(T), new T());
            }

            mutex.ReleaseMutex();

            return (T)DAOS[typeof(T)];
        }
    }
}
