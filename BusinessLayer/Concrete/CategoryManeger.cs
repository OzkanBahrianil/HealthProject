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
  public  class CategoryManeger : ICategoryService
    {
        ICategoryDal _CategoryDal;

        public CategoryManeger(ICategoryDal categoryDal)
        {
            _CategoryDal = categoryDal;
        }

        public Category GetByIDT(int id)
        {
            return _CategoryDal.Get(x => x.CategoryID == id);
        }

        public List<Category> GetListT()
        {
            return _CategoryDal.List();
        }

        public void TAdd(Category t)
        {
            _CategoryDal.Insert(t);
        }

        public void TDelete(Category t)
        {
            _CategoryDal.Delete(t);
        }

        public void TUpdate(Category t)
        {
            _CategoryDal.Update(t);
        }

    }
}
