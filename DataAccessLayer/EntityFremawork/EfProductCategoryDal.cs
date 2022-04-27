using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFremawork
{
    public class EfProductCategoryDal : GenericRepository<ProductCategory>, IProductCategoryDal
    {
        public List<ProductCategory> GetListWithProduct()
        {
            using (var c = new Context())
            {
                return c.ProductCategory.Include(x => x.MedicalProducts).ToList();
            }
        }
    }
}
