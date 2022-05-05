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
  public  class ArticleCategoryManeger : IArticleCategoryService
    {
        IArticleCategoryDal _ArticleCategoryDal;

        public ArticleCategoryManeger(IArticleCategoryDal articleCategoryDal)
        {
            _ArticleCategoryDal = articleCategoryDal;
        }

        public ArticleCategory GetByIDT(int id)
        {
            return _ArticleCategoryDal.Get(x => x.ArticleCategoryID == id);
        }   
        public List<ArticleCategory> GetCategoryListWithArticle()
        {
            return _ArticleCategoryDal.GetListWithBlog().Where(x=>x.ArticleCategoryStatus==true).ToList();
        }

        public List<ArticleCategory> GetListT()
        {
            return _ArticleCategoryDal.List().Where(x => x.ArticleCategoryStatus == true).ToList();
        }
        public List<ArticleCategory> GetListTAdmin()
        {
            return _ArticleCategoryDal.List();
        }

        public List<ArticleCategory> Search(string key)
        {
            key = key.ToLower();
            return _ArticleCategoryDal.List().Where(p => p.ArticleCategoryName.ToLower().Contains(key)
            || p.ArticleCategoryDescription.ToLower().Contains(key)
            || p.ArticleCategoryStatus.ToString().ToLower().Contains(key)).ToList();

        }

        public void TAdd(ArticleCategory t)
        {
            _ArticleCategoryDal.Insert(t);
        }

        public void TDelete(ArticleCategory t)
        {
            _ArticleCategoryDal.Delete(t);
        }

        public void TUpdate(ArticleCategory t)
        {
            _ArticleCategoryDal.Update(t);
        }
    }
}
