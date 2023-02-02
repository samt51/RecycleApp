using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire.Business.Abstrack
{
    public interface IFactoryService
    {
        List<Factory> GetAll();
        Factory GetById(int id);
        void Create(Factory entity);
        void Delete(Factory entity);
        void Update(Factory entity);
    }
}
