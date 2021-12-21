using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Controllers
{

    public class BlogController : Controller
    {
        CategoryManeger cm = new CategoryManeger(new EfCategoryDal());
        BlogManeger bm = new BlogManeger(new EfBlogDal());
        WriterManeger wm = new WriterManeger(new EfWriterDal());
        [AllowAnonymous]
        public IActionResult Index()
        {
            var values = bm.GetBlogListWithCategory().OrderByDescending(x=>x.BlogCreateDate).ToList();
            return View(values);
        }
        [AllowAnonymous]
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;
            var values = bm.GetBlogListById(id);
            return View(values);
        }

        public IActionResult BlogListByWriter()
        {
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.WriterMail == usermail).WriterId;
            var values = bm.GetListWithCategoryByWriterbm(writerID);
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
        public IActionResult BlogAddByWriter(Blog p)
        {
            BlogValidation bv = new BlogValidation();
            ValidationResult result = bv.Validate(p);
            if (result.IsValid)
            {
                p.BlogStatus = true;
                p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                var usermail = User.Identity.Name;
                var writerID = wm.TGetByFilter(x => x.WriterMail == usermail).WriterId;
                p.WriterID = writerID;
                bm.TAdd(p);
                TempData["AletrMessage"] = "Ekleme İşlemi Başarılı...!";
                return RedirectToAction("BlogListByWriter", "Blog");

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
