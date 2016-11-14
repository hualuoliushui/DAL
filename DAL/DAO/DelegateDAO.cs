using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Base;
namespace DAL.DAO
{
    class DelegateDAO : DAOBase
    {
        public DelegateDAO()
        {
            databaseTableName = "delegate";
            IDMax = getIDMax();
        }
    }
}
