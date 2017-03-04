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
            Log.DebugInfo("数据库初始化...");
            //此函数必须单独运行，同时停止其他持有数据库的程序的运行
            reset();
            Log.DebugInfo("数据库初始化结束");
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
            admin.personState = 1;
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

            List<PermissionVO> permissions_admin = new List<PermissionVO>();

            #region 超级管理员角色的权限列表
            //
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "进入管理员首页",
                    permissionDescription = "Account-Admin"
                });
            //
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "查看会场",
                    permissionDescription = "MeetingPlace-GetMeetingPlaces"
                });
            //
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "添加会场",
                    permissionDescription = "MeetingPlace-CreateMeetingPlace"
                });
            //
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "更新前获取会场",
                    permissionDescription = "MeetingPlace-GetMeetingPlaceForUpdate"
                });
            //
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "更新会场",
                    permissionDescription = "MeetingPlace-UpdateMeetingPlace"
                });
            //
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "更新会场状态",
                    permissionDescription = "MeetingPlace-UpdateMeetingPlaceAvailable"
                });
            //
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "查看设备",
                    permissionDescription = "Device-GetDevices"
                });
            //
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "添加设备",
                    permissionDescription = "Device-CreateDevice"
                });
            //
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "更新前获取设备",
                    permissionDescription = "Device-GetDeviceForUpdate"
                });
            //
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "更新设备",
                    permissionDescription = "Device-UpdateDevice"
                });

            //
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "更新设备状态",
                    permissionDescription = "Device-UpdateDeviceAvailable"
                });

            //
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "查看用户",
                    permissionDescription = "User-GetUsers"
                });
            //
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "添加用户",
                    permissionDescription = "User-CreateUser"
                });
            //
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "更新前获取用户",
                    permissionDescription = "User-GetUserForUpdate"
                });
            //
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "更新用户",
                    permissionDescription = "User-UpdateUser"
                });
            //
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "更新用户状态",
                    permissionDescription = "User-UpdateUserAvailable"
                });
            //
            permissions_admin.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "导入用户信息",
                    permissionDescription = "User-Upload"
                });
            //
            permissions_admin.Add(
               new PermissionVO
               {
                   permissionID = PermissionDAO.getID(),
                   permissionName = "查看权限",
                   permissionDescription = "Role-GetPermissions"
               });
            //
            permissions_admin.Add(
               new PermissionVO
               {
                   permissionID = PermissionDAO.getID(),
                   permissionName = "查看角色",
                   permissionDescription = "Role-GetRoles"
               });
            //
            permissions_admin.Add(
               new PermissionVO
               {
                   permissionID = PermissionDAO.getID(),
                   permissionName = "创建角色",
                   permissionDescription = "Role-CreateRole"
               });
           
            

            foreach (PermissionVO vo in permissions_admin)
            {
                Console.WriteLine(permissionDao.insert<PermissionVO>(vo));
            }

            #endregion

            #region 建立超级管理员角色与权限关联
            Console.WriteLine("建立超级管理员角色与权限关联");

            //管理员角色、权限
            List<Role_PermissionVO> role_permissions_admin = new System.Collections.Generic.List<Role_PermissionVO>();
            foreach (PermissionVO permissionVo in permissions_admin)
            {
                role_permissions_admin.Add(
                    new Role_PermissionVO
                    {
                        role_permissionID = Role_PermissionDAO.getID(),
                        roleID = roleIDs[0],
                        permissionID = permissionVo.permissionID
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

            List<PermissionVO> permissions_org = new List<PermissionVO>();

            #region 组织者角色的权限列表
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "进入组织者首页",
                    permissionDescription = "Account-Organizor"
                });
            #region 会议
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "展示会议",
                    permissionDescription = "Meeting-Show_organizor"
                });
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "添加会议",
                    permissionDescription = "Meeting-Add_organizor"
                });
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "编辑会议",
                    permissionDescription = "Meeting-Edit_organizor"
                });
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "删除会议",
                    permissionDescription = "Meeting-Delete_organizor"
                });
            #endregion
            ///=================================================
            #region 参会人员
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "参会人员首页",
                    permissionDescription = "Delegate-Index_organizor"
                });
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "编辑参会人员",
                    permissionDescription = "Delegate-Edit_organizor"
                });
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "添加参会人员",
                    permissionDescription = "Delegate-Add_organizor"
                });
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "删除参会人员",
                    permissionDescription = "Delegate-Delete_organizor"
                });
            #endregion
            //===========================================
            #region 议程
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "议程首页",
                    permissionDescription = "Agenda-Index_organizor"
                });
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "添加议程",
                    permissionDescription = "Agenda-Add_organizor"
                });
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "编辑议程",
                    permissionDescription = "Agenda-Edit_organizor"
                });
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "删除议程",
                    permissionDescription = "Agenda-Delete_organizor"
                });
            #endregion
            //===========================================
            #region 附件
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "附件首页",
                    permissionDescription = "Document-Index_organizor"
                });
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "上传附件",
                    permissionDescription = "Document-Add_organizor"
                });
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "转换附件",
                    permissionDescription = "Document-StartConvert"
                });
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "下载附件",
                    permissionDescription = "Document-Download"
                });
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "删除附件",
                    permissionDescription = "Document-Delete_organizor"
                });
            #endregion
            //================================================
            #region 表决
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "表决首页",
                    permissionDescription = "Vote-Index_organizor"
                });
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "添加表决",
                    permissionDescription = "Vote-Add_organizor"
                });
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "编辑表决",
                    permissionDescription = "Vote-Edit_organizor"
                });
            //
            permissions_org.Add(
                new PermissionVO
                {
                    permissionID = PermissionDAO.getID(),
                    permissionName = "删除表决",
                    permissionDescription = "Vote-Delete_organizor"
                });
            #endregion
            foreach (PermissionVO vo in permissions_org)
            {
                Console.WriteLine(permissionDao.insert<PermissionVO>(vo));
            }

            #endregion
            //==============================================
           
            #region 组织者角色与权限关联
            Console.WriteLine("建立组织者角色与权限关联");

            //组织者角色、权限
            List<Role_PermissionVO> role_permissions_org = new System.Collections.Generic.List<Role_PermissionVO>();
            foreach (PermissionVO permissionVo in permissions_org)
            {
                role_permissions_org.Add(
                    new Role_PermissionVO
                    {
                        role_permissionID = Role_PermissionDAO.getID(),
                        roleID = roleIDs[1],
                        permissionID = permissionVo.permissionID
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
