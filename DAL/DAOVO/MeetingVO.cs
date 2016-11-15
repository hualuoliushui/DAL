using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAOVO
{
    public class MeetingVO
    {
        public int meetingID { set; get; }
        public string meetingName { set; get; }
        public string meetingSummary { set; get; }
        public int meetingDuration { set; get; }
        public DateTime meetingToStartTime { set; get; }
        public DateTime meetingStartedTime { set; get; }
        public int meetingStatus { set; get; }
        public int meetingUpdateStatus { set; get; }
        public int meetingPlaceID { set; get; }
        public int personID { set; get; }
    }
}
