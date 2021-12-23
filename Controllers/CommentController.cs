using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using FluentValidation.Results;
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
            int id = p.BlogID;
            CommentValidation bv = new CommentValidation();
            ValidationResult result = bv.Validate(p);
            if (result.IsValid)
            {
                p.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.CommentStatus = true;    
                cm.TAdd(p);
                return RedirectToAction("BlogReadAll", "Blog", new { @id = id });
            }
            else
            {

                foreach (var item in result.Errors)
                {
                    TempData["AletrMessage"] = item.ErrorMessage;
                }
            }
            return RedirectToAction("BlogReadAll", "Blog", new { @id = id });




        }

    }
}
