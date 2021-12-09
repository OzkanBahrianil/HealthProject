using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Blog
{
    public class WriterLastBlog: ViewComponent
    {
        BlogManeger bm = new BlogManeger(new EfBlogDal());
        public IViewComponentResult Invoke(int id)
        {
            var values = bm.GetListWriterBlog(id);
            return View(values);
        }
    }
}
