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
  public  class CommentProductManeger : ICommentProductService
    {
        ICommentProductDal _CommentProductDal;

        public CommentProductManeger(ICommentProductDal commentProductDal)
        {
            _CommentProductDal = commentProductDal;
        }

        public CommentProduct GetByIDT(int id)
        {
            return _CommentProductDal.Get(x => x.CommentProductID == id);
        }

        public List<CommentProduct> GetCommentListByIdProduct(int id)
        {
            return _CommentProductDal.List().Where(p => p.ProductID == id).Where(x => x.CommentProductStatus == true).ToList();
        }
        public List<CommentProduct> GetCommentListByIdProductAdmin(int id)
        {
            return _CommentProductDal.List().Where(p => p.ProductID == id).ToList();
        }
        public List<CommentProduct> GetListT()
        {
            return _CommentProductDal.List().Where(x => x.CommentProductStatus == true).ToList();
        }
        public List<CommentProduct> GetListTAdmin()
        {
            return _CommentProductDal.List();
        }
        public void TAdd(CommentProduct t)
        {
            _CommentProductDal.Insert(t);
        }

        public void TDelete(CommentProduct t)
        {
            _CommentProductDal.Delete(t);
        }

        public void TUpdate(CommentProduct t)
        {
            _CommentProductDal.Update(t);
        }
    }
}
