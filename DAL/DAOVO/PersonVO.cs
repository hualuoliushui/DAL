using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAOVO
{
    public class PersonVO
    {
        public int personID { set; get; }

        public string personName { set; get; }

        public string personPassword { set; get; }

        public string personDepartment { set; get; }

        public string personJob { set; get; }

        public string personDescription { set; get; }

        public int personState { set; get; }

        public bool isAdmin { set; get; }

        public int personLevel { set; get; }
    }
}
