using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFremawork
{
   public class EfArticlesDal : GenericRepository<Articles>, IArticlesDal
    {
        public List<Articles> GetListWithArticlesCategory()
        {
            using (var c = new Context())
            {
                return c.Articles.Include(x => x.ArticleCategory).Include(y=>y.User).ToList();
            }
        }
        public List<Articles> GetListWithCategoryByWriter(int id)
        {
            using (var c = new Context())
            {
                return c.Articles.Include(x => x.ArticleCategory).Include(x => x.User).Where(x => x.UserID == id).ToList();
            }
        }
        public List<Articles> GetList(Expression<Func<Articles, bool>> filter)
        {
            using (var c = new Context())
            {
                return c.Articles.Include(x => x.ArticleCategory).Include(x => x.User).Where(filter).ToList();
            }
        }

    }
}
