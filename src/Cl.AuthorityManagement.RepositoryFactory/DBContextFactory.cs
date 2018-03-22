using Cl.AuthorityManagement.Data;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace Cl.AuthorityManagement.RepositoryFactory
{
    public class DBContextFactory
    {
        public static DbContext CreateDbContext()
        {
            DbContext dbContext = CallContext.GetData("dbContext") as DbContext;
            if (dbContext == null)
            {
                dbContext = new AuthorityManagementContext();
                CallContext.SetData("dbContext", dbContext);
            }
            return dbContext;
        }
    }
}
