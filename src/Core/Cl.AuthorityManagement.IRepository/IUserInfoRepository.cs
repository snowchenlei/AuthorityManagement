using Cl.AuthorityManagement.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cl.AuthorityManagement.IRepository
{
    public partial interface IUserInfoRepository : IBaseRepository<UserInfo>
    {
        Task<UserInfo> LoadUserInfoAsync(Expression<Func<UserInfo, bool>> whereLamada);
    }
}
