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
            var values = bm.GetBlogListWithCategoryWithComments().OrderByDescending(x => x.BlogCreateDate).ToList().ToPagedList(page, 9);
            return View(values);
        }

        [AllowAnonymous, HttpGet]
        public ActionResult WriterVideo(int id)
        {
            var model = wm.GetByIDT(id);
            return PartialView(model);
        }


        [AllowAnonymous]
        public IActionResult BlogReadAll(int id, int page = 1)
        {
            ViewBag.page = page;
            ViewBag.CommentCount = cmt.GetCommentListByIdBlog(id).Count();
            ViewBag.i = id;
            var rayting = brm.GetListT().Where(x => x.BlogID == id).FirstOrDefault();
            if (rayting != null)
            {
                if (rayting.BlogRaytingCount == 0)
                {
                    ViewBag.raytingstar = 0;
                }
                else
                {
                    Decimal raytingstar = (Decimal)rayting.BlogTotalScore / (Decimal)rayting.BlogRaytingCount;
                    raytingstar = Decimal.Round(raytingstar, 2);
                    ViewBag.raytingstar = raytingstar;

                }


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
        public IActionResult BlogListByWriter(string sortOrder, string SearchString, int page = 1)
        {
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;
            ViewData["CurrentFilterSearch"] = SearchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParam"] = sortOrder == "Title" ? "TitleDesc" : "Title";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "DateDesc" : "Date";
            ViewData["CategorySortParam"] = sortOrder == "Category" ? "CategoryDesc" : "Category";
            ViewData["StatusParam"] = sortOrder == "Status" ? "StatusDesc" : "Status";
            ViewData["CauntParam"] = sortOrder == "Caunt" ? "CauntDesc" : "Caunt";
            if (!String.IsNullOrEmpty(SearchString))
            {
                var values = bm.GetListWithCategoryByWriterbmFSearch(SearchString, writerID);
                switch (sortOrder)
                {
                    case "Title":
                        values = values.OrderBy(x => x.BlogTitle).ToList();
                        break;
                    case "TitleDesc":
                        values = values.OrderByDescending(x => x.BlogTitle).ToList();
                        break;
                    case "Date":
                        values = values.OrderBy(s => s.BlogCreateDate).ToList();
                        break;
                    case "DateDesc":
                        values = values.OrderByDescending(s => s.BlogCreateDate).ToList();
                        break;
                    case "Category":
                        values = values.OrderBy(s => s.Category.CategoryName).ToList();
                        break;
                    case "CategoryDesc":
                        values = values.OrderByDescending(s => s.Category.CategoryName).ToList();
                        break;
                    case "Status":
                        values = values.OrderBy(s => s.BlogStatus).ToList();
                        break;
                    case "StatusDesc":
                        values = values.OrderByDescending(s => s.BlogStatus).ToList();
                        break;
                    case "Caunt":
                        values = values.OrderBy(s => s.Comments.Count()).ToList();
                        break;
                    case "CauntDesc":
                        values = values.OrderByDescending(s => s.Comments.Count()).ToList();
                        break;
                    default:
                        values = values.OrderByDescending(s => s.BlogID).ToList();
                        break;
                }
                return View(values.ToPagedList(page, 10));
            }
            else
            {
                var values = bm.GetListWithCategoryByWriterbmF(writerID);
                switch (sortOrder)
                {
                    case "Title":
                        values = values.OrderBy(x => x.BlogTitle).ToList();
                        break;
                    case "TitleDesc":
                        values = values.OrderByDescending(x => x.BlogTitle).ToList();
                        break;
                    case "Date":
                        values = values.OrderBy(s => s.BlogCreateDate).ToList();
                        break;
                    case "DateDesc":
                        values = values.OrderByDescending(s => s.BlogCreateDate).ToList();
                        break;
                    case "Category":
                        values = values.OrderBy(s => s.Category.CategoryName).ToList();
                        break;
                    case "CategoryDesc":
                        values = values.OrderByDescending(s => s.Category.CategoryName).ToList();
                        break;
                    case "Status":
                        values = values.OrderBy(s => s.BlogStatus).ToList();
                        break;
                    case "StatusDesc":
                        values = values.OrderByDescending(s => s.BlogStatus).ToList();
                        break;
                    case "Caunt":
                        values = values.OrderBy(s => s.Comments.Count()).ToList();
                        break;
                    case "CauntDesc":
                        values = values.OrderByDescending(s => s.Comments.Count()).ToList();
                        break;
                    default:
                        values = values.OrderByDescending(s => s.BlogID).ToList();
                        break;
                }
                return View(values.ToPagedList(page, 10));
            }



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
        [ValidateAntiForgeryToken]
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
            ViewBag.cv = CategoryValue;
            if (result.IsValid)
            {
                if (p.BlogImage != null)
                {
                    var extension = Path.GetExtension(p.BlogImage.FileName);
                    if (extension == ".png")
                    {
                        var newImageName = Guid.NewGuid() + extension;
                        var Location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", newImageName);
                        var stream = new FileStream(Location, FileMode.Create);
                        p.BlogImage.CopyTo(stream);
                        w.BlogImage = newImageName;
                    }
                    else
                    {
                        TempData["AlertMessageAdd"] = "Sadece .png uzantılı resimler kabul edilir.";
                    }



                }

                if (p.BlogThumbnailImage != null)
                {
                    var extensionThumbnail = Path.GetExtension(p.BlogThumbnailImage.FileName);
                    if (extensionThumbnail == ".png")
                    {
                        var newImageNameThumbnail = Guid.NewGuid() + extensionThumbnail;
                        var LocationThumbnail = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImageFiles/", newImageNameThumbnail);
                        var streamThumbnail = new FileStream(LocationThumbnail, FileMode.Create);
                        p.BlogThumbnailImage.CopyTo(streamThumbnail);
                        w.BlogThumbnailImage = newImageNameThumbnail;
                    }
                    else
                    {
                        TempData["AlertMessageAdd"] = "Sadece .png uzantılı resimler kabul edilir.";
                    }


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
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;
            if (blogvalue != null)
            {


                if (blogvalue.UserID == writerID)
                {


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
                else
                {
                    TempData["AletrMessage"] = "Yetkiniz Yoktur...!";
                    return RedirectToAction("BlogListByWriter");
                }
            }
            else
            {
                TempData["AletrMessage"] = "Böyle bir blog yoktur.!";
                return RedirectToAction("BlogListByWriter");
            }

        }



        [HttpGet, Authorize(Roles = "Writer")]
        public IActionResult EditBlog(int id)
        {
            var blogvalue = bm.GetByIDT(id);
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;
            if (blogvalue != null)
            {
                if (blogvalue.UserID == writerID)
                {
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
                else
                {
                    return RedirectToAction("BlogListByWriter", "Blog");
                }
            }
            else
            {
                return RedirectToAction("BlogListByWriter", "Blog");
            }

        }
        [HttpPost, Authorize(Roles = "Writer")]
        [ValidateAntiForgeryToken]
        public IActionResult EditBlog(AddBlogImage p)
        {
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;
            var w = bm.GetByIDT(p.BlogID);
            w.BlogShortContent = p.BlogShortContent;
            w.BlogContent = p.BlogContent;
            w.BlogTitle = p.BlogTitle;
            w.CategoryID = p.CategoryID;
            w.BlogStatus = false;
            w.UserID = writerID;
            BlogValidation bv = new BlogValidation();
            ValidationResult result = bv.Validate(w);
            List<SelectListItem> CategoryValue = (from x in cm.GetListT()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.cv = CategoryValue;
            if (result.IsValid)
            {
                if (p.BlogImage != null)
                {
                    var extension = Path.GetExtension(p.BlogImage.FileName);
                    if (extension == ".png")
                    {

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
                    var extensionThumbnail = Path.GetExtension(p.BlogThumbnailImage.FileName);
                    if (extensionThumbnail == ".png")
                    {



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
                        TempData["AlertMessageAdd"] = "Sadece .png uzantılı resimler kabul edilir.";
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
