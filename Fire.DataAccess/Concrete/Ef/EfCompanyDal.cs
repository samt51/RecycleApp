﻿using Fire.DataAccess.Abstrack;
using Fire.DataAccess.DbConnection;
using Fire.Entity.Concrete;

namespace Fire.DataAccess.Concrete.Ef
{
    public class EfCompanyDal : EfRepositoryDal<Company, DataContext>, ICompanyDal
    {
    }
}
