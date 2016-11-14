using DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class Role_PermissionDAO : DAOBase
    {
        public Role_PermissionDAO()
        {
            databaseTableName = "role_permission";
            IDMax = getIDMax();

        }
    }
}
