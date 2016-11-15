using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAOVO
{
    public class VoteOptionVO
    {
        public int voteOptionID { set; get; }
        public string voteOptionName { set; get; }
        public int voteOptionIndex { set; get; }
        public int voteID { set; get; }
    }
}
