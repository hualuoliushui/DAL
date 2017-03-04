using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAOVO
{
    public class MeetingPlaceVO
    {
        public int meetingPlaceID { set; get; }

        public string meetingPlaceName { set; get; }

        public int meetingPlaceCapacity { set; get; }

        public int meetingPlaceState { set; get; }

        public int seatType { set; get; } //会场座位布置类型：
    }
}
