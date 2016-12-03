using DAL.DAO;
using DAL.DAOFactory;
using DAL.DAOVO;
using DAL.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class systemInit
    {
        static void Main(string[] args)
        {
            //此函数必须单独运行，同时停止其他持有数据库的程序的运行
            reset();
        }
        
        /// <summary>
        /// 此函数必须单独运行，同时停止其他持有数据库的程序的运行，
        /// 防止其他程序对数据库主键产生冲突。
        /// 初始化过程，表中主键ID为人为直接制定
        /// </summary>
        static void reset()
        {
            //==================

            #region 删除所有数据
            Queue<String> tableNames = new Queue<string>();
            tableNames.Enqueue("person_role");
            tableNames.Enqueue("role_permission");
            tableNames.Enqueue("role");
            tableNames.Enqueue("permission");
            tableNames.Enqueue("voteOptionPersonResult");
            tableNames.Enqueue("voteOption");
            tableNames.Enqueue("vote");
            tableNames.Enqueue("file");
            tableNames.Enqueue("agenda");
            tableNames.Enqueue("delegate");
            tableNames.Enqueue("meeting");
            tableNames.Enqueue("meetingPlace");
            tableNames.Enqueue("person");
            tableNames.Enqueue("device");
            tableNames.Enqueue("person");
            while (tableNames.Count != 0)
            {
                //删除数据库数据，不使用DAO
                string commandText = "delete from " + tableNames.Dequeue() + ";";
                DBFactory.GetInstance().ExecuteNonQuery(commandText, null);
            }
            #endregion
            /////////////////////////////////////////////////////////////////////////

            #region 初始化数据库表操作对象
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

            VoteOptionPersonResultDAO voteOptionPersonResultDao = Factory.getInstance<VoteOptionPersonResultDAO>();

            #endregion

            #region webServer 系统初始化数据

            #region 添加超级管理员用户
            Console.WriteLine("添加超级管理员用户");

            PersonVO admin = new PersonVO();
            int adminID = 1;//超级管理员用户ID固定为1

            admin.personID = adminID;
            admin.personName = "超级管理员";
            admin.personDepartment = "##";
            admin.personJob = "##";
            admin.personDescription = "***";
            admin.personPassword = "123456";
            admin.personState = 0;
            admin.isAdmin = true;

            Console.WriteLine(personDao.insert<PersonVO>(admin));
            #endregion

            #region 角色、权限

            #region 添加系统基本角色 1-超级管理员,2-会议组织者，3-成员
            Console.WriteLine("添加系统基本角色");
            //系统角色，超级管理员角色ID=1,会议组织者=2，成员=3
            int[] roleIDs = new int[3];
            roleIDs[0] = 1;
            roleIDs[1] = 2;
            roleIDs[2] = 3;

            List<RoleVO> roles = new List<RoleVO>();
            roles.Add(new RoleVO { roleID = roleIDs[0], roleName = "超级管理员", isIntegrant = true });
            roles.Add(new RoleVO { roleID = roleIDs[1], roleName = "会议组织者", isIntegrant = true });
            roles.Add(new RoleVO { roleID = roleIDs[2], roleName = "成员", isIntegrant = true });

            foreach (RoleVO vo in roles)
            {
                Console.WriteLine(roleDao.insert<RoleVO>(vo));
            }
            #endregion

            #region 添加权限（超级管理员角色)
            Console.WriteLine("添加权限（超级管理员角色)");

            int permissionNum_admin = 17;
            int[] permissionIDs_admin = new int[permissionNum_admin];
            for (int i = 0; i < permissionNum_admin; i++)
            {
                //自动获取ID
                permissionIDs_admin[i] = PermissionDAO.getID();
            }

            List<PermissionVO> permissions_admin = new List<PermissionVO>();
            int permissionIndex = 0;

            #region 超级管理员角色的权限列表
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

            

            foreach (PermissionVO vo in permissions_admin)
            {
                Console.WriteLine(permissionDao.insert<PermissionVO>(vo));
            }

            #endregion

            #region 建立超级管理员角色与权限关联
            Console.WriteLine("建立超级管理员角色与权限关联");

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
            #endregion

            #region 添加权限（组织者角色）
            Console.WriteLine("建立权限(组织者角色)");

            int permissionNum_org = 9;
            int[] permissionIDs_org = new int[permissionNum_org];
            for (int i = 0; i < permissionNum_org; i++)
            {
                permissionIDs_org[i] = PermissionDAO.getID();
            }

            List<PermissionVO> permissions_org = new List<PermissionVO>();
            int permissionIndex_org = 0;

            #region 组织者角色的权限列表
            //0
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_org[permissionIndex_org++],
                    permissionName = "进入组织者首页",
                    permissionDescription = "Account-Organizor"
                });
            //1
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_org[permissionIndex_org++],
                    permissionName = "获取会议列表",
                    permissionDescription = "Meeting-GetMeetings"
                });
            //2
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_org[permissionIndex_org++],
                    permissionName = "获取指定会议的基本信息",
                    permissionDescription = "Meeting-GetMeeting"
                });
            //3
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_org[permissionIndex_org++],
                    permissionName = "创建会议基本信息",
                    permissionDescription = "Meeting-CreateMeeting"
                });
            //4
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_org[permissionIndex_org++],
                    permissionName = "更新前获取会议信息",
                    permissionDescription = "Meeting-GetMeetingForUpdate"
                });
            //5
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_org[permissionIndex_org++],
                    permissionName = "更新会议信息",
                    permissionDescription = "Meeting-UpdateMeeting"
                });
            //6
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_org[permissionIndex_org++],
                    permissionName = "批量删除会议",
                    permissionDescription = "Meeting-DeleteMeetingMultipe"
                });
            //7
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_org[permissionIndex_org++],
                    permissionName = "为参会人员获取用户信息",
                    permissionDescription = "User-GetUsersForDelegate"
                });
            //8
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = permissionIDs_org[permissionIndex_org++],
                    permissionName = "为参会人员创建用户",
                    permissionDescription = "User-CreateForDelegate"
                });
            

            foreach (PermissionVO vo in permissions_org)
            {
                Console.WriteLine(permissionDao.insert<PermissionVO>(vo));
            }

            #endregion

            #region 组织者角色与权限关联
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

            foreach (Role_PermissionVO vo in role_permissions_org)
            {
                Console.WriteLine(role_permissionDao.insert<Role_PermissionVO>(vo));
            }
            #endregion
            #endregion

            #region 超级管理员用户与超级管理员角色关联
            Console.WriteLine("建立超级管理员用户与超级管理员角色关联");

            int person_roleID = Person_RoleDAO.getID();
            Person_RoleVO adminPR = new Person_RoleVO
            {
                person_roleID = person_roleID,
                personID = adminID,
                roleID = roleIDs[0]
            };

            Console.WriteLine(person_roleDao.insert<Person_RoleVO>(adminPR));
            #endregion
            #endregion

            #endregion


        }
    }
}
