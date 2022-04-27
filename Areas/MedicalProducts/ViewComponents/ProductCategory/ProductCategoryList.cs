using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Areas.MedicalProducts.ViewComponents.ProductCategory
{
    public class ProductCategoryList: ViewComponent
    {
        ProductCategoryManeger mpm = new ProductCategoryManeger(new EfProductCategoryDal());
        public IViewComponentResult Invoke()
        {
            var values = mpm.GetListT();
            return View(values);
        }
    }
}
