using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Abstrack
{
    public interface IUserService
    {
        User GetUserByLogin(User user);
        List<User> GetAll();
        User GetById(int id);
        void Create(User entity);
        void Delete(User entity);
        void Update(User entity);
        string GetToken(User entity);
    }
}
