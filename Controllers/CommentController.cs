using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Controllers
{
    [AllowAnonymous]
    public class CommentController : Controller
    {
        CommentManeger cm = new CommentManeger(new EfCommentDal());
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult PartialAddComment()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult PartialAddComment(Comment p)
        {
            p.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.CommentStatus = true;
            int id = p.BlogID;
            cm.TAdd(p);
            return RedirectToAction("BlogReadAll","Blog", new { @id = id });
            


        }
        public PartialViewResult CommentListByBlog(int id)
        {
            var values = cm.GetByIDT(id);
            return PartialView(values);
        }
    }
}
