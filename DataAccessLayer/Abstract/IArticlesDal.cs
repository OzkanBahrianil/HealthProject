using DataAccessLayer.Concrete;
using EntityLayer.Concrate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IArticlesDal : IRepository<Articles>
    {
        List<Articles> GetListWithArticlesCategory();
        List<Articles> GetListWithCategoryByWriter(int id);

        List<Articles> GetList(Expression<Func<Articles, bool>> filter = null);
    }
}
