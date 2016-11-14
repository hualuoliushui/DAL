using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAOVO
{
    class VoteVO
    {
        public int voteID { set; get; }
        public string voteName { set; get; }
        public int voteIndex { set; get; }
        public string voteDescription { set; get; }
        public int voteType { set; get; }
        public int voteStatus { set; get; }
        public int agendaID { set; get; }
    }
}
