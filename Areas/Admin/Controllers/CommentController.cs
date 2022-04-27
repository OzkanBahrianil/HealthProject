using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class CommentController : Controller
    {
        CommentManeger cm = new CommentManeger(new EfCommentDal());
        CommentProductManeger cpm = new CommentProductManeger(new EfCommentProductDal());
        public IActionResult Index(int page = 1)
        {
            var values = cm.GetListTAdmin().Where(y=>y.CommentStatus==false).OrderByDescending(x => x.CommentDate).ToList().ToPagedList(page, 20);
            return View(values);
        }
        [HttpGet]
        public IActionResult DeleteComment(int id)
        {
            var values = cm.GetByIDT(id);
            cm.TDelete(values);
            return RedirectToAction("Index");
        }   
        [HttpGet]
        public IActionResult ApproveComment(int id)
        {
            var values = cm.GetByIDT(id);
            values.CommentStatus = true;
            cm.TUpdate(values);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCommentBlog(int CommentId)
        {
            var values = cm.GetListTAdmin().FirstOrDefault(x => x.CommentID == CommentId);
            cm.TDelete(values);
            return Json(values);
        }
      
        public IActionResult ApproveCommentBlog(int CommentId)
        {
            var values = cm.GetListTAdmin().FirstOrDefault(x => x.CommentID == CommentId);
            values.CommentStatus = true;
            cm.TUpdate(values);
            return Json(values);
        }

        public IActionResult DeleteCommentProduct(int CommentId)
        {
            var values = cpm.GetListTAdmin().FirstOrDefault(x => x.ProductID == CommentId);
            cpm.TDelete(values);
            return Json(values);
        }

        public IActionResult ApproveCommentProduct(int CommentId)
        {
            var values = cpm.GetListTAdmin().FirstOrDefault(x => x.ProductID == CommentId);
            values.CommentProductStatus = true;
            cpm.TUpdate(values);
            return Json(values);
        }

        public IActionResult IndexProduct(int page = 1)
        {
            var values = cpm.GetListTAdmin().Where(y => y.CommentProductStatus == false).OrderByDescending(x => x.CommentProductDate).ToList().ToPagedList(page, 20);
            return View(values);
        }
        [HttpGet]
        public IActionResult DeleteCommentProductFalse(int id)
        {
            var values = cpm.GetByIDT(id);
            cpm.TDelete(values);
            return RedirectToAction("IndexProduct");
        }
        [HttpGet]
        public IActionResult ApproveCommentProductFalse(int id)
        {
            var values = cpm.GetByIDT(id);
            values.CommentProductStatus = true;
            cpm.TUpdate(values);
            return RedirectToAction("IndexProduct");
        }

    }
}
