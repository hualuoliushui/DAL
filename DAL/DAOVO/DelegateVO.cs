using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAOVO
{
    public class DelegateVO
    {
        public int delegateID { set; get; }
        public bool isSignIn { set; get; }
        public int personMeetingRole { set; get; }
        public int deviceID { set; get; }
        public int meetingID { set; get; }
        public int personID { set; get; }
        public bool isUpdate { set; get; }
    }
}
