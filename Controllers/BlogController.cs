using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using FluentValidation.Results;
using HealthProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.Controllers
{

    public class BlogController : Controller
    {
        CategoryManeger cm = new CategoryManeger(new EfCategoryDal());
        BlogManeger bm = new BlogManeger(new EfBlogDal());
        WriterManeger wm = new WriterManeger(new EfWriterDal());
        CommentManeger cmt = new CommentManeger(new EfCommentDal());
        [AllowAnonymous]
        public IActionResult Index(int page = 1)
        {
            var values = bm.GetBlogListWithCategoryWithComments().OrderByDescending(x => x.BlogID).ToList().ToPagedList(page, 9);
            return View(values);
        }
        [AllowAnonymous]
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.CommentCount = cmt.GetCommentListByIdBlog(id).Count();
            ViewBag.i = id;
            var values = bm.GetBlogListById(id);
            return View(values);
        }

        public IActionResult BlogListByWriter(int page = 1)
        {
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.WriterMail == usermail).WriterId;
            var values = bm.GetListWithCategoryByWriterbm(writerID).ToPagedList(page, 10);
            return View(values);
        }
        [HttpGet]
        public IActionResult BlogAddByWriter()
        {


            List<SelectListItem> CategoryValue = (from x in cm.GetListT()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.cv = CategoryValue;
            return View();
        }
        [HttpPost]
        public IActionResult BlogAddByWriter(AddBlogImage p)
        {
            Blog w = new Blog();
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.WriterMail == usermail).WriterId;
            if (p.BlogImage != null)
            {
                var extension = Path.GetExtension(p.BlogImage.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var Location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", newImageName);
                var stream = new FileStream(Location, FileMode.Create);
                p.BlogImage.CopyTo(stream);
                w.BlogImage = newImageName;
            }
            if (p.BlogThumbnailImage != null)
            {
                var extensionThumbnail = Path.GetExtension(p.BlogThumbnailImage.FileName);
                var newImageNameThumbnail = Guid.NewGuid() + extensionThumbnail;
                var LocationThumbnail = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", newImageNameThumbnail);
                var streamThumbnail = new FileStream(LocationThumbnail, FileMode.Create);
                p.BlogThumbnailImage.CopyTo(streamThumbnail);
                w.BlogThumbnailImage = newImageNameThumbnail;
            }
            w.BlogShortContent = p.BlogShortContent;
            w.BlogContent = p.BlogContent;
            w.BlogTitle = p.BlogTitle;
            w.CategoryID = p.CategoryID;
            w.BlogStatus = true;
            w.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            w.WriterID = writerID;
            BlogValidation bv = new BlogValidation();
            ValidationResult result = bv.Validate(w);
            if (result.IsValid)
            { 
                bm.TAdd(w);
            TempData["AletrMessage"] = "Ekleme İşlemi Başarılı...!";
            return RedirectToAction("BlogListByWriter", "Blog");

            }
            else
            {
                foreach (var item in result.Errors)
                {
                    TempData["AlertMessageAdd"] = item.ErrorMessage;

                }
                List<SelectListItem> CategoryValue = (from x in cm.GetListT()
                                                      select new SelectListItem
                                                      {
                                                          Text = x.CategoryName,
                                                          Value = x.CategoryID.ToString()
                                                      }).ToList();
                ViewBag.cv = CategoryValue;
                return View();
            }
          
        }
        public IActionResult DeleteBlog(int id)
        {
            var blogvalue = bm.GetByIDT(id);
            bm.TDelete(blogvalue);
            TempData["AletrMessage"] = "Silme İşlemi Başarılı...!";
            return RedirectToAction("BlogListByWriter");
        }
        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            var blogvalue = bm.GetByIDT(id);

            List<SelectListItem> CategoryValue = (from x in cm.GetListT()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.cv = CategoryValue;

            return View(blogvalue);
        }
        [HttpPost]
        public IActionResult EditBlog(Blog p)
        {
            BlogValidation bv = new BlogValidation();
            ValidationResult result = bv.Validate(p);
            if (result.IsValid)
            {
                var usermail = User.Identity.Name;
                var writerID = wm.TGetByFilter(x => x.WriterMail == usermail).WriterId;
                p.WriterID = writerID;
                p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.BlogStatus = true;
                bm.TUpdate(p);
                TempData["AletrMessage"] = "Güncelleme İşlemi Başarılı...!";
                return RedirectToAction("BlogListByWriter");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}
