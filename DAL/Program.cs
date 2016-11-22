using System.Collections.Generic;

using DAL.DAO;

using System;
using DAL.DAOFactory;
using DAL.DAOVO;




namespace DAL
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonDAO personDao = Factory.getInstance<PersonDAO>();
            RoleDAO roleDao = Factory.getInstance<RoleDAO>();
            Person_RoleDAO person_roleDao = Factory.getInstance<Person_RoleDAO>();
            PermissionDAO permissionDao = Factory.getInstance<PermissionDAO>();
            Role_PermissionDAO role_permissionDao = Factory.getInstance<Role_PermissionDAO>();

            DeviceDAO deviceDao = Factory.getInstance<DeviceDAO>();
            MeetingPlaceDAO meetingPlaceDao = Factory.getInstance<MeetingPlaceDAO>();

            MeetingDAO meetingDao = Factory.getInstance<MeetingDAO>();

            DelegateDAO delegateDao = Factory.getInstance<DelegateDAO>();

            AgendaDAO agendaDao = Factory.getInstance<AgendaDAO>();

            FileDAO fileDao = Factory.getInstance<FileDAO>();

            VoteDAO voteDao = Factory.getInstance<VoteDAO>();

             VoteOptionDAO voteOptionDao = Factory.getInstance<VoteOptionDAO>();
            
             
            VoteOptionPersonResultDAO voteOptionPersonResultDao;
            
            voteOptionPersonResultDao = Factory.getInstance<VoteOptionPersonResultDAO>();
           

            //==================

            //删除所有数据
            voteOptionPersonResultDao.deleteAll();
            voteOptionDao.deleteAll();
            voteDao.deleteAll();
            fileDao.deleteAll();
            agendaDao.deleteAll();
            delegateDao.deleteAll();
            meetingDao.deleteAll();
            meetingPlaceDao.deleteAll();
            deviceDao.deleteAll();
            role_permissionDao.deleteAll();
            permissionDao.deleteAll();
            person_roleDao.deleteAll();
            roleDao.deleteAll();
            personDao.deleteAll();




            #region //webServer 权限管理
            ///添加超级管理员
            Console.WriteLine("添加超级管理员用户");


            PersonVO admin = new PersonVO();
            int adminID = PersonDAO.getID();

            admin.personID = adminID;
            admin.personName = "admin";
            admin.personDepartment = "##";
            admin.personJob = "##";
            admin.personDescription = "***";
            admin.personPassword = "123456";
            admin.personState = 0;

            Console.WriteLine(personDao.insert<PersonVO>(admin));

            //
            Console.WriteLine("添加系统基本角色");

            int[] roleIDs = new int[3];
            roleIDs[0] = RoleDAO.getID();
            roleIDs[1] = RoleDAO.getID();
            roleIDs[2] = RoleDAO.getID();

            List<RoleVO> roles = new List<RoleVO>();
            roles.Add(new RoleVO { roleID = roleIDs[0], roleName = "Admin" });
            roles.Add(new RoleVO { roleID = roleIDs[1], roleName = "Organizor" });
            roles.Add(new RoleVO { roleID = roleIDs[2], roleName = "Member" });

            foreach (RoleVO vo in roles)
            {
                Console.WriteLine(roleDao.insert<RoleVO>(vo));
            }

            #region 管理员角色
            Console.WriteLine("添加权限（管理员角色)");

            int permissionNum_admin = 17;
            int[] permissionIDs_admin = new int[permissionNum_admin];
            for (int i = 0; i < permissionNum_admin; i++)
            {
                permissionIDs_admin[i] = PermissionDAO.getID();
            }

            List<PermissionVO> permissions_admin = new List<PermissionVO>();
            int permissionIndex = 0;

            #region 管理员角色、权限列表
            //0
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_admin[permissionIndex++],
                    permissionName = "进入管理员首页",
                    permissionDescription = "Account-Admin"
                });
            //1
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_admin[permissionIndex++],
                    permissionName = "查看会场",
                    permissionDescription = "MeetingPlace-GetMeetingPlaces"
                });
            //2
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_admin[permissionIndex++],
                    permissionName = "添加会场",
                    permissionDescription = "MeetingPlace-CreateMeetingPlace"
                });
            //3
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_admin[permissionIndex++],
                    permissionName = "更新前获取会场",
                    permissionDescription = "MeetingPlace-GetMeetingPlaceForUpdate"
                });
            //4
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_admin[permissionIndex++],
                    permissionName = "更新会场",
                    permissionDescription = "MeetingPlace-UpdateMeetingPlace"
                });
            //5
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_admin[permissionIndex++],
                    permissionName = "更新会场状态",
                    permissionDescription = "MeetingPlace-UpdateMeetingPlaceAvailable"
                });
            //6
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_admin[permissionIndex++],
                    permissionName = "查看设备",
                    permissionDescription = "Device-GetDevices"
                });
            //7
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_admin[permissionIndex++],
                    permissionName = "添加设备",
                    permissionDescription = "Device-CreateDevice"
                });
            //8
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_admin[permissionIndex++],
                    permissionName = "更新前获取设备",
                    permissionDescription = "Device-GetDeviceForUpdate"
                });
            //9
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_admin[permissionIndex++],
                    permissionName = "更新设备",
                    permissionDescription = "Device-UpdateDevice"
                });

            //10
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_admin[permissionIndex++],
                    permissionName = "更新设备状态",
                    permissionDescription = "Device-UpdateDeviceAvailable"
                });

            //11
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_admin[permissionIndex++],
                    permissionName = "查看用户",
                    permissionDescription = "User-GetUsers"
                });
            //12
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_admin[permissionIndex++],
                    permissionName = "添加用户",
                    permissionDescription = "User-CreateUser"
                });
            //13
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_admin[permissionIndex++],
                    permissionName = "更新前获取用户",
                    permissionDescription = "User-GetUserForUpdate"
                });
            //14
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_admin[permissionIndex++],
                    permissionName = "更新用户",
                    permissionDescription = "User-UpdateUser"
                });
            //15
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_admin[permissionIndex++],
                    permissionName = "更新用户状态",
                    permissionDescription = "User-UpdateUserAvailable"
                });
            //16
            permissions_admin.Add(
               new PermissionVO
               {
                   permissionID = permissionIDs_admin[permissionIndex++],
                   permissionName = "查看角色",
                   permissionDescription = "Role-GetRoles"
               });

            #endregion

            foreach (PermissionVO vo in permissions_admin)
            {
                Console.WriteLine(permissionDao.insert<PermissionVO>(vo));
            }

           
            //
            Console.WriteLine("建立角色与权限关联");

            //管理员角色、权限
            int[] role_permissionIDs_admin = new int[permissionNum_admin];
            List<Role_PermissionVO> role_permissions_admin = new System.Collections.Generic.List<Role_PermissionVO>();
            for (int i = 0; i < permissionNum_admin; i++)
            {
                role_permissionIDs_admin[i] = Role_PermissionDAO.getID();
                role_permissions_admin.Add(
                    new Role_PermissionVO
                    {
                        role_permissionID = role_permissionIDs_admin[i],
                        roleID = roleIDs[0],
                        permissionID = permissionIDs_admin[i]
                    });
            }

            foreach (Role_PermissionVO vo in role_permissions_admin)
            {
                Console.WriteLine(role_permissionDao.insert<Role_PermissionVO>(vo));
            }
#endregion

            #region 组织者角色
            Console.WriteLine("建立权限(组织者角色)");

            int permissionNum_org = 1;
            int[] permissionIDs_org = new int[permissionNum_org];
            for (int i = 0; i < permissionNum_org; i++)
            {
                permissionIDs_org[i] = PermissionDAO.getID();
            }

            List<PermissionVO> permissions_org = new List<PermissionVO>();
            int permissionIndex_org = 0;

            #region 组织者角色、权限列表
            //0
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_org[permissionIndex_org++],
                    permissionName = "进入组织者首页",
                    permissionDescription = "Account-Organizor"
                });

            #endregion

            foreach (PermissionVO vo in permissions_org)
            {
                Console.WriteLine(permissionDao.insert<PermissionVO>(vo));
            }


            //
            Console.WriteLine("建立组织者角色与权限关联");

            //组织者角色、权限
            int[] role_permissionIDs_org = new int[permissionNum_org];
            List<Role_PermissionVO> role_permissions_org = new System.Collections.Generic.List<Role_PermissionVO>();
            for (int i = 0; i < permissionNum_org; i++)
            {
                role_permissionIDs_org[i] = Role_PermissionDAO.getID();
                role_permissions_org.Add(
                    new Role_PermissionVO
                    {
                        role_permissionID = role_permissionIDs_org[i],
                        roleID = roleIDs[1],
                        permissionID = permissionIDs_org[i]
                    });
            }

            #endregion

            foreach (Role_PermissionVO vo in role_permissions_org)
            {
                Console.WriteLine(role_permissionDao.insert<Role_PermissionVO>(vo));
            }


            //
            Console.WriteLine("建立管理员用户与角色关联");

            int person_roleID = Person_RoleDAO.getID();
            Person_RoleVO adminPR = new Person_RoleVO
            {
                person_roleID = person_roleID,
                personID = adminID,
                roleID = roleIDs[0]
            };

            Console.WriteLine(person_roleDao.insert<Person_RoleVO>(adminPR));
            #endregion

            #region apiServer ，会议测试数据

            List<DeviceVO> devices = new List<DeviceVO>();
            int[] deviceIDs = new int[3];
            for (int i = 0; i < 3; i++)
            {
                deviceIDs[i] = DeviceDAO.getID();
            }

            //魔方429
            devices.Add(
                new DeviceVO
                {
                    deviceID = deviceIDs[0],
                    IMEI = "862823023300546",
                    deviceIndex = 1,
                    deviceState = 0
                });

            //魔方450
            devices.Add(
                new DeviceVO
                {
                    deviceID = deviceIDs[1],
                    IMEI = "862823023301916",
                    deviceIndex = 2,
                    deviceState = 0
                });
            //台电
            devices.Add(
                new DeviceVO
                {
                    deviceID = deviceIDs[2],
                    IMEI = "359365002515686",
                    deviceIndex = 3,
                    deviceState = 0
                });

            Console.WriteLine("添加设备");
            foreach (DeviceVO vo in devices)
            {
                Console.WriteLine(deviceDao.insert<DeviceVO>(vo));
            }



            List<MeetingPlaceVO> meetingPlaces = new List<MeetingPlaceVO>();
            int[] meetingPalceIDs = new int[2];
            for (int i = 0; i < 2; i++)
            {
                meetingPalceIDs[i] = MeetingPlaceDAO.getID();
            }
            meetingPlaces.Add(
                new MeetingPlaceVO
                {
                    meetingPlaceID = meetingPalceIDs[0],
                    meetingPlaceName = "人民大灰厂",
                    meetingPlaceCapacity = 200,
                    meetingPlaceState = 0
                });
            meetingPlaces.Add(
                new MeetingPlaceVO
                {
                    meetingPlaceID = meetingPalceIDs[1],
                    meetingPlaceName = "人民厂",
                    meetingPlaceCapacity = 300,
                    meetingPlaceState = 0
                });

            //////////////////////////////////
            Console.WriteLine("添加会场");
            foreach (MeetingPlaceVO vo in meetingPlaces)
            {
                Console.WriteLine(meetingPlaceDao.insert<MeetingPlaceVO>(vo));
            }


            int[] personIDs = new int[3];
            for (int i = 0; i < 3; i++)
            {
                personIDs[i] = PersonDAO.getID();
            }
            List<PersonVO> persons = new List<PersonVO>();
            persons.Add(
                new PersonVO
                {
                    personID = personIDs[0],
                    personName = "张三丰",
                    personDepartment = "华工" + personIDs[0],
                    personJob = "学生" + personIDs[0],
                    personDescription = "小学生",
                    personPassword = "123456",
                    personState = 0
                });
            persons.Add(
                new PersonVO
                {
                    personID = personIDs[1],
                    personName = "李四爷",
                    personDepartment = "华工" + personIDs[1],
                    personJob = "学生" + personIDs[1],
                    personDescription = "中学生",
                    personPassword = "123456",
                    personState = 0
                });
            persons.Add(
                new PersonVO
                {
                    personID = personIDs[2],
                    personName = "王五哥",
                    personDepartment = "华工" + personIDs[2],
                    personJob = "学生" + personIDs[2],
                    personDescription = "大学生",
                    personPassword = "123456",
                    personState = 0
                });

            //////////////////////////////////////
            Console.WriteLine("添加用户");
            foreach (PersonVO vo in persons)
            {
                Console.WriteLine(personDao.insert<PersonVO>(vo));
            }

            int[] person_roleIDs = new int[3];
            for (int i = 0; i < 3; i++)
            {
                person_roleIDs[i] = Person_RoleDAO.getID();
            }

            List<Person_RoleVO> person_roles = new List<Person_RoleVO>();
            person_roles.Add(new Person_RoleVO { person_roleID = person_roleIDs[0], roleID = roleIDs[2], personID = personIDs[0] });
            person_roles.Add(new Person_RoleVO { person_roleID = person_roleIDs[1], roleID = roleIDs[2], personID = personIDs[1] });
            person_roles.Add(new Person_RoleVO { person_roleID = person_roleIDs[2], roleID = roleIDs[2], personID = personIDs[2] });

            Console.WriteLine("添加用户角色关联");
            foreach (Person_RoleVO vo in person_roles)
            {
                Console.WriteLine(person_roleDao.insert<Person_RoleVO>(vo));
            }

            ///////////////////////////////////////
            Console.WriteLine("添加会议");

            int meetingID = MeetingDAO.getID();
            MeetingVO meeting = new MeetingVO();
            meeting.meetingID = meetingID;
            meeting.meetingPlaceID = meetingPalceIDs[0];
            meeting.meetingName = "人民代表大会"+meetingID;
            meeting.meetingSummary = "自由民主";
            meeting.meetingStatus = 1;
            meeting.meetingUpdateStatus = 0;
            meeting.meetingStartedTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            meeting.meetingToStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            meeting.meetingDuration = 0;
            meeting.personID = personIDs[0];

            Console.WriteLine(meetingDao.insert<MeetingVO>(meeting));

            ////////////////////////////////////////////
            Console.WriteLine("添加参会人员");

            List<DelegateVO> delegates = new List<DelegateVO>();
            int[] delegateIDs = new int[3];
            for (int i = 0; i < 3; i++)
            {
                delegateIDs[i] = DelegateDAO.getID();
            }
            delegates.Add(
                new DelegateVO
                {
                    delegateID = deviceIDs[0],
                    deviceID = deviceIDs[0],
                    personID = personIDs[0],
                    meetingID = meetingID,
                    personMeetingRole = 1,
                    isSignIn = false
                });
            delegates.Add(
                new DelegateVO
                {
                    delegateID = deviceIDs[1],
                    deviceID = deviceIDs[1],
                    personID = personIDs[1],
                    meetingID = meetingID,
                    personMeetingRole = 0,
                    isSignIn = false
                });
            delegates.Add(
                new DelegateVO
                {
                    delegateID = deviceIDs[2],
                    deviceID = deviceIDs[2],
                    personID = personIDs[2],
                    meetingID = meetingID,
                    personMeetingRole = 2,
                    isSignIn = false
                });
            foreach (DelegateVO vo in delegates)
            {
                Console.WriteLine(delegateDao.insert<DelegateVO>(vo));
            }

            //////////////////////////////////////////
            Console.WriteLine("添加议程");
            int[] agendaIDs = new int[2];
            agendaIDs[0] = AgendaDAO.getID();
            agendaIDs[1] = AgendaDAO.getID();
            //议程1
            AgendaVO agenda1 = new AgendaVO();
            agenda1.agendaID = agendaIDs[0];
            agenda1.agendaIndex = 1;
            agenda1.agendaName = "测试议程"+agendaIDs[0];
            agenda1.agendaDuration = 10;
            agenda1.meetingID = meetingID;
            agenda1.personID = personIDs[0];

            Console.WriteLine(agendaDao.insert<AgendaVO>(agenda1));
            //议程2
            agenda1.agendaID = agendaIDs[1];
            agenda1.agendaIndex = 2;
            agenda1.agendaName = "测试议程" + agendaIDs[1];
            agenda1.agendaDuration = 2;
            agenda1.meetingID = meetingID;
            agenda1.personID = personIDs[1];

            Console.WriteLine(agendaDao.insert<AgendaVO>(agenda1));

            //////////////////////////////////////////
            Console.WriteLine("添加附件");
            int[] fileIDs = new int[3];
            for (int i = 0; i < 3; i++)
            {
                fileIDs[i] = FileDAO.getID();
            }

            List<FileVO> files = new List<FileVO>();

            //议程1中附件1
            files.Add(
                new FileVO
                {
                    agendaID = agendaIDs[0],
                    fileID = fileIDs[0],
                    fileName = "CUDA.pdf",
                    fileIndex = 1,
                    fileSize = 12,
                    filePath = "CUDA.pdf"
                });
            files.Add(
                 new FileVO
                 {
                     agendaID = agendaIDs[1],
                     fileID = fileIDs[1],
                     fileName = "权限mvc.pdf",
                     fileIndex = 1,
                     fileSize = 13,
                     filePath = "权限mvc.pdf"
                 });
            files.Add(
                new FileVO
                {
                    agendaID = agendaIDs[1],
                    fileID = fileIDs[2],
                    fileName = "测试议程中的第二个附件.pdf",
                    fileIndex = 2,
                    fileSize = 14,
                    filePath = "测试议程中的第二个附件.pdf"
                });

            foreach (FileVO vo in files)
            {
                Console.WriteLine(fileDao.insert<FileVO>(vo));
            }

            //////////////////////////////////////////


            List<VoteVO> votes = new List<VoteVO>();
            List<VoteOptionVO> vote1Options = new List<VoteOptionVO>();
            List<VoteOptionVO> vote2Options = new List<VoteOptionVO>();

            int[] voteIDs = new int[2];
            for (int i = 0; i < 2; i++)
            {
                voteIDs[i] = VoteDAO.getID();
            }

            int[] vote1OptionIDs = new int[3];
            int[] vote2OptionIDs = new int[3];
            for (int i = 0; i < 3; i++)
            {
                vote1OptionIDs[i] = VoteOptionDAO.getID();
                vote2OptionIDs[i] = VoteOptionDAO.getID();
            }

            Console.WriteLine("添加表决");
            //表决
            votes.Add(
                new VoteVO
                {
                    agendaID = agendaIDs[0],
                    voteID = voteIDs[0],
                    voteName = "测试表决1",
                    voteDescription = "表决谁做助理",
                    voteIndex = 1,
                    voteStatus = 1,
                    voteType = 1 //单选
                });

            votes.Add(
                new VoteVO
                {
                    agendaID = agendaIDs[1],
                    voteID = voteIDs[1],
                    voteName = "测试表决2",
                    voteDescription = "表决谁做coding",
                    voteIndex = 1,
                    voteStatus = 1,
                    voteType = 2 //最多双选
                });

            for (int i = 0; i < 3; i++)
            {
                vote1Options.Add(
                   new VoteOptionVO
                   {
                       voteID = voteIDs[0],
                       voteOptionID = vote1OptionIDs[i],
                       voteOptionIndex = 0,
                       voteOptionName = "A" + vote1OptionIDs[i]
                   });
                vote2Options.Add(
                  new VoteOptionVO
                  {
                      voteID = voteIDs[1],
                      voteOptionID = vote2OptionIDs[i],
                      voteOptionIndex = 0,
                      voteOptionName = "B" + vote2OptionIDs[i]
                  });
            }

            foreach (VoteVO vo in votes)
            {
                Console.WriteLine(voteDao.insert<VoteVO>(vo));
            }

            foreach (VoteOptionVO vo in vote1Options)
            {
                Console.WriteLine(voteOptionDao.insert<VoteOptionVO>(vo));
            }

            foreach (VoteOptionVO vo in vote2Options)
            {
                Console.WriteLine(voteOptionDao.insert<VoteOptionVO>(vo));
            }

            #endregion

            #region//测试更新

            #endregion


            #region 重置数据
            Dictionary<string, object> list = new Dictionary<string, object>();
            list.Add("meetingStatus", 1);
            meetingDao.update(list, meetingID);

            list.Clear();
            list.Add("meetingID", meetingID);
            List<AgendaVO> agendas = agendaDao.getAll<AgendaVO>(list);
            foreach (AgendaVO agenda in agendas)
            {
                list.Clear();
                list.Add("agendaID", agenda.agendaID);
                List<VoteVO> voteVolist = voteDao.getAll<VoteVO>(list);

                foreach (VoteVO vote in voteVolist)
                {
                    list.Clear();
                    list.Add("voteStatus", 1);
                    voteDao.update(list, vote.voteID);

                    list.Clear();
                    list.Add("voteID", vote.voteID);
                    voteOptionPersonResultDao.delete(list);

                }
            }

            #endregion
        }
    }
}

