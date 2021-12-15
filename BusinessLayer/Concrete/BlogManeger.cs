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
    public class BlogManeger : IBlogService
    {
        IBlogDal _BlogDal;

        public BlogManeger(IBlogDal blogDal)
        {
            _BlogDal = blogDal;
        }

        public List<Blog> GetBlogListWithCategory()
        {
            return _BlogDal.GetListWithCategory();
        }

        public Blog GetByIDT(int id)
        {
            return _BlogDal.Get(x => x.BlogID == id);
        }

     
        public List<Blog> GetListBlogLast3()
        {
            return _BlogDal.List().Take(3).ToList();
        }

        public List<Blog> GetListT()
        {
            return _BlogDal.List();
        }

        public List<Blog> GetListWriterBlog(int id)
        {
          
              var a =  _BlogDal.List().Where(p => p.WriterID == id);
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
            return _BlogDal.GetListWithCategoryByWriter(id);
        }

        public List<Blog> GetBlogListById(int id)
        {
           return _BlogDal.List().Where(p => p.BlogID == id).ToList();
        }
    }
}
