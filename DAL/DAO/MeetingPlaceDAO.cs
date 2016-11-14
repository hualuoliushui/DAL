using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Base;
namespace DAL.DAO
{
    class MeetingPlaceDAO : DAOBase
    {
        public MeetingPlaceDAO()
        {
            databaseTableName = "meetingPlace";
            IDMax = getIDMax();
        }
    }
}
