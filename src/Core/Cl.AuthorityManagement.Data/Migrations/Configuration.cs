using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Data.Migrations
{
    internal class Configuration : DbMigrationsConfiguration<AuthorityManagementContext>
    {
        public Configuration()
        {
            //指示迁移数据库时是否可使用自动迁移的值
            AutomaticMigrationsEnabled = true;
            //指示迁移时是否接收数据值丢失
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Cl.AuthorityManagement.Data.AuthorityManagementContext";
        }

        protected override void Seed(AuthorityManagementContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
