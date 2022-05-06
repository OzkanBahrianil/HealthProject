using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.ViewComponents.Company
{
    public class MedicalProductsListHomePage:ViewComponent
    {
        MedicalProductManeger mpm = new MedicalProductManeger(new EfMedicalProductDal());
        public IViewComponentResult Invoke(int page)
        {
            var values = mpm.GetProductListWithCategoryWithComments().OrderByDescending(x => x.ProductID).ToPagedList(page, 10);
            return View(values);
        }
    }
}
