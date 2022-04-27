using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IMedicalProductDal : IRepository<MedicalProduct>
    {
        List<MedicalProduct> GetListWithCategoryComment();
        List<MedicalProduct> GetListWithCategoryCommentByCompany(int id);

        List<MedicalProduct> GetList(Expression<Func<MedicalProduct, bool>> filter = null);
    }
}
