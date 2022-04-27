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
    public class BlogManeger : IBlogService
    {
        IBlogDal _BlogDal;

        public BlogManeger(IBlogDal blogDal)
        {
            _BlogDal = blogDal;
        }

        public List<Blog> GetBlogListWithCategory()
        {
            return _BlogDal.GetListWithCategory().Where(x => x.BlogStatus == true).ToList();
        }
        public List<Blog> GetBlogListWithCategoryWithComments()
        {
            return _BlogDal.GetListWithCategoryWithComments().Where(x => x.BlogStatus == true).ToList();
        }
        public List<Blog> GetBlogListWithCategoryWithCommentsAdmin()
        {
            return _BlogDal.GetListWithCategoryWithComments();
        }

        public Blog GetByIDT(int id)
        {
            return _BlogDal.Get(x => x.BlogID == id);
        }

     
        public List<Blog> GetListBlogLast3()
        {
            return _BlogDal.List().OrderByDescending(x=>x.BlogID).Take(3).ToList().Where(x => x.BlogStatus == true).ToList();
        }

        public List<Blog> GetListT()
        {
            return _BlogDal.List().Where(x => x.BlogStatus == true).ToList();
        }

        public List<Blog> GetListWriterBlog(int id)
        {
          
              var a =  _BlogDal.List().Where(p => p.UserID == id).Where(x => x.BlogStatus == true).ToList();
              return a.ToList();
        }

        public void TAdd(Blog t)
        {
            _BlogDal.Insert(t);
        }

        public void TDelete(Blog t)
        {
            _BlogDal.Delete(t);
        }

        public void TUpdate(Blog t)
        {
            _BlogDal.Update(t);
        }

        public List<Blog> GetListWithCategoryByWriterbm(int id)
        {
            return _BlogDal.GetListWithCategoryByWriter(id).Where(x => x.BlogStatus == true).ToList();
        }
        public List<Blog> GetListWithCategoryByWriterbmF(int id)
        {
            return _BlogDal.GetListWithCategoryByWriter(id);
        }

        public List<Blog> GetBlogListById(int id)
        {
           return _BlogDal.List().Where(p => p.BlogID == id).Where(x => x.BlogStatus == true).ToList();
        }    
        
        public List<Blog> GetBlogListByIdWithUser(int id)
        {
           return _BlogDal.GetListWithUser().Where(p => p.BlogID == id).Where(x => x.BlogStatus == true).ToList();
        }
        public List<Blog> GetBlogListByIdAdmin(int id)
        {
            return _BlogDal.List().Where(p => p.BlogID == id).ToList();
        }

        public List<Blog> TGetByFilter(Expression<Func<Blog, bool>> filter)
        {
            return _BlogDal.GetList(filter).Where(x => x.BlogStatus == true).ToList();
        }
    }
}
