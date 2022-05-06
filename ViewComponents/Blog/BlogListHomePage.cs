using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.ViewComponents.Blog
{
    public class BlogListHomePage:ViewComponent
    {
        BlogManeger bm = new BlogManeger(new EfBlogDal());
        public IViewComponentResult Invoke(int page)
        {

            var values = bm.GetBlogListWithCategoryWithComments().OrderByDescending(x => x.BlogID).ToPagedList(page, 9);
            return View(values);
        }
    }
}
