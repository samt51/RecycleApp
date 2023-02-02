using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Abstrack
{
    public interface ICompanyService
    {
        List<Company> GetAll();
        Company GetById(int id);
        void Create(Company entity);
        void Update(Company entity);
    }
}
