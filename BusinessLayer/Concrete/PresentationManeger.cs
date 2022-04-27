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
  public  class PresentationManeger : IPresentationService
    {
        IPresentationDal _PresentationDal;

        public PresentationManeger(IPresentationDal presentationDal)
        {
            _PresentationDal = presentationDal;
        }

        public Presentation GetByIDT(int id)
        {
            return _PresentationDal.Get(x => x.PresentationID == id);
        }

        public List<Presentation> GetListT()
        {
            return _PresentationDal.List();
        }

        public void TAdd(Presentation t)
        {
            _PresentationDal.Insert(t);
        }

        public void TDelete(Presentation t)
        {
            _PresentationDal.Delete(t);
        }

        public void TUpdate(Presentation t)
        {
            _PresentationDal.Update(t);
        }
    }
}
