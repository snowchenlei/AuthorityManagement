using Cl.AuthorityManagement.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cl.AuthorityManagement.Data
{
    public class AuthorityManagementContext : DbContext
    {
        public AuthorityManagementContext(DbContextOptions<AuthorityManagementContext> options) : base(options)
        {
        }

        //权限管理
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<ModuleElement> ModuleElement { get; set; }
        //public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        public virtual DbSet<UserInfoModuleElement> UserInfoModuleElement { get; set; }
        public virtual DbSet<RoleModuleElement> RoleModuleElement { get; set; }

        //多对对中间表--以后支持则删除
        public virtual DbSet<RoleUserInfo> RoleUserInfo { get; set; }
        public virtual DbSet<RoleModule> RoleModule { get; set; }
        public virtual DbSet<ModuleUserInfo> ModuleUserInfo { get; set; }
        public virtual DbSet<ModuleElementModule> ModuleElementModule { get; set; }
        
        //日志记录
        public virtual DbSet<RequestInfo> RequestInfo { get; set; }
        public virtual DbSet<MonitorInfo> MonitorInfo { get; set; }
        public virtual DbSet<ErrorInfo> ErrorInfo { get; set; }
        public virtual DbSet<LoginInfo> LoginInfo { get; set; }
        public virtual DbSet<OperateInfo> OperateInfo { get; set; }

        //资源表
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<IPInfo> IPInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ////表名不用复数形式
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            ////移除一对多的级联删除约定，想要级联删除可以在 EntityTypeConfiguration<TEntity>的实现类中进行控制
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            ////多对多启用级联删除约定，不想级联删除可以在删除前判断关联的数据进行拦截
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //配置联合主键
            modelBuilder.Entity<UserInfoModuleElement>()
              .HasKey(r => new { r.UserInfoID, r.ModuleID, r.ModuleElementID });
            modelBuilder.Entity<RoleModuleElement>()
              .HasKey(r => new { r.RoleID, r.ModuleID, r.ModuleElementID });

            modelBuilder.Entity<RoleUserInfo>()
                .HasKey(r => new { r.RoleID, r.UserInfoID });
            modelBuilder.Entity<RoleModule>()
                .HasKey(r => new { r.RoleID, r.MouleID });
            modelBuilder.Entity<ModuleUserInfo>()
                .HasKey(r => new { r.ModuleID, r.UserInfoID });
            modelBuilder.Entity<ModuleElementModule>()
               .HasKey(r => new { r.ModuleID, r.ModuleElementID });

            base.OnModelCreating(modelBuilder);
        }
    }
}
