using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Blog
{
    public class BlogLast3Post: ViewComponent
    {
        BlogManeger bm = new BlogManeger(new EfBlogDal());
        public IViewComponentResult Invoke()
        {
            var values = bm.GetListBlogLast3();
            return View(values);
        }
    }
}
