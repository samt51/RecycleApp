using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Abstrack
{
    public interface IBankService
    {
        List<bank> GetAll();
        bank GetById(int id);
        void Create(bank entity);
        void Delete(bank entity);
        void Update(bank entity);
    }
}
