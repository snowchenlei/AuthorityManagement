using Cl.AuthorityManagement.Data;
using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.Repository
{
    public partial class RoleUserInfoRepository : BaseRepository<RoleUserInfo>, IRoleUserInfoRepository
    {
        public RoleUserInfoRepository(AuthorityManagementContext context) : base(context)
        {
        }

        public List<RoleUserInfo> LoadRoleUserInfo(Expression<Func<RoleUserInfo, bool>> whereLamada)
        {
            return CurrentContext.RoleUserInfo
                .Include(i => i.Role)
                    .ThenInclude(i => i.RoleModules)
                        .ThenInclude(i => i.Module)
                .AsNoTracking()
                .Where(whereLamada)
                .ToList();
        }
    }
}
