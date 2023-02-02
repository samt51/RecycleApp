using Fire.Entity.Concrete;

namespace Fire.DataAccess.Abstrack
{
    public interface IUserDal:IRepository<User>
    {
        User GetUserByLogin(User user);
    }
}
