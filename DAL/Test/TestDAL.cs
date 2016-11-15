using System;

using DAL.DAOVO;
using DAL.DAO;
using System.Collections.Generic;



namespace DAL.Test
{
    class TestDAL
    {
        public static void run()
        {
            int check = 0;


            //插入数据
            Console.WriteLine("插入数据");

            int personNum = 10;
            List<int> personIDs = new List<int>();
            for (int i = 0; i < personNum; i++)
            {
                int id = PersonDAO.getID();
                personIDs.Add(id);
                check = TestOperator.insert<PersonDAO, PersonVO>(id);
                Console.WriteLine("TestOperator.insert<PersonDAO, PersonVO>(personID)：{0}", check);
            }

            int RoleNum = 1;
            List<int> roleIDs = new List<int>();
            for (int i = 0; i < RoleNum; i++)
            {
                int id = RoleDAO.getID();
                roleIDs.Add(id);
                check = TestOperator.insert<RoleDAO, RoleVO>(id);
                Console.WriteLine("TestOperator.insert<RoleDAO, RoleVO>(roleID)：{0}", check);
            }

            int permissionNum = 1;
            List<int> permissionIDs = new List<int>();
            for (int i = 0; i < permissionNum; i++)
            {
                int permissionID = PermissionDAO.getID();
                permissionIDs.Add(permissionID);
                check = TestOperator.insert<PermissionDAO, PermissionVO>(permissionID);
                Console.WriteLine("TestOperator.insert<PermissionDAO, PermissionVO>(permissionID)：{0}", check);
            }

            int meetingPlaceNum = 1;
            List<int> meetingPlaceIDs = new List<int>();
            for (int i = 0; i < meetingPlaceNum; i++)
            {
                int meetingPlaceID = MeetingPlaceDAO.getID();
                meetingPlaceIDs.Add(meetingPlaceID);
                check = TestOperator.insert<MeetingPlaceDAO, MeetingPlaceVO>(meetingPlaceID);
                Console.WriteLine("TestOperator.insert<MeetingPlaceDAO, MeetingPlaceVO>(meetingPlaceID)：{0}", check);
            }

            int deviceNum = 1;
            List<int> deviceIDs = new List<int>();
            for (int i = 0; i < deviceNum; i++)
            {
                int deviceID = DeviceDAO.getID();
                deviceIDs.Add(deviceID);
                check = TestOperator.insert<DeviceDAO, DeviceVO>(deviceID);
                Console.WriteLine("TestOperator.insert<DeviceDAO, DeviceVO>(deviceID)：{0}", check);
            }
            

            TestOperator.getAll<PersonDAO, PersonVO>();

            foreach (int id in permissionIDs)
            {
                Console.WriteLine("getone");
                TestOperator.getOne<PermissionDAO, PermissionVO>(id);
            }

            Dictionary<string, object> setlist = new Dictionary<string, object>();
            PersonDAO personDao = DAOFactory.Factory.getInstance<PersonDAO>();
            foreach (int id in personIDs)
            {
                Console.WriteLine("update person");
                setlist.Clear();
                setlist.Add("personName", "testupdate" + id);
                Console.WriteLine("testupdate{0}",personDao.update(setlist, id));
            }
            //start = DateTime.Now;
            //PersonDAO personDao = DAOFactory.Factory.getInstance<PersonDAO>();
            //end = DateTime.Now;
            //printDiffTime(start, end, "personDAO init");

            //start = DateTime.Now;
            //int personID = PersonDAO.getID();
            //end = DateTime.Now;
            //printDiffTime(start, end, "person getID " + personID);

            //start = DateTime.Now;
            //check = personDao.Insert<PersonVO>(
            //    new PersonVO
            //    {
            //        personID = personID,
            //        personName = string.Format("gss{0}", personID),
            //        personPassword = "123456",
            //        personDepartment = string.Format("华工{0}", personID),
            //        personJob = string.Format("学生{0}", personID),
            //        personDescription = string.Format("描述{0}", personID),
            //        personStatus = 0
            //    });
            //Console.WriteLine("Insert<PersonVO>()：{0}", check);
            //end = DateTime.Now;
            //printDiffTime(start, end, "person Insert");

            //start = DateTime.Now;
            //RoleDAO roleDao = DAOFactory.Factory.getInstance<RoleDAO>();
            //end = DateTime.Now;
            //printDiffTime(start, end, "roleDao init");

            //start = DateTime.Now;
            //int roleID = RoleDAO.getID();
            //end = DateTime.Now;
            //printDiffTime(start, end, "RoleDAO getID " + roleID);

            //start = DateTime.Now;
            //check = roleDao.Insert<RoleVO>(
            //    new RoleVO
            //    {
            //        roleID = roleID,
            //        roleName = string.Format("admin{0}", roleID)
            //    });
            ////Console.WriteLine("Insert<RoleVO>()：{0}", check);
            //end = DateTime.Now;
            //printDiffTime(start, end, "role nsert");

            // PermissionDAO permissionDao = DAOFactory.Factory.getInstance<PermissionDAO>();
            // int permissionID = permissionDao.getID();
            // check = permissionDao.Insert<PermissionVO>(
            //     new PermissionVO
            //     {
            //         permissionID = permissionID,
            //         permissionName = string.Format("权限{0}", permissionID),
            //         permissionDescription = string.Format("权限描述{0}", permissionID)
            //     });
            // Console.WriteLine("Insert<PermissionVO>()：{0}", check);

            // Person_RoleDAO person_roleDao = DAOFactory.Factory.getInstance<Person_RoleDAO>();

            // int person_roleID = person_roleDao.getID();
            // check = person_roleDao.Insert<Person_RoleVO>(
            //     new Person_RoleVO
            //     {
            //         person_roleID = person_roleID,
            //         personID = personID,
            //         roleID = roleID
            //     });
            // Console.WriteLine("Insert<Person_RoleVO>()：{0}", check);

            // Role_PermissionDAO role_permissionDao = DAOFactory.Factory.getInstance<Role_PermissionDAO>();
            // int role_permissionID = role_permissionDao.getID();
            // check=  role_permissionDao.Insert<Role_PermissionVO>(
            //     new Role_PermissionVO
            //     {
            //         role_permissionID = role_permissionID,
            //         roleID = roleID,
            //         permissionID = permissionID
            //     });
            // Console.WriteLine("Insert<Role_Permission>()：{0}", check);


            // end = DateTime.Now;
            // Console.WriteLine("spanTime: {0} ", (end - start).TotalMilliseconds);


            // //查询数据
            // Console.WriteLine("测试 getAll<T>()");

            // Console.WriteLine("测试 person");
            // List<PersonVO> personList = personDao.getAll<PersonVO>();
            //// printList<PersonVO>(personList);
            // printListCount<PersonVO>(personList);

            // Console.WriteLine("测试 Role");
            // List<RoleVO> roleList = roleDao.getAll<RoleVO>();
            // //printList<RoleVO>(roleList);
            // printListCount<RoleVO>(roleList);

            // Console.WriteLine("测试 Permission");
            // List<PermissionVO> permissionList = permissionDao.getAll<PermissionVO>();
            //// printList<PermissionVO>(permissionList);
            // printListCount<PermissionVO>(permissionList);

            // Console.WriteLine("测试 person_role");
            // List<Person_RoleVO> person_roleList = person_roleDao.getAll<Person_RoleVO>();
            // //printList<Person_RoleVO>(person_roleList);
            // printListCount<Person_RoleVO>(person_roleList);

            // Console.WriteLine("测试 role_permission");
            // List<Role_PermissionVO> role_permissionList = role_permissionDao.getAll<Role_PermissionVO>();
            // //printList<Role_PermissionVO>(role_permissionList);
            // printListCount<Role_PermissionVO>(role_permissionList);

            // Console.WriteLine("测试 getAll<T>(Dictionary<string, object> where)");
            // Dictionary<string, object> where = new Dictionary<string, object>();

            // where.Add("personName", "gss1");
            // personList = personDao.getAll<PersonVO>(where);
            // printList<PersonVO>(personList);
            // //printListCount<PersonVO>(personList);

            // Console.WriteLine("测试查询账号密码");
            // where.Clear();
            // where.Add("personName", "gss2");
            // where.Add("personPassword", "123456");
            // personList = personDao.getAll<PersonVO>(where);
            // printList<PersonVO>(personList);

            // where.Clear();
            // where.Add("personID", personID);
            // person_roleList = person_roleDao.getAll<Person_RoleVO>(where);
            // printList<Person_RoleVO>(person_roleList);



            // start = DateTime.Now;
            // //  DAOFactory.Factory.getUserDAOInstance().deleteAll();
            // //id = ++temp;
            // //for (int i = 0; i < num; i++)
            // //{

            // //    DAOFactory.Factory.getUserDAOInstance().delete(id + i);
            // //}
            // end = DateTime.Now;
            // Console.WriteLine("spanTime: {0} ", (end - start).TotalMilliseconds);
        }
    }
}
