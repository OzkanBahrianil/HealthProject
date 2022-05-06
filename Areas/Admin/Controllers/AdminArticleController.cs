using BusinessLayer.Concrete;
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

namespace HealthProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class AdminArticleController : Controller
    {
        ArticlesManeger atm = new ArticlesManeger(new EfArticlesDal());
        WriterManeger wm = new WriterManeger(new EfWriterDal());
        ArticleCategoryManeger acm = new ArticleCategoryManeger(new EfArticleCategoryDal());
        public IActionResult Index(string SearchString, int page = 1)
        {
            ViewData["CurrentFilter"] = SearchString;
            if (!String.IsNullOrEmpty(SearchString))
            {
                var valuesSearch = atm.Search(SearchString).OrderByDescending(x => x.ArticlesStatus).ToPagedList(page, 9);
                return View(valuesSearch);
            }
            else
            {
                var values = atm.GetArticlesListWithArticlesCategoryAdmin().OrderByDescending(x => x.ArticlesStatus).ToPagedList(page, 9);
                return View(values);
            }



        }

        public IActionResult ArticlesReadAll(int id)
        {

            var values = atm.TGetByFilterAdmin(x => x.ArticlesID == id);
            return View(values);
        }

        [HttpGet]
        public IActionResult DisableArticle(int id)
        {

            var values = atm.GetByIDT(id);
            values.ArticlesStatus = false;
            atm.TUpdate(values);
            TempData["AletrMessage"] = "Deactive İşlemi Başarılı...!" + id;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EnableArticle(int id)
        {

            var values = atm.GetByIDT(id);
            values.ArticlesStatus = true;
            atm.TUpdate(values);
            TempData["AletrMessage"] = "active İşlemi Başarılı...!" + id;
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult EditArticle(int id)
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
        public IActionResult EditArticle(AddArticlesPdf p)
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
            ViewBag.cv = CategoryValue;


            ArticleValidation bv = new ArticleValidation();
            ValidationResult result = bv.Validate(w);
            if (result.IsValid)
            {
                if (p.ArticlesPdf != null)
                {
                    var extension = Path.GetFileName(p.ArticlesPdf.FileName);
                    if (extension == ".pdf")
                    {



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
                return RedirectToAction("Index");

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
        public IActionResult DeleteArticle(int id)
        {
            var blogvalue = atm.GetByIDT(id);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ArticlesPdf/", blogvalue.ArticlesPdf);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            atm.TDelete(blogvalue);
            TempData["AletrMessage"] = "Silme İşlemi Başarılı...!";
            return RedirectToAction("Index");

        }
    }
}
