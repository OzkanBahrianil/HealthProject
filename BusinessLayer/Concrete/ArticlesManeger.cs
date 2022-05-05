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
  public class ArticlesManeger : IArticlesService
    {
        IArticlesDal _ArticlesDal;

        public ArticlesManeger(IArticlesDal articlesDal)
        {
            _ArticlesDal = articlesDal;
        }

        public List<Articles> GetArticlesListWithArticlesCategory()
        {
            return _ArticlesDal.GetListWithArticlesCategory().Where(x => x.ArticlesStatus == true).ToList();
        }
        public List<Articles> GetArticlesListWithArticlesCategoryAdmin()
        {
            return _ArticlesDal.GetListWithArticlesCategory();
        }
        public List<Articles> Search(string key)
        {
            key = key.ToLower();
            return _ArticlesDal.GetListWithArticlesCategory().Where(p => p.ArticlesTitle.ToLower().Contains(key) 
            || p.ArticlesShortContent.ToLower().Contains(key) 
            || p.ArticlesStatus.ToString().ToLower().Contains(key) 
            || p.User.UserName.ToLower().Contains(key) 
            || p.ArticlesType.ToLower().Contains(key) 
            || p.ArticlesWritersName.ToLower().Contains(key)).ToList();
        }
        public Articles GetByIDT(int id)
        {
            return _ArticlesDal.Get(x => x.ArticlesID == id);
        }

        public List<Articles> GetListT()
        {
            return _ArticlesDal.List().Where(x => x.ArticlesStatus == true).ToList();
        }
        public List<Articles> GetListTAdmin()
        {
            return _ArticlesDal.List().ToList();
        }
        public void TAdd(Articles t)
        {
            _ArticlesDal.Insert(t);
        }

        public void TDelete(Articles t)
        {
            _ArticlesDal.Delete(t);
        }

        public void TUpdate(Articles t)
        {
            _ArticlesDal.Update(t);
        }
        public List<Articles> TGetByFilter(Expression<Func<Articles, bool>> filter)
        {
            return _ArticlesDal.GetList(filter).Where(x => x.ArticlesStatus == true).ToList();
        }
        public List<Articles> TGetByFilterAdmin(Expression<Func<Articles, bool>> filter)
        {
            return _ArticlesDal.GetList(filter);
        }
        public List<Articles> GetListWithCategoryByWriterbm(int id)
        {
            return _ArticlesDal.GetListWithCategoryByWriter(id).Where(x => x.ArticlesStatus == true).ToList();
        }
        public List<Articles> GetListWithCategoryByWriterbmF(int id)
        {
            return _ArticlesDal.GetListWithCategoryByWriter(id);
        }
    }
}
