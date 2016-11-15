using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Test
{
    class TestPrint
    {
        public static void printVO<T>(T t)
        {
            if (t == null)
            {
                return;
            }
            Console.WriteLine("{0}:", t.GetType());
            foreach (PropertyInfo p in t.GetType().GetProperties())
            {
                Console.Write("{0}-{1} ", p.Name, p.GetValue(t));
            }
            Console.WriteLine();
        }

        public static void printList<T>(List<T> list)
        {
            if (list != null)
            {
                foreach (T t in list)
                {
                    printVO<T>(t);
                }
            }
        }

        public static void printListCount<T>(List<T> list)
        {
             Console.WriteLine("{0} num :{1}", list.GetType(), list.Count);
        }

        public static void printDiffTime(DateTime start, DateTime end, string message)
        {
            TimeSpan ts = end - start;
            Console.WriteLine("process {0} :spent time : {1}--{2}", Thread.CurrentThread.ManagedThreadId, ts.TotalMilliseconds, message);
        }
    }
}
