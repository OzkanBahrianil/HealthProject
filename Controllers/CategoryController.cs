using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Controllers
{
    public class CategoryController : Controller
    {
        CategoryManeger cm = new CategoryManeger(new EfCategoryDal());
        public IActionResult Index()
        {
            var values = cm.GetListT();
            return View(values);
        }
    }
}
