using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using HealthProject.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.Areas.MedicalProducts.Controllers
{
    [Area("MedicalProducts"),AllowAnonymous]
    [PageVisitCountFilter]
    public class HomeController : Controller
    {
        MedicalProductManeger mpm = new MedicalProductManeger(new EfMedicalProductDal());
        ProductCategoryManeger pcm = new ProductCategoryManeger(new EfProductCategoryDal());
        PageVisitManeger pvm = new PageVisitManeger(new EfPageVisitDal());
        public IActionResult ProductIndex(int page = 1)
        {
            var values = mpm.GetProductListWithCategoryWithComments().OrderByDescending(x=>x.ProductID).ToPagedList(page, 9);
            return View(values);
        }
        public IActionResult ProductReadAll(int id, int page = 1)
        {
            var Urlstring = HttpContext.Request.Path;
            var viewvalue = pvm.GetByUrl(Urlstring);
            if (viewvalue != null)
            {
                ViewBag.viewvalue = viewvalue.Visits;
            }
            ViewBag.page = page;
            ViewBag.i = id;
            var values = mpm.TGetByFilter(x=>x.ProductID==id);
            return View(values);
        }
        public IActionResult BlogListByProductCategory(int id, int page = 1)
        {
            ViewBag.CategoryName = pcm.GetByIDT(id).ProductCategoryName;
            var values = mpm.TGetByFilter(y => y.ProductCategoryID == id).OrderByDescending(x => x.ProductID).ToList().ToPagedList(page, 9);
            return View(values);
        }



    }
}
