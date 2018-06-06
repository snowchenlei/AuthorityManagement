using Cl.AuthorityManagement.Entity;
using Cl.AuthorityManagement.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cl.AuthorityManagement.Repository
{
    public partial class ModuleUserInfoRepository:BaseRepository<ModuleUserInfo>, IModuleUserInfoRepository
    {
        public List<Module> LoadModule(int userID)
        {
            return CurrentContext
                .ModuleUserInfo
                .AsNoTracking()
                .Where(m => m.UserInfoID == userID)
                .Select(m => m.Module)
                .ToList();
        }
    }
}
