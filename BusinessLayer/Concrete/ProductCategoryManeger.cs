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
   public class ProductCategoryManeger : IProductCategoryService
    {
        IProductCategoryDal _ProductCategoryDal;

        public ProductCategoryManeger(IProductCategoryDal productCategoryDal)
        {
            _ProductCategoryDal = productCategoryDal;
        }

        public ProductCategory GetByIDT(int id)
        {
            return _ProductCategoryDal.Get(x => x.ProductCategoryID == id);
        }

        public List<ProductCategory> GetCategoryListWithProduct()
        {
            return _ProductCategoryDal.GetListWithProduct().Where(x => x.ProductCategoryStatus == true).ToList();
        }

        public List<ProductCategory> GetListT()
        {
            return _ProductCategoryDal.List().Where(x => x.ProductCategoryStatus == true).ToList();
        }
        public List<ProductCategory> GetListTAdmin()
        {
            return _ProductCategoryDal.List();
        }
        public List<ProductCategory> Search(string key)
        {
            key = key.ToLower();
            return _ProductCategoryDal.List().Where(p => p.ProductCategoryName.ToLower().Contains(key)
            || p.ProductCategoryDescription.ToLower().Contains(key)
            || p.ProductCategoryStatus.ToString().ToLower().Contains(key)).ToList();

        }

        public void TAdd(ProductCategory t)
        {
            _ProductCategoryDal.Insert(t);
        }

        public void TDelete(ProductCategory t)
        {
            _ProductCategoryDal.Delete(t);
        }

        public void TUpdate(ProductCategory t)
        {
            _ProductCategoryDal.Update(t);
        }


    }
}
