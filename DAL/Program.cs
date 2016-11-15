using System.Collections.Generic;

using System.Threading;
using DAL.DAO;

using DAL.Test;
using System;
using DAL.DAOFactory;

namespace DAL
{
    class Program
    {
        static void Main(string[] args)
        {
            //DAOFactory.Factory.getInstance<PersonDAO>().deleteAll();
            //DAOFactory.Factory.getInstance<RoleDAO>().deleteAll();
            //DAOFactory.Factory.getInstance<PermissionDAO>().deleteAll();
            //DAOFactory.Factory.getInstance<MeetingPlaceDAO>().deleteAll();
            //DAOFactory.Factory.getInstance<DeviceDAO>().deleteAll();
            int threadNum = 1;

            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < threadNum; i++)
            {
                threads.Add(new Thread(TestDAL.run));
            }

            for (int i = 0; i < threadNum; i++)
            {
                threads[i].Start();
            }

            //for (int i = 0; i < threadNum; i++)
            //{
            //    threads[i].Join();
            //}
        }

    }
}
