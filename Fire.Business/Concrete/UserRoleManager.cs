using Fire.Business.Abstrack;
using Fire.DataAccess.Abstrack;
using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Concrete
{
    public class UserRoleManager : IUserRoleService
    {
        private readonly IUserRoleDal _userRoleDal;
        public UserRoleManager(IUserRoleDal userRoleDal)
        {
            _userRoleDal = userRoleDal;
        }
        public void Create(UserRoles entity)
        {
            _userRoleDal.Create(entity);
        }

        public void Delete(UserRoles entity)
        {
            _userRoleDal.Delete(entity);
        }

        public List<UserRoles> GetAll()
        {
            return _userRoleDal.GetAll();
        }

        public UserRoles GetById(int id)
        {
            return _userRoleDal.GetById(id);
        }

        public void Update(UserRoles entity)
        {
            _userRoleDal.Update(entity);
        }
    }
}
