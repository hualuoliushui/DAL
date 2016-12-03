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
        //会议状态，1-未开启，2-正在开启，16-已结束
        public int meetingStatus { set; get; }
        //会议的更新状态，默认为0-无更新
        public int delegateUpdateStatus { set; get; }
        public int agendaUpdateStatus { set; get; }
        public int fileUpdateStatus { set; get; }
        public int voteUpdateStatus { set; get; }

        public int meetingPlaceID { set; get; }
        public int personID { set; get; }
    }
}
