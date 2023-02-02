using Fire.DataAccess.Abstrack;
using Fire.DataAccess.DbConnection;
using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire.DataAccess.Concrete.Ef
{
    public class EfFactoryDal: EfRepositoryDal<Factory, DataContext>, IFactoryDal
    {
    }
}
