using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAOVO
{
    public class FileVO
    {
        public int fileID { set; get; }
        public string fileName { set; get; }
        public int fileIndex { set; get; }
        public int fileSize { set; get; }
        public string filePath { set; get; }
        public int agendaID { set; get; }
        public bool isUpdate { set; get; }
    }
}
