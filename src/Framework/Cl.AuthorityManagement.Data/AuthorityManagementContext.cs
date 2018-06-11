using Cl.AuthorityManagement.Data.Migrations;
using Cl.AuthorityManagement.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Data
{
    public class AuthorityManagementContext : DbContext
    {
        public AuthorityManagementContext() : base("name=AuthorityManagementContext")
        {
            //修改实体时自动更新数据库不会删除数据
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<AuthorityManagementContext, Configuration>());
        }

        //权限管理
        public virtual IDbSet<Module> Module { get; set; }
        public virtual IDbSet<ModuleElement> ModuleElement { get; set; }
        public virtual IDbSet<Role> Role { get; set; }
        //public virtual DbSet<Permission> Permission { get; set; }
        public virtual IDbSet<UserInfo> UserInfo { get; set; }
        public virtual IDbSet<UserInfoModuleElement> UserInfoModuleElement { get; set; }
        public virtual IDbSet<RoleModuleElement> RoleModuleElement { get; set; }
        
        //日志记录
        public virtual DbSet<RequestInfo> RequestInfo { get; set; }
        public virtual DbSet<MonitorInfo> MonitorInfo { get; set; }
        public virtual DbSet<ErrorInfo> ErrorInfo { get; set; }
        public virtual DbSet<LoginInfo> LoginInfo { get; set; }
        public virtual DbSet<OperateInfo> OperateInfo { get; set; }

        //资源表
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<IPInfo> IPInfo { get; set; }

        public virtual DbSet<Push> Push { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //表名不用复数形式
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //移除一对多的级联删除约定，想要级联删除可以在 EntityTypeConfiguration<TEntity>的实现类中进行控制
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //多对多启用级联删除约定，不想级联删除可以在删除前判断关联的数据进行拦截
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
