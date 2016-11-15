using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using DAL.DB;
using System.Data;
using System.Reflection;

using DAL.DAO;

using DAL.Base;

namespace DAL.DAOFactory
{
    public class Factory
    {
        public static T getInstance<T>() where T : DAOBase, new()
        {
            return (T)(new T());
        }
    }
}
