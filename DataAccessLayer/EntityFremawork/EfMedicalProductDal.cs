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
   public class EfMedicalProductDal : GenericRepository<MedicalProduct>, IMedicalProductDal
    {
        public List<MedicalProduct> GetListWithCategoryComment()
        {
            using (var c = new Context())
            {
                return c.MedicalProducts.Include(x => x.ProductCategory).Include(x => x.CommentProducts).ToList();
            }
        }
        public List<MedicalProduct> GetListWithCategoryCommentByCompany(int id)
        {
            using (var c = new Context())
            {
                return c.MedicalProducts.Include(x => x.ProductCategory).Include(x => x.CommentProducts).Include(x => x.User).Where(x => x.UserID == id).ToList();
            }
        }
        public List<MedicalProduct> GetList(Expression<Func<MedicalProduct, bool>> filter)
        {
            using (var c = new Context())
            {
                return c.MedicalProducts.Include(x => x.ProductCategory).Include(x => x.CommentProducts).Include(x => x.User).Where(filter).ToList();
            }
        }
    }
}
