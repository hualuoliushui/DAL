using DAL.Base;

namespace DAL.DAOFactory
{
    public class Factory
    {
        public static T getInstance<T>() where T : DAOBase, new()
        {
           return new T();
        }
    }
}
