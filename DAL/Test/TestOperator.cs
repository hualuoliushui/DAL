using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Test;
using System.Reflection;

namespace DAL.Test
{
    class TestOperator
    {
        //只能模拟无外键的情形
        public static int insert<T, K>(int id)
            where T : DAL.Base.DAOBase, new()
            where K : new()
        {
            K k = new K();
            foreach (PropertyInfo pk in k.GetType().GetProperties())
            {
                string typeName = pk.PropertyType.Name;
                if (string.Compare(typeName, "String") == 0)
                {
                    pk.SetValue(k, pk.Name + id + "");
                }
                else if (string.Compare(typeName, "Int32") == 0)
                {
                    pk.SetValue(k, id);
                }
                else if (string.Compare(typeName, "DateTime") == 0)
                {
                    pk.SetValue(k, DateTime.Now);
                }
                else if (string.Compare(typeName, "Boolean") == 0)
                {
                    pk.SetValue(k, false);
                }
            }

            T t = DAOFactory.Factory.getInstance<T>();
            return t.insert<K>(k);
        }

        public static void getAll<T, K>()
            where T : DAL.Base.DAOBase, new()
            where K : new()
        {
            T t = DAOFactory.Factory.getInstance<T>();
            List<K> ks = t.getAll<K>();
            TestPrint.printList<K>(ks);
        }

        public static void getOne<T, K>(int id)
            where T : DAL.Base.DAOBase, new()
            where K : new()
        {
            T t = DAOFactory.Factory.getInstance<T>();
            K k = t.getOne<K>(id);
            TestPrint.printVO<K>(k);
        }

        public static void test<T, K>()
            where T : DAL.Base.DAOBase, new()
            where K : new()
        {

        }
    }
}
