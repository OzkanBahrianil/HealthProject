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
    public class EfBlogDal : GenericRepository<Blog>, IBlogDal
    {
        public List<Blog> GetListWithCategory()
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Category).ToList();
            }
        }
        public List<Blog> GetListWithCategoryWithComments()
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Category).Include(x => x.Comments).Include(x=>x.User).ToList();
            }
        }

        public List<Blog> GetListWithCategoryByWriter(int id)
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Category).Include(x => x.Comments).Where(x=>x.UserID == id).ToList();
            }
        }
        public List<Blog> GetList(Expression<Func<Blog, bool>> filter)
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x=>x.Category).Include(x=>x.Comments).Where(filter).ToList();
            }
        }
        public List<Blog> GetListWithUser()
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.User).ToList();
            }
        }

    }
}
