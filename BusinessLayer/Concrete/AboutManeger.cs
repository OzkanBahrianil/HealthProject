using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AboutManeger : IAboutService
    {
        IAbautDal _AboutDal;

        public AboutManeger(IAbautDal aboutDal)
        {
            _AboutDal = aboutDal;
        }

        public About GetByIDT(int id)
        {
            return _AboutDal.Get(x => x.AboutID == id);
        }

        public List<About> GetListT()
        {
            return _AboutDal.List();
        }

        public void TAdd(About t)
        {
            _AboutDal.Insert(t);
        }

        public void TDelete(About t)
        {
            _AboutDal.Delete(t);
        }

        public void TUpdate(About t)
        {
            _AboutDal.Update(t);
        }

       

    }
}
