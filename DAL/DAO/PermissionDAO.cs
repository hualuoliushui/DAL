using DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class PermissionDAO : DAOBase
    {
        public PermissionDAO()
        {
            databaseTableName = "permission";
            IDMax = getIDMax();
        }
    }
}
