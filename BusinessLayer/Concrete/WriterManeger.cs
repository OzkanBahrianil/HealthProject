﻿using BusinessLayer.Abstract;
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

        public Writer GetByIDT(int id)
        {
            return _WriterDal.Get(x => x.WriterId == id);
        }

        public List<Writer> GetListT()
        {
            return _WriterDal.List();
        }

        public List<Writer> GetWriterByID(int id)
        {
            return _WriterDal.List().Where(p => p.WriterId == id).ToList();
        }

        public void TAdd(Writer t)
        {
            _WriterDal.Insert(t);
        }

        public void TDelete(Writer t)
        {
            _WriterDal.Delete(t);
        }

        public void TUpdate(Writer t)
        {
            _WriterDal.Update(t);
        }
        public Writer TGetByFilter(Expression<Func<Writer, bool>> filter)
        {
            return _WriterDal.GetByFilterFL(filter);
        }



    }
}
