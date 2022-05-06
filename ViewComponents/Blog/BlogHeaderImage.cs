using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Blog
{

    public class BlogHeaderImage : ViewComponent
    {
        BlogManeger bm = new BlogManeger(new EfBlogDal());
        BlogRaytingManeger brm = new BlogRaytingManeger(new EfBlogRaytingDal());
        public IViewComponentResult Invoke()
        {
            var values = brm.GetListT().OrderByDescending(x => x.BlogTotalScore).Take(3);
            List<EntityLayer.Concrate.Blog> cs_list = new List<EntityLayer.Concrate.Blog>();
            foreach (var item in values)
            {
                var blog = bm.GetListT().Where(x => x.BlogID == item.BlogID).FirstOrDefault();
                if (blog != null)
                {
                    cs_list.Add(blog);
                }





            }
            return View(cs_list);


        }
    }
}
