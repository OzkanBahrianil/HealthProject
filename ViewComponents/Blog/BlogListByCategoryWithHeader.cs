using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Blog
{
    public class BlogListByCategoryWithHeader:ViewComponent
    {
        CategoryManeger cm = new CategoryManeger(new EfCategoryDal());
        public IViewComponentResult Invoke()
        {
         
            var values = cm.GetListT();
            return View(values);
        }
    }
}
