using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
  public  interface IArticlesService : IGenericService<Articles>
    {
        List<Articles> GetArticlesListWithArticlesCategory();
        List<Articles> TGetByFilter(Expression<Func<Articles, bool>> filter);
    }
}
