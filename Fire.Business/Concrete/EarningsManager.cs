using Fire.Business.Abstrack;
using Fire.DataAccess.Abstrack;
using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Concrete
{
    public class EarningsManager : IEarningsService
    {
        private readonly IEarningsDal _earningsDal;
        public EarningsManager(IEarningsDal earningsDal)
        {
            _earningsDal = earningsDal;
        }
        public void Create(Earnings entity)
        {
            _earningsDal.Create(entity);
        }

        public void Delete(Earnings entity)
        {
            _earningsDal.Delete(entity);
        }

        public List<Earnings> GetAll()
        {
            return _earningsDal.GetAll();
        }

        public Earnings GetById(int id)
        {
            return _earningsDal.GetById(id);
        }

        public void Update(Earnings entity)
        {
            _earningsDal.Update(entity);
        }
    }
}
