﻿using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using FluentValidation.Results;
using HealthProject.Areas.Article.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.Areas.Article.Controllers
{   [Area("Article")]
    [Authorize(Roles = "Writer")]
    public class WriterController : Controller
    {

        ArticleCategoryManeger acm = new ArticleCategoryManeger(new EfArticleCategoryDal());
        ArticlesManeger atm = new ArticlesManeger(new EfArticlesDal());
        WriterManeger wm = new WriterManeger(new EfWriterDal());
      
        public IActionResult ArticlesListByWriter(int page = 1)
        {
            var usermail = User.Identity.Name;
            var CompanyLevel = wm.TGetByFilter(x => x.Email == usermail);
            var WriterID = wm.TGetByFilter(x => x.Email == usermail).Id;
            var values = atm.GetListWithCategoryByWriterbmF(WriterID).ToPagedList(page, 10);
            return View(values);



        }

        [HttpGet]
        public IActionResult ArticlesAddByWriter()
        {
            List<SelectListItem> CategoryValue = (from x in acm.GetListT()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.ArticleCategoryName,
                                                      Value = x.ArticleCategoryID.ToString()
                                                  }).ToList();
            ViewBag.cv = CategoryValue;
            return View();
        }
        [HttpPost]
        public IActionResult ArticlesAddByWriter(AddArticlesPdf p)
        {
            Articles w = new Articles();
            var usermail = User.Identity.Name;
            var WriterID = wm.TGetByFilter(x => x.Email == usermail).Id;

            w.ArticlesShortContent = p.ArticlesShortContent;
            w.ArticlesContent = p.ArticlesContent;
            w.ArticlesTitle = p.ArticlesTitle;
            w.ArticleCategoryID = p.ArticleCategoryID;
            w.ArticlesType = p.ArticlesType;
            w.ArticlesWritersName = p.ArticlesWritersName;
            w.ArticlesStatus = false;
            w.ArticlesPublishDate = p.ArticlesPublishDate;
            w.UserID = WriterID;


            ArticleValidation bv = new ArticleValidation();
            ValidationResult result = bv.Validate(w);
            if (result.IsValid)
            {
                if (p.ArticlesPdf != null && p.ArticlesPdf.FileName.Contains(".pdf"))
                {
                    var extension = Path.GetFileName(p.ArticlesPdf.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var Location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ArticlesPdf/", newImageName);
                    var stream = new FileStream(Location, FileMode.Create);
                    p.ArticlesPdf.CopyTo(stream);
                    w.ArticlesPdf = newImageName;

                }

                if (w.ArticlesPdf.Contains(".pdf"))
                {
                    atm.TAdd(w);
                    TempData["AletrMessage"] = "Ekleme İşlemi Başarılı...!";
                    return RedirectToAction("ArticlesListByWriter", "Writer");
                }
                else
                {
                    TempData["AlertMessageAdd"] = "Sadece .png uzantılı resimler kabul edilir.";
                    List<SelectListItem> CategoryValue = (from x in acm.GetListT()
                                                          select new SelectListItem
                                                          {
                                                              Text = x.ArticleCategoryName,
                                                              Value = x.ArticleCategoryID.ToString()
                                                          }).ToList();
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
                List<SelectListItem> CategoryValue = (from x in acm.GetListT()
                                                      select new SelectListItem
                                                      {
                                                          Text = x.ArticleCategoryName,
                                                          Value = x.ArticleCategoryID.ToString()
                                                      }).ToList();
                ViewBag.cv = CategoryValue;
                return View();
            }

        }
        public IActionResult DeleteArticles(int id)
        {
            var blogvalue = atm.GetByIDT(id);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ArticlesPdf/", blogvalue.ArticlesPdf);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            atm.TDelete(blogvalue);
            TempData["AletrMessage"] = "Silme İşlemi Başarılı...!";
            return RedirectToAction("ArticlesListByWriter");

        }

        [HttpGet]
        public IActionResult EditArticles(int id)
        {
            var value = atm.GetByIDT(id);


            List<SelectListItem> CategoryValue = (from x in acm.GetListT()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.ArticleCategoryName,
                                                      Value = x.ArticleCategoryID.ToString()
                                                  }).ToList();
            ViewBag.cv = CategoryValue;
            AddArticlesPdf addArticlesPdf = new AddArticlesPdf();
            addArticlesPdf.ArticlesTitle = value.ArticlesTitle;
            addArticlesPdf.ArticlesID = value.ArticlesID;
            addArticlesPdf.ArticlesPdfString = value.ArticlesPdf;
            addArticlesPdf.ArticlesPublishDate = value.ArticlesPublishDate;
            addArticlesPdf.ArticlesType = value.ArticlesType;
            addArticlesPdf.ArticlesWritersName = value.ArticlesWritersName;
            addArticlesPdf.ArticlesShortContent = value.ArticlesShortContent;
            addArticlesPdf.ArticlesContent = value.ArticlesContent;
            addArticlesPdf.ArticleCategoryID = value.ArticleCategoryID;
            return View(addArticlesPdf);
        }
        [HttpPost]
        public IActionResult EditArticles(AddArticlesPdf p)
        {
            Articles w = new Articles();
            var usermail = User.Identity.Name;
            var WriterID = wm.TGetByFilter(x => x.Email == usermail).Id;
            List<SelectListItem> CategoryValue = (from x in acm.GetListT()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.ArticleCategoryName,
                                                      Value = x.ArticleCategoryID.ToString()
                                                  }).ToList();
            w.ArticleCategoryID = p.ArticleCategoryID;
            w.ArticlesID = p.ArticlesID;
            w.ArticlesShortContent = p.ArticlesShortContent;
            w.ArticlesContent = p.ArticlesContent;
            w.ArticlesTitle = p.ArticlesTitle;
            w.ArticlesStatus = false;
            w.ArticlesPublishDate = p.ArticlesPublishDate;
            w.ArticlesType = p.ArticlesType; 
            w.ArticlesWritersName = p.ArticlesWritersName;
            w.UserID = WriterID;



            ArticleValidation bv = new ArticleValidation();
            ValidationResult result = bv.Validate(w);
            if (result.IsValid)
            {
                if (p.ArticlesPdf != null)
                {
                    if (p.ArticlesPdf.FileName.Contains(".pdf"))
                    {


                        var extension = Path.GetFileName(p.ArticlesPdf.FileName);
                        var newImageName = Guid.NewGuid() + extension;
                        var Location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ArticlesPdf/", newImageName);
                        var stream = new FileStream(Location, FileMode.Create);
                        p.ArticlesPdf.CopyToAsync(stream);
                        w.ArticlesPdf = newImageName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ArticlesPdf/", p.ArticlesPdfString);
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
                    w.ArticlesPdf = p.ArticlesPdfString;

                }


                atm.TUpdate(w);
                TempData["AletrMessage"] = "Güncelleme İşlemi Başarılı...!";
                return RedirectToAction("ArticlesListByWriter", "Writer");

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
