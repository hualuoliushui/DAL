using DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class Person_RoleDAO : DAOBase
    {
        public Person_RoleDAO()
        {
            databaseTableName = "person_role";
            IDMax = getIDMax();

        }
    }
}
