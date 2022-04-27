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
   public class CommentManeger : ICommentService
    {
        ICommentDal _CommentDal;

        public CommentManeger(ICommentDal commentDal)
        {
            _CommentDal = commentDal;
        }

        public Comment GetByIDT(int id)
        {
            return _CommentDal.Get(x => x.CommentID == id);
        }

        public List<Comment> GetCommentListByIdBlog(int id)
        {
            return _CommentDal.List().Where(p => p.BlogID == id).ToList().Where(x => x.CommentStatus == true).ToList();
        }
        public List<Comment> GetCommentListByIdBlogAdmin(int id)
        {
            return _CommentDal.List().Where(p => p.BlogID == id).ToList();
        }

        public List<Comment> GetListT()
        {
            return _CommentDal.List().Where(x => x.CommentStatus == true).ToList();
        }
        public List<Comment> GetListTAdmin()
        {
            return _CommentDal.List();
        }

        public void TAdd(Comment t)
        {
           _CommentDal.Insert(t);
        }

        public void TDelete(Comment t)
        {
            _CommentDal.Delete(t);
        }

        public void TUpdate(Comment t)
        {
            _CommentDal.Update(t);
        }

    
    }
}
