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
  public class NewsLetterManeger : INewsLetterService
    {

        INewsLetterDal _NewsLetterDal;

        public NewsLetterManeger(INewsLetterDal NewsLetterDal)
        {
            _NewsLetterDal = NewsLetterDal;
        }

        public NewsLetter GetByIDT(int id)
        {
            return _NewsLetterDal.Get(x => x.MailID == id);
        }

        public List<NewsLetter> GetListT()
        {
            return _NewsLetterDal.List();
        }

        public void TAdd(NewsLetter t)
        {
            _NewsLetterDal.Insert(t);
        }

        public void TDelete(NewsLetter t)
        {
            _NewsLetterDal.Delete(t);
        }

        public void TUpdate(NewsLetter t)
        {
            _NewsLetterDal.Update(t);
        }

     
    }
}
