using Cl.AuthorityManagement.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cl.AuthorityManagement.IRepository
{
    public partial interface IModuleUserInfoRepository:IBaseRepository<ModuleUserInfo>
    {
        List<Module> LoadModule(int userID);
    }
}
