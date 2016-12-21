using System.Collections.Generic;

using DAL.DAO;

using System;
using DAL.DAOFactory;
using DAL.DAOVO;
using DAL.DB;



namespace DAL
{
    public class TestData
    {
        /// <summary>
        /// 须其他程序调用，不能在本程序调用
        /// 添加测试数据
        /// </summary>
        public static void init()
        {
            Log.DebugInfo("测试数据初始化...");

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
           

           

            #region apiServer ，会议测试数据

            List<DeviceVO> devices = new List<DeviceVO>();
            int deviceNum = 4;
            int[] deviceIDs = new int[deviceNum];
            for (int i = 0; i < deviceNum; i++)
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
            //
            devices.Add(
                new DeviceVO
                {
                    deviceID = deviceIDs[3],
                    IMEI = "862823023300520",
                    deviceIndex = 4,
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


            int[] personIDs = new int[deviceNum];
            for (int i = 0; i < deviceNum; i++)
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
            persons.Add(
                new PersonVO
                {
                    personID = personIDs[3],
                    personName = "谢六",
                    personDepartment = "华工" + personIDs[3],
                    personJob = "学生" + personIDs[3],
                    personDescription = "全职",
                    personPassword = "123456",
                    personState = 0
                });

            //////////////////////////////////////
            Console.WriteLine("添加用户");
            foreach (PersonVO vo in persons)
            {
                Console.WriteLine(personDao.insert<PersonVO>(vo));
            }

            int[] person_roleIDs = new int[deviceNum];
            for (int i = 0; i < deviceNum; i++)
            {
                person_roleIDs[i] = Person_RoleDAO.getID();
            }

            List<Person_RoleVO> person_roles = new List<Person_RoleVO>();
            //默认为无权限角色："成员"角色，roleID=3
            for (int i = 0; i < deviceNum; i++)
            {
                person_roles.Add(new Person_RoleVO { person_roleID = person_roleIDs[i], roleID = 3, personID = personIDs[i] });
            }
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
            meeting.meetingStartedTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            meeting.meetingToStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            meeting.meetingDuration = 0;
            meeting.personID = personIDs[0];

            Console.WriteLine(meetingDao.insert<MeetingVO>(meeting));

            ////////////////////////////////////////////
            Console.WriteLine("添加参会人员");

            List<DelegateVO> delegates = new List<DelegateVO>();
            int[] delegateIDs = new int[deviceNum];
            for (int i = 0; i < deviceNum; i++)
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
            delegates.Add(
                new DelegateVO
                {
                    delegateID = deviceIDs[3],
                    deviceID = deviceIDs[3],
                    personID = personIDs[3],
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
                    fileName = "竞品dy.docx",
                    fileIndex = 1,
                    fileSize = 12,
                    filePath = @"\测试会议\测试议程1\竞品dy.docx"
                });
            files.Add(
                 new FileVO
                 {
                     agendaID = agendaIDs[1],
                     fileID = fileIDs[1],
                     fileName = "竞品dy.docx",
                     fileIndex = 1,
                     fileSize = 13,
                     filePath = @"\测试会议\测试议程2\竞品dy.docx"
                 });
            files.Add(
                new FileVO
                {
                    agendaID = agendaIDs[1],
                    fileID = fileIDs[2],
                    fileName = "test哈哈.docx",
                    fileIndex = 2,
                    fileSize = 14,
                    filePath = @"\测试会议\测试议程2\test哈哈.docx"
                });

            files.Add(
               new FileVO
               {
                   agendaID = agendaIDs[1],
                   fileID = FileDAO.getID(),
                   fileName = "干系人登记表.xlsx",
                   fileIndex = 2,
                   fileSize = 14,
                   filePath = @"\测试会议\测试议程2\干系人登记表.xlsx"
               });
            files.Add(
               new FileVO
               {
                   agendaID = agendaIDs[1],
                   fileID = FileDAO.getID(),
                   fileName = "p谷歌.pptx",
                   fileIndex = 3,
                   fileSize = 14,
                   filePath = @"\测试会议\测试议程2\p谷歌.pptx"
               });
            
            files.Add(
                new FileVO
                {
                    agendaID = agendaIDs[1],
                    fileID = FileDAO.getID(),
                    fileName = "test.xls",
                    fileIndex = 4,
                    fileSize = 14,
                    filePath = @"\1\2\test.xls"
                });

            files.Add(
                new FileVO
                {
                    agendaID = agendaIDs[1],
                    fileID = FileDAO.getID(),
                    fileName = "test.pptx",
                    fileIndex = 5,
                    fileSize = 14,
                    filePath = @"\测试会议\测试议程2\test.pptx"
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
            if (agendas != null)
            {
                foreach (AgendaVO agenda in agendas)
                {
                    list.Clear();
                    list.Add("agendaID", agenda.agendaID);
                    List<VoteVO> voteVolist = voteDao.getAll<VoteVO>(list);

                    if (voteVolist != null)
                    {
                        foreach (VoteVO vote in voteVolist)
                        {
                            //恢复表决状态
                            list.Clear();
                            list.Add("voteStatus", 1);
                            voteDao.update(list, vote.voteID);
                            //清空表决结果
                            list.Clear();
                            list.Add("voteID",vote.voteID);
                            voteOptionPersonResultDao.delete(list);

                        }
                    }
                }
            }
            #endregion

            Log.DebugInfo("测试数据初始化结束");
        }
    }
}

