﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Blog
{
    public class BlogListDashboard: ViewComponent
    {

        BlogManeger bm = new BlogManeger(new EfBlogDal());
        public IViewComponentResult Invoke()
        {
            var values = bm.GetBlogListWithCategoryWithComments().OrderByDescending(x=>x.BlogCreateDate).Take(10).ToList();
            return View(values);
        }
    }
}
