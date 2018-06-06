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
    public partial class UserInfoRepository: BaseRepository<UserInfo>, IUserInfoRepository
    {
        public Task<UserInfo> LoadUserInfoAsync(Expression<Func<UserInfo, bool>> whereLamada)
        {
            return CurrentContext.UserInfo                
                .Include(i => i.RoleUserInfos)
                    .ThenInclude(i => i.Role)
                .Include(i => i.UserInfoModuleElements)
                    .ThenInclude(i => i.ModuleElement)
                .Include(i => i.UserInfoModuleElements)
                    .ThenInclude(i => i.Module)
                .AsNoTracking()
                .SingleOrDefaultAsync(whereLamada);
        }
    }
}
