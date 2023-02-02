using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Abstrack
{
    public interface IEarningsService
    {
        List<Earnings> GetAll();
        Earnings GetById(int id);
        void Create(Earnings entity);
        void Delete(Earnings entity);
        void Update(Earnings entity);
    }
}
