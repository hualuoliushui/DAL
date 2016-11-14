
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;

using DAL.DAOVO;
using DAL.DAO;

namespace DAL
{
    class Program
    {

        public static void printVO<T>(T t)
        {
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
       
        static void Main(string[] args)
        {
            int threadNum = 1;
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < threadNum; i++)
            {
                threads.Add(new Thread(run));
            }

            for (int i = 0; i < threadNum; i++)
            {
                threads[i].Start();
            }

            for (int i = 0; i < threadNum; i++)
            {
                threads[i].Join();
            }
        }

        public static void run()
        {
            int check = 0;

            DateTime start = DateTime.Now;
            //插入数据
            Console.WriteLine("插入数据");
            PersonDAO personDao = new PersonDAO();
            int personID = personDao.getID();
            check = personDao.insert<PersonVO>(
                new PersonVO
                {
                    personID = personID,
                    personName = string.Format("gss{0}", personID),
                    personPassword = "123456",
                    personDepartment = string.Format("华工{0}", personID),
                    personJob = string.Format("学生{0}", personID),
                    personDescription = string.Format("描述{0}", personID),
                    personStatus = 0
                });
            Console.WriteLine("insert<PersonVO>()：{0}", check);

            RoleDAO roleDao = DAOFactory.Factory.getInstance<RoleDAO>();
            int roleID = roleDao.getID();
            check = roleDao.insert<RoleVO>(
                new RoleVO
                {
                    roleID = roleID,
                    roleName = string.Format("admin{0}", roleID)
                });
            Console.WriteLine("insert<RoleVO>()：{0}", check);

            PermissionDAO permissionDao = DAOFactory.Factory.getInstance<PermissionDAO>();
            int permissionID = permissionDao.getID();
            check = permissionDao.insert<PermissionVO>(
                new PermissionVO
                {
                    permissionID = permissionID,
                    permissionName = string.Format("权限{0}", permissionID),
                    permissionDescription = string.Format("权限描述{0}", permissionID)
                });
            Console.WriteLine("insert<PermissionVO>()：{0}", check);

            Person_RoleDAO person_roleDao = DAOFactory.Factory.getInstance<Person_RoleDAO>();

            int person_roleID = person_roleDao.getID();
            check = person_roleDao.insert<Person_RoleVO>(
                new Person_RoleVO
                {
                    person_roleID = person_roleID,
                    personID = personID,
                    roleID = roleID
                });
            Console.WriteLine("insert<Person_RoleVO>()：{0}", check);

            Role_PermissionDAO role_permissionDao = DAOFactory.Factory.getInstance<Role_PermissionDAO>();
            int role_permissionID = role_permissionDao.getID();
            check=  role_permissionDao.insert<Role_PermissionVO>(
                new Role_PermissionVO
                {
                    role_permissionID = role_permissionID,
                    roleID = roleID,
                    permissionID = permissionID
                });
            Console.WriteLine("insert<Role_Permission>()：{0}", check);


            DateTime end = DateTime.Now;
            Console.WriteLine("spanTime: {0} ", (end - start).TotalMilliseconds);


            //查询数据
            Console.WriteLine("测试 getAll<T>()");

            Console.WriteLine("测试 person");
            List<PersonVO> personList = personDao.getAll<PersonVO>();
           // printList<PersonVO>(personList);
            printListCount<PersonVO>(personList);

            Console.WriteLine("测试 Role");
            List<RoleVO> roleList = roleDao.getAll<RoleVO>();
            printList<RoleVO>(roleList);

            Console.WriteLine("测试 Permission");
            List<PermissionVO> permissionList = permissionDao.getAll<PermissionVO>();
           // printList<PermissionVO>(permissionList);
            printListCount<PermissionVO>(permissionList);

            Console.WriteLine("测试 person_role");
            List<Person_RoleVO> person_roleList = person_roleDao.getAll<Person_RoleVO>();
            //printList<Person_RoleVO>(person_roleList);
            printListCount<Person_RoleVO>(person_roleList);

            Console.WriteLine("测试 role_permission");
            List<Role_PermissionVO> role_permissionList = role_permissionDao.getAll<Role_PermissionVO>();
            //printList<Role_PermissionVO>(role_permissionList);
            printListCount<Role_PermissionVO>(role_permissionList);

            Console.WriteLine("测试 getAll<T>(Dictionary<string, object> where)");
            Dictionary<string, object> where = new Dictionary<string, object>();

            where.Add("personName", "gss1");
            personList = personDao.getAll<PersonVO>(where);
            printList<PersonVO>(personList);
            //printListCount<PersonVO>(personList);

            Console.WriteLine("测试查询账号密码");
            where.Clear();
            where.Add("personName", "gss2");
            where.Add("personPassword", "123456");
            personList = personDao.getAll<PersonVO>(where);
            printList<PersonVO>(personList);

            where.Clear();
            where.Add("personID", personID);
            person_roleList = person_roleDao.getAll<Person_RoleVO>(where);
            printList<Person_RoleVO>(person_roleList);



            start = DateTime.Now;
            //  DAOFactory.Factory.getUserDAOInstance().deleteAll();
            //id = ++temp;
            //for (int i = 0; i < num; i++)
            //{

            //    DAOFactory.Factory.getUserDAOInstance().delete(id + i);
            //}
            end = DateTime.Now;
            Console.WriteLine("spanTime: {0} ", (end - start).TotalMilliseconds);
        }
    }
}
