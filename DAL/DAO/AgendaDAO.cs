using DAL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    class AgendaDAO : DAOBase
    {
        public AgendaDAO()
        {
            databaseTableName = "agenda";
            IDMax = getIDMax();
        }
    }
}
