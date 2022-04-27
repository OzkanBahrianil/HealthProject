using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
  public  class WriterManeger : IWriterService
    {
        IWriterDal _WriterDal;

        public WriterManeger(IWriterDal writerDal)
        {
            _WriterDal = writerDal;
        }

        public AppUser GetByIDT(int id)
        {
            return _WriterDal.Get(x => x.Id == id);
        }

        public List<AppUser> GetListT()
        {
            return _WriterDal.List();
        }

        public List<AppUser> GetWriterByID(int id)
        {
            return _WriterDal.List().Where(p => p.Id == id).ToList();
        }

        public void TAdd(AppUser t)
        {
            _WriterDal.Insert(t);
        }

        public void TDelete(AppUser t)
        {
            _WriterDal.Delete(t);
        }

        public void TUpdate(AppUser t)
        {
            _WriterDal.Update(t);
        }
        public AppUser TGetByFilter(Expression<Func<AppUser, bool>> filter)
        {
            return _WriterDal.GetByFilterFL(filter);
        }



    }
}
