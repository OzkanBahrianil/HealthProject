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
using X.PagedList;

namespace HealthProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class CategoryController : Controller
    {
        CategoryManeger cm = new CategoryManeger(new EfCategoryDal());
        ProductCategoryManeger pcm = new ProductCategoryManeger(new EfProductCategoryDal());
        ArticleCategoryManeger acm = new ArticleCategoryManeger(new EfArticleCategoryDal());
        public IActionResult Index(string sortOrder, string SearchString, int page = 1)
        {
            ViewData["CurrentFilterSearch"] = SearchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = sortOrder == "Name" ? "NameDesc" : "Name";
            ViewData["AboutSortParam"] = sortOrder == "About" ? "AboutDesc" : "About";


            if (!String.IsNullOrEmpty(SearchString))
            {
                var valuesSearch = cm.Search(SearchString);
                switch (sortOrder)
                {
                    case "About":
                        valuesSearch = valuesSearch.OrderBy(x => x.CategoryDescription).ToList();
                        break;
                    case "AboutDesc":
                        valuesSearch = valuesSearch.OrderByDescending(x => x.CategoryDescription).ToList();
                        break;
                    case "Name":
                        valuesSearch = valuesSearch.OrderBy(s => s.CategoryName).ToList();
                        break;
                    case "NameDesc":
                        valuesSearch = valuesSearch.OrderByDescending(s => s.CategoryName).ToList();
                        break;
                    default:
                        valuesSearch = valuesSearch.OrderByDescending(s => s.CategoryID).ToList();
                        break;
                }
                return View(valuesSearch.ToPagedList(page, 10));
            }
            else
            {
                var values = cm.GetListTAdmin();
                switch (sortOrder)
                {
                    case "About":
                        values = values.OrderBy(x => x.CategoryDescription).ToList();
                        break;
                    case "AboutDesc":
                        values = values.OrderByDescending(x => x.CategoryDescription).ToList();
                        break;
                    case "Name":
                        values = values.OrderBy(s => s.CategoryName).ToList();
                        break;
                    case "NameDesc":
                        values = values.OrderByDescending(s => s.CategoryName).ToList();
                        break;
                    default:
                        values = values.OrderByDescending(s => s.CategoryID).ToList();
                        break;
                }
                return View(values.ToPagedList(page, 10));
            }

        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category p)
        {
            CategoryValidation cv = new CategoryValidation();
            ValidationResult result = cv.Validate(p);
            if (result.IsValid)
            {
                p.CategoryStatus = true;
                cm.TAdd(p);
                return RedirectToAction("Index", "Category");

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
        [HttpGet]
        public IActionResult CategoryEnable(int id)
        {
            var values = cm.GetByIDT(id);
            values.CategoryStatus = true;
            cm.TUpdate(values);
            return RedirectToAction("Index", "Category");
        }
        [HttpGet]
        public IActionResult CategoryDisable(int id)
        {
            var values = cm.GetByIDT(id);
            values.CategoryStatus = false;
            cm.TUpdate(values);
            return RedirectToAction("Index", "Category");
        }
        [HttpGet]
        public IActionResult CategoryUpdate(int id)
        {
            var values = cm.GetByIDT(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult CategoryUpdate(Category p)
        {


            CategoryValidation cv = new CategoryValidation();
            ValidationResult result = cv.Validate(p);
            if (result.IsValid)
            {
                p.CategoryStatus = true;
                cm.TUpdate(p);
                return RedirectToAction("Index", "Category");

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



        public IActionResult MedicalCategori(string sortOrder, string SearchString, int page = 1)
        {
            ViewData["CurrentFilterSearch"] = SearchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = sortOrder == "Name" ? "NameDesc" : "Name";
            ViewData["AboutSortParam"] = sortOrder == "About" ? "AboutDesc" : "About";
            if (!String.IsNullOrEmpty(SearchString))
            {
                var valuesSearch = pcm.Search(SearchString);
                switch (sortOrder)
                {
                    case "About":
                        valuesSearch = valuesSearch.OrderBy(x => x.ProductCategoryDescription).ToList();
                        break;
                    case "AboutDesc":
                        valuesSearch = valuesSearch.OrderByDescending(x => x.ProductCategoryDescription).ToList();
                        break;
                    case "Name":
                        valuesSearch = valuesSearch.OrderBy(s => s.ProductCategoryName).ToList();
                        break;
                    case "NameDesc":
                        valuesSearch = valuesSearch.OrderByDescending(s => s.ProductCategoryName).ToList();
                        break;
                    default:
                        valuesSearch = valuesSearch.OrderByDescending(s => s.ProductCategoryID).ToList();
                        break;
                }
                return View(valuesSearch.ToPagedList(page, 10));
            }
            else
            {
                var values = pcm.GetListTAdmin();
                switch (sortOrder)
                {
                    case "About":
                        values = values.OrderBy(x => x.ProductCategoryDescription).ToList();
                        break;
                    case "AboutDesc":
                        values = values.OrderByDescending(x => x.ProductCategoryDescription).ToList();
                        break;
                    case "Name":
                        values = values.OrderBy(s => s.ProductCategoryName).ToList();
                        break;
                    case "NameDesc":
                        values = values.OrderByDescending(s => s.ProductCategoryName).ToList();
                        break;
                    default:
                        values = values.OrderByDescending(s => s.ProductCategoryID).ToList();
                        break;
                }

                return View(values.ToPagedList(page, 10));
            }


        }
        [HttpGet]
        public IActionResult AddMedicalCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddMedicalCategory(ProductCategory p)
        {
            ProductCategoryValidation cv = new ProductCategoryValidation();
            ValidationResult result = cv.Validate(p);
            if (result.IsValid)
            {
                p.ProductCategoryStatus = true;
                pcm.TAdd(p);
                return RedirectToAction("MedicalCategori", "Category");

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
        [HttpGet]
        public IActionResult MedicalCategoryEnable(int id)
        {
            var values = pcm.GetByIDT(id);
            values.ProductCategoryStatus = true;
            pcm.TUpdate(values);
            return RedirectToAction("MedicalCategori", "Category");
        }
        [HttpGet]
        public IActionResult MedicalCategoryDisable(int id)
        {
            var values = pcm.GetByIDT(id);
            values.ProductCategoryStatus = false;
            pcm.TUpdate(values);
            return RedirectToAction("MedicalCategori", "Category");
        }
        [HttpGet]
        public IActionResult MedicalCategoryUpdate(int id)
        {
            var values = pcm.GetByIDT(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult MedicalCategoryUpdate(ProductCategory p)
        {


            ProductCategoryValidation cv = new ProductCategoryValidation();
            ValidationResult result = cv.Validate(p);
            if (result.IsValid)
            {
                p.ProductCategoryStatus = true;
                pcm.TUpdate(p);
                return RedirectToAction("MedicalCategori", "Category");

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


        public IActionResult ArticleCategori(string sortOrder, string SearchString, int page = 1)
        {
            ViewData["CurrentFilterSearch"] = SearchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = sortOrder == "Name" ? "NameDesc" : "Name";
            ViewData["AboutSortParam"] = sortOrder == "About" ? "AboutDesc" : "About";
            if (!String.IsNullOrEmpty(SearchString))
            {
                var valuesSearch = acm.Search(SearchString);
                switch (sortOrder)
                {
                    case "About":
                        valuesSearch = valuesSearch.OrderBy(x => x.ArticleCategoryDescription).ToList();
                        break;
                    case "AboutDesc":
                        valuesSearch = valuesSearch.OrderByDescending(x => x.ArticleCategoryDescription).ToList();
                        break;
                    case "Name":
                        valuesSearch = valuesSearch.OrderBy(s => s.ArticleCategoryName).ToList();
                        break;
                    case "NameDesc":
                        valuesSearch = valuesSearch.OrderByDescending(s => s.ArticleCategoryName).ToList();
                        break;
                    default:
                        valuesSearch = valuesSearch.OrderByDescending(s => s.ArticleCategoryID).ToList();
                        break;
                }
                return View(valuesSearch.ToPagedList(page, 10));
            }
            else
            {
                var values = acm.GetListTAdmin();
                switch (sortOrder)
                {
                    case "About":
                        values = values.OrderBy(x => x.ArticleCategoryDescription).ToList();
                        break;
                    case "AboutDesc":
                        values = values.OrderByDescending(x => x.ArticleCategoryDescription).ToList();
                        break;
                    case "Name":
                        values = values.OrderBy(s => s.ArticleCategoryName).ToList();
                        break;
                    case "NameDesc":
                        values = values.OrderByDescending(s => s.ArticleCategoryName).ToList();
                        break;
                    default:
                        values = values.OrderByDescending(s => s.ArticleCategoryID).ToList();
                        break;
                }
                return View(values.ToPagedList(page, 10));
            }
        }


        [HttpGet]
        public IActionResult AddArticleCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddArticleCategory(ArticleCategory p)
        {
            ArticleCategoryValidation cv = new ArticleCategoryValidation();
            ValidationResult result = cv.Validate(p);
            if (result.IsValid)
            {
                p.ArticleCategoryStatus = true;
                acm.TAdd(p);
                return RedirectToAction("ArticleCategori", "Category");

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
        [HttpGet]
        public IActionResult ArticleCategoryEnable(int id)
        {
            var values = acm.GetByIDT(id);
            values.ArticleCategoryStatus = true;
            acm.TUpdate(values);
            return RedirectToAction("ArticleCategori", "Category");
        }
        [HttpGet]
        public IActionResult ArticleCategoryDisable(int id)
        {
            var values = acm.GetByIDT(id);
            values.ArticleCategoryStatus = false;
            acm.TUpdate(values);
            return RedirectToAction("ArticleCategori", "Category");
        }
        [HttpGet]
        public IActionResult ArticleCategoryUpdate(int id)
        {
            var values = acm.GetByIDT(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult ArticleCategoryUpdate(ArticleCategory p)
        {


            ArticleCategoryValidation cv = new ArticleCategoryValidation();
            ValidationResult result = cv.Validate(p);
            if (result.IsValid)
            {
                p.ArticleCategoryStatus = true;
                acm.TUpdate(p);
                return RedirectToAction("ArticleCategori", "Category");

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
