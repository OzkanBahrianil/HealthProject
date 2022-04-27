using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Company
{
    public class CategoryListDashboardProduct : ViewComponent
    {
        ProductCategoryManeger cm = new ProductCategoryManeger(new EfProductCategoryDal());
        public IViewComponentResult Invoke()
        {
            var values = cm.GetCategoryListWithProduct();
            return View(values);
        }
    }
}
