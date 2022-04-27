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
        BlogRaytingManeger brm = new BlogRaytingManeger(new EfBlogRaytingDal());

        [AllowAnonymous]
        public IActionResult Index(int page = 1)
        {
            var values = bm.GetBlogListWithCategoryWithComments().Where(y => y.BlogStatus == true).OrderByDescending(x => x.BlogCreateDate).ToList().ToPagedList(page, 9);
            return View(values);
        }

        [AllowAnonymous, HttpGet]
        public ActionResult WriterVideo(int id)
        {
            var model = wm.GetByIDT(id);
            return PartialView(model);
        }


        [AllowAnonymous]
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.CommentCount = cmt.GetCommentListByIdBlog(id).Count();
            ViewBag.i = id;
            var rayting = brm.GetListT().Where(x => x.BlogID == id).FirstOrDefault();
            if (rayting != null)
            {
                Decimal raytingstar = (Decimal)rayting.BlogTotalScore / (Decimal)rayting.BlogRaytingCount;
                raytingstar = Decimal.Round(raytingstar, 2);
                ViewBag.raytingstar = raytingstar;

            }
            else
            {
                ViewBag.raytingstar = 0;
            }
            var values = bm.GetBlogListByIdWithUser(id);
            return View(values);
        }
        [AllowAnonymous]
        public IActionResult BlogListByCategory(int id, int page = 1)
        {
            ViewBag.CategoryName = cm.GetByIDT(id).CategoryName;
            var values = bm.TGetByFilter(x => x.CategoryID == id).OrderByDescending(x => x.BlogCreateDate).ToList().ToPagedList(page, 9);
            return View(values);
        }



        [Authorize(Roles = "Writer")]
        public IActionResult BlogListByWriter(int page = 1)
        {
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;
            var values = bm.GetListWithCategoryByWriterbmF(writerID).ToPagedList(page, 10);
            return View(values);
        }
        [HttpGet, Authorize(Roles = "Writer")]
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
        [HttpPost, Authorize(Roles = "Writer")]
        public IActionResult BlogAddByWriter(AddBlogImage p)
        {
            Blog w = new Blog();
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;

            w.BlogShortContent = p.BlogShortContent;
            w.BlogContent = p.BlogContent;
            w.BlogTitle = p.BlogTitle;
            w.CategoryID = p.CategoryID;
            w.BlogStatus = false;
            w.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            w.UserID = writerID;
            BlogValidation bv = new BlogValidation();
            ValidationResult result = bv.Validate(w);
            List<SelectListItem> CategoryValue = (from x in cm.GetListT()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();

            if (result.IsValid)
            {
                if (p.BlogImage != null && p.BlogImage.FileName.Contains(".png"))
                {
                    var extension = Path.GetExtension(p.BlogImage.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var Location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", newImageName);
                    var stream = new FileStream(Location, FileMode.Create);
                    p.BlogImage.CopyTo(stream);
                    w.BlogImage = newImageName;

                }

                if (p.BlogThumbnailImage != null && p.BlogThumbnailImage.FileName.Contains(".png"))
                {
                    var extensionThumbnail = Path.GetExtension(p.BlogThumbnailImage.FileName);
                    var newImageNameThumbnail = Guid.NewGuid() + extensionThumbnail;
                    var LocationThumbnail = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", newImageNameThumbnail);
                    var streamThumbnail = new FileStream(LocationThumbnail, FileMode.Create);
                    p.BlogThumbnailImage.CopyTo(streamThumbnail);
                    w.BlogThumbnailImage = newImageNameThumbnail;
                }
                if (w.BlogImage != null && w.BlogThumbnailImage != null)
                {

                    if (w.BlogImage.Contains(".png") && w.BlogThumbnailImage.Contains(".png"))
                    {
                        bm.TAdd(w);
                        TempData["AletrMessage"] = "Ekleme İşlemi Başarılı...!";
                        return RedirectToAction("BlogListByWriter", "Blog");
                    }
                    else
                    {
                        TempData["AlertMessageAdd"] = "Sadece .png uzantılı resimler kabul edilir.";

                        ViewBag.cv = CategoryValue;
                    }
                }
                else
                {
                    TempData["AlertMessageAdd"] = "Sadece .png uzantılı resimler kabul edilir.";

                    ViewBag.cv = CategoryValue;
                }
                return View();

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
        [Authorize(Roles = "Writer")]
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
            return RedirectToAction("BlogListByWriter");

        }



        [HttpGet, Authorize(Roles = "Writer")]
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
            addBlogImage.BlogTitle = blogvalue.BlogTitle;
            addBlogImage.BlogImageString = blogvalue.BlogImage;
            addBlogImage.BlogThumbnailImageString = blogvalue.BlogThumbnailImage;
            addBlogImage.BlogShortContent = blogvalue.BlogShortContent;
            addBlogImage.BlogContent = blogvalue.BlogContent;
            addBlogImage.CategoryID = blogvalue.CategoryID;
            return View(addBlogImage);
        }
        [HttpPost, Authorize(Roles = "Writer")]
        public IActionResult EditBlog(AddBlogImage p)
        {
            Blog w = new Blog();
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;
            w.BlogID = p.BlogID;
            w.BlogShortContent = p.BlogShortContent;
            w.BlogContent = p.BlogContent;
            w.BlogTitle = p.BlogTitle;
            w.CategoryID = p.CategoryID;
            w.BlogStatus = false;
            w.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            w.UserID = writerID;
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
                return RedirectToAction("BlogListByWriter", "Blog");


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
