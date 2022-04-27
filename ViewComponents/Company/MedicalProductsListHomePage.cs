using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Company
{
    public class MedicalProductsListHomePage:ViewComponent
    {
        MedicalProductManeger mpm = new MedicalProductManeger(new EfMedicalProductDal());
        public IViewComponentResult Invoke()
        {
            var values = mpm.GetProductListWithCategoryWithComments().OrderByDescending(x => x.ProductID).Take(10).ToList();
            return View(values);
        }
    }
}
