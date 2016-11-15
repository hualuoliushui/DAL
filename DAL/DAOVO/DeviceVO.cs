using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAOVO
{
    public class DeviceVO
    {
        public int deviceID { set; get; }
        public string IMEI { set; get; }
        public int deviceIndex { set; get; }
        //0:未冻结，1:已冻结
        public int deviceState { set; get; }
    }
}
