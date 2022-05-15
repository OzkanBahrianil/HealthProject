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
   public class PageVisitManeger : IPageVisitService
    {
        IPageVisitDal _PageVisitDal;

        public PageVisitManeger(IPageVisitDal pageVisitDal)
        {
            _PageVisitDal = pageVisitDal;
        }

        public PageVisit GetByIDT(int id)
        {
            return _PageVisitDal.Get(x => x.PageID == id);
        }
        public PageVisit GetByUrl(string url)
        {
            return _PageVisitDal.Get(x => x.PageUrl == url);
        }

        public List<PageVisit> GetListT()
        {
            return _PageVisitDal.List();
        }
        public List<PageVisit> Search(string key)
        {
            key = key.ToLower();
            return _PageVisitDal.List().Where(p => p.PageUrl.ToLower().Contains(key)
            || p.PageID.ToString().ToLower().Contains(key)
            || p.Visits.ToString().ToLower().Contains(key)).ToList();
        }
        public void TAdd(PageVisit t)
        {
            _PageVisitDal.Insert(t);
        }

        public void TDelete(PageVisit t)
        {
            _PageVisitDal.Delete(t);
        }

        public void TUpdate(PageVisit t)
        {
            _PageVisitDal.Update(t);
        }
    }
}
