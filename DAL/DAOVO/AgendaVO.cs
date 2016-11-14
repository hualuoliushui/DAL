using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAOVO
{
    class AgendaVO
    {
        public int agendaID { set; get; }
        public string agendaName { set; get; }
        public int agendaIndex { set; get; }
        public int agendaDuration { set; get; }
        public int meetingID { set; get; }
        public int personID { set; get; }
    }
}
