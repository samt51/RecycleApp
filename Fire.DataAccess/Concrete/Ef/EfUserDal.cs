using Fire.DataAccess.Abstrack;
using Fire.DataAccess.DbConnection;
using Fire.Entity.Concrete;
using System.Linq;

namespace Fire.DataAccess.Concrete.Ef
{
    public class EfUserDal : EfRepositoryDal<User, DataContext>, IUserDal
    {
        public User GetUserByLogin(User user)
        {
            using (var db=new DataContext())
            {
                return db.users.Where(x => x.email == user.email && x.password == user.password).FirstOrDefault();
            }
        }
    }
}
