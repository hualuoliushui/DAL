using DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    class DeviceDAO : DAOBase
    {
        public DeviceDAO()
        {
            databaseTableName = "device";
            IDMax = getIDMax();
        }
    }
}
