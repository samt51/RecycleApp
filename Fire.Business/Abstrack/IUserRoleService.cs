using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Abstrack
{
    public interface IUserRoleService
    {
        List<UserRoles> GetAll();
        UserRoles GetById(int id);
        void Create(UserRoles entity);
        void Delete(UserRoles entity);
        void Update(UserRoles entity);
    }
}
