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
    public class MedicalProductManeger : IMedicalProductService
    {
        IMedicalProductDal _MedicalProductDal;

        public MedicalProductManeger(IMedicalProductDal medicalProductDal)
        {
            _MedicalProductDal = medicalProductDal;
        }
        public List<MedicalProduct> GetProductListWithCategoryWithComments()
        {
            return _MedicalProductDal.GetListWithCategoryComment().Where(x => x.ProductStatus == true).ToList();
        }
        public List<MedicalProduct> GetProductListWithCategoryWithCommentsAdmin()
        {
            return _MedicalProductDal.GetListWithCategoryComment();
        }
        public List<MedicalProduct> Search(string key)
        {
            key = key.ToLower();
            return _MedicalProductDal.GetListWithCategoryComment().Where(p => p.ProductTitle.ToLower().Contains(key)
            || p.ProductShortContent.ToLower().Contains(key)
            || p.ProductStatus.ToString().ToLower().Contains(key)
            || p.User.UserName.ToLower().Contains(key)).ToList();
        }
        public MedicalProduct GetByIDT(int id)
        {
            return _MedicalProductDal.Get(x => x.ProductID == id);
        }

        public List<MedicalProduct> GetListT()
        {
            return _MedicalProductDal.List().Where(x => x.ProductStatus == true).ToList();
        }
        public List<MedicalProduct> GetListTAdmin()
        {
            return _MedicalProductDal.List().ToList();
        }
        public void TAdd(MedicalProduct t)
        {
            _MedicalProductDal.Insert(t);
        }

        public void TDelete(MedicalProduct t)
        {
            _MedicalProductDal.Delete(t);
        }

        public void TUpdate(MedicalProduct t)
        {
            _MedicalProductDal.Update(t);
        }

        public List<MedicalProduct> TGetByFilter(Expression<Func<MedicalProduct, bool>> filter)
        {
            return _MedicalProductDal.GetList(filter).Where(x => x.ProductStatus == true).ToList();
        }
        public List<MedicalProduct> TGetByFilterAdmin(Expression<Func<MedicalProduct, bool>> filter)
        {
            return _MedicalProductDal.GetList(filter);
        }
        public List<MedicalProduct> GetListWithCategoryCommentBCompanybm(int id)
        {
            return _MedicalProductDal.GetListWithCategoryCommentByCompany(id).Where(x => x.ProductStatus == true).ToList();
        }    
        public List<MedicalProduct> GetListWithCategoryCommentBCompanybmF(int id)
        {
            return _MedicalProductDal.GetListWithCategoryCommentByCompany(id);
        }
    }


}
