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

namespace HealthProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class AdminBlogController : Controller
    {
        CategoryManeger cm = new CategoryManeger(new EfCategoryDal());
        BlogManeger bm = new BlogManeger(new EfBlogDal());
        WriterManeger wm = new WriterManeger(new EfWriterDal());
        CommentManeger cmt = new CommentManeger(new EfCommentDal());
        public IActionResult Index(string SearchString, int page = 1)
        {
            ViewData["CurrentFilter"] = SearchString;
            if (!String.IsNullOrEmpty(SearchString))
            {
                var valuesSearch = bm.Search(SearchString).OrderByDescending(x => x.BlogStatus).ToPagedList(page, 9);
                return View(valuesSearch);
            }
            else
            {
                var values = bm.GetBlogListWithCategoryWithCommentsAdmin().OrderByDescending(x => x.BlogCreateDate).ToList().ToPagedList(page, 9);
                return View(values);
            }
          
        }
        public IActionResult AdminReadAll(int id)
        {
            ViewBag.CommentCount = cmt.GetCommentListByIdBlogAdmin(id).Count();
            ViewBag.i = id;
            var values = bm.GetBlogListByIdAdmin(id);
            return View(values);
        }

        public IActionResult DeleteBlog(int id)
        {
            var blogvalue = bm.GetByIDT(id);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", blogvalue.BlogImage);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            var path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", blogvalue.BlogThumbnailImage);
            if (System.IO.File.Exists(path2))
            {
                System.IO.File.Delete(path2);
            }
            bm.TDelete(blogvalue);
            TempData["AletrMessage"] = "Silme İşlemi Başarılı...!";
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult DisableBlog(int id)
        {
          
            var values = bm.GetByIDT(id);
            values.BlogStatus = false;
            bm.TUpdate(values);
            TempData["AletrMessage"] = "Deactive İşlemi Başarılı...!"+ id;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EnableBlog(int id)
        {
          
            var values = bm.GetByIDT(id);
            values.BlogStatus = true;
            bm.TUpdate(values);
            TempData["AletrMessage"] = "active İşlemi Başarılı...!" + id;
            return RedirectToAction("Index");
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
            AddBlogImage addBlogImage = new AddBlogImage();
            addBlogImage.BlogID = blogvalue.BlogID;
            addBlogImage.WriterID = blogvalue.UserID;
            addBlogImage.BlogTitle = blogvalue.BlogTitle;
            addBlogImage.BlogImageString = blogvalue.BlogImage;
            addBlogImage.BlogThumbnailImageString = blogvalue.BlogThumbnailImage;
            addBlogImage.BlogShortContent = blogvalue.BlogShortContent;
            addBlogImage.BlogContent = blogvalue.BlogContent;
            addBlogImage.CategoryID = blogvalue.CategoryID;
            return View(addBlogImage);
        }
        [HttpPost]
        public IActionResult EditBlog(AddBlogImage p)
        {
            Blog w = new Blog();
        
           
            w.BlogID = p.BlogID;
            w.BlogShortContent = p.BlogShortContent;
            w.BlogContent = p.BlogContent;
            w.BlogTitle = p.BlogTitle;
            w.CategoryID = p.CategoryID;
            w.BlogStatus = true;
            w.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            w.UserID = p.WriterID;
            BlogValidation bv = new BlogValidation();
            ValidationResult result = bv.Validate(w);
            TempData["AlertMessageAdd"] = "Sadece .png uzantılı resimler kabul edilir.";
            List<SelectListItem> CategoryValue = (from x in cm.GetListT()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();

            if (result.IsValid)
            {
                if (p.BlogImage != null)
                {
                    if (p.BlogImage.FileName.Contains(".png"))
                    {
                        var extension = Path.GetExtension(p.BlogImage.FileName);
                        var newImageName = Guid.NewGuid() + extension;
                        var Location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", newImageName);
                        var stream = new FileStream(Location, FileMode.Create);
                        p.BlogImage.CopyTo(stream);
                        w.BlogImage = newImageName;

                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", p.BlogImageString);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    else
                    {
                        TempData["AlertMessageAdd"] = "Sadece .png uzantılı resimler kabul edilir.";
                        ViewBag.cv = CategoryValue;
                        return View();
                    }

                }
                else
                {
                    w.BlogImage = p.BlogImageString;

                }
                if (p.BlogThumbnailImage != null)
                {
                    if (p.BlogThumbnailImage.FileName.Contains(".png"))
                    {


                        var extensionThumbnail = Path.GetExtension(p.BlogThumbnailImage.FileName);
                        var newImageNameThumbnail = Guid.NewGuid() + extensionThumbnail;
                        var LocationThumbnail = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", newImageNameThumbnail);
                        var streamThumbnail = new FileStream(LocationThumbnail, FileMode.Create);
                        p.BlogThumbnailImage.CopyTo(streamThumbnail);
                        w.BlogThumbnailImage = newImageNameThumbnail;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", p.BlogThumbnailImageString);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    else
                    {

                        ViewBag.cv = CategoryValue;
                        return View();
                    }
                }
                else
                {
                    w.BlogThumbnailImage = p.BlogThumbnailImageString;
                }


                bm.TUpdate(w);
                TempData["AletrMessage"] = "Güncelleme İşlemi Başarılı...!";
                return RedirectToAction("Index", "AdminBlog");


            }
            else
            {
                foreach (var item in result.Errors)
                {
                    TempData["AlertMessageAdd"] = item.ErrorMessage;

                }
                ViewBag.cv = CategoryValue;
                return View();
            }







        }
    }
}
