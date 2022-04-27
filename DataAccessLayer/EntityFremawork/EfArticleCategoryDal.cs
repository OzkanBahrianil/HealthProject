using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFremawork
{
  public  class EfArticleCategoryDal : GenericRepository<ArticleCategory>, IArticleCategoryDal
    {
        public List<ArticleCategory> GetListWithBlog()
        {
            using (var c = new Context())
            {
                return c.ArticleCategories.Include(x => x.Articles).ToList();
            }
        }
    }
}
