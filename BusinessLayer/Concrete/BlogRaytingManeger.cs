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
 public   class BlogRaytingManeger : IBlogRaytingService
    {
        IBlogRaytingDal _BlogRaytingDal;

        public BlogRaytingManeger(IBlogRaytingDal blogRaytingDal)
        {
            _BlogRaytingDal = blogRaytingDal;
        }

        public BlogRayting GetByIDT(int id)
        {
            return _BlogRaytingDal.Get(x => x.BlogRaytingID == id);
        }

        public List<BlogRayting> GetListT()
        {
            return _BlogRaytingDal.List();
        }

        public void TAdd(BlogRayting t)
        {
            _BlogRaytingDal.Insert(t);
        }

        public void TDelete(BlogRayting t)
        {
            _BlogRaytingDal.Delete(t);
        }

        public void TUpdate(BlogRayting t)
        {
            _BlogRaytingDal.Update(t);
        }
    }
}
