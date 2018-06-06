using Cl.AuthorityManagement.IRepository;
using System.Runtime.Remoting.Messaging;

namespace Cl.AuthorityManagement.RepositoryFactory
{
    public class DBSessionFactory
    {
        public static IDBSession CreateDBSession()
        {
            IDBSession dbSession = (IDBSession)CallContext.GetData("dbSession");
            if (dbSession == null)
            {
                dbSession = new DBSession();
                CallContext.SetData("dbSession", dbSession);
            }
            return dbSession;
        }
    }
}
