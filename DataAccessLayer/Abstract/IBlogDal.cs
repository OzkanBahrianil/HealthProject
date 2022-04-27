using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IBlogDal : IRepository<Blog>
    {
        List<Blog> GetListWithCategory();
        List<Blog> GetListWithCategoryWithComments();
        List<Blog> GetListWithUser();
        List<Blog> GetListWithCategoryByWriter(int id);

        List<Blog> GetList(Expression<Func<Blog, bool>> filter = null);
    }
}
