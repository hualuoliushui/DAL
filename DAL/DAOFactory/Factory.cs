using DAL.Base;

namespace DAL.DAOFactory
{
    public class Factory
    {
        public static T getInstance<T>() where T : DAOBase, new()
        {
            try
            {
                Log.DebugInfo("访问数据库" + typeof(T).ToString());
                return new T();
            }
            catch (System.Exception e)
            {
                Log.LogInfo("数据库访问异常", e);
                throw e.InnerException;
            } 
        }
    }
}
