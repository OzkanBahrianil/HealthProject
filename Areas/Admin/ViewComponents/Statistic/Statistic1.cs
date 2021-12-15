using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic1:ViewComponent
    {
        BlogManeger bm = new BlogManeger(new EfBlogDal());
        ContactManeger cm = new ContactManeger(new EfContactDal());
        CommentManeger cmm = new CommentManeger(new EfCommentDal());
        public IViewComponentResult Invoke()
        {
            ViewBag.BLC = bm.GetListT().Count();
            ViewBag.CLC = cm.GetListT().Count();
            ViewBag.CmLC = cmm.GetListT().Count();
            return View();
        }

    }
}
