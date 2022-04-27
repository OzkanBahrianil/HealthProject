using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.Areas.MedicalProducts.Controllers
{
    [Area("MedicalProducts"), AllowAnonymous]
    public class SearchController : Controller
    {

        MedicalProductManeger mpm = new MedicalProductManeger(new EfMedicalProductDal());
        public IActionResult Search(string Search, int page = 1)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                ViewBag.Search = Search;
                var SearchValues = mpm.TGetByFilter(x => x.ProductTitle.Contains(Search)).OrderByDescending(x => x.ProductRealiseDate).ToList().ToPagedList(page, 9);
                return View(SearchValues);
            }
            else
            {
                var Values = mpm.GetProductListWithCategoryWithComments().OrderByDescending(x => x.ProductRealiseDate).ToList().ToPagedList(page, 9);
                return View(Values);
            }


        }

    }
}
