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
        public IActionResult Index(int page = 1)
        {
            var values = cm.GetListTAdmin().ToPagedList(page, 10);
            return View(values);
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



        public IActionResult MedicalCategori(int page = 1)
        {
            var values = pcm.GetListTAdmin().ToPagedList(page, 10);
            return View(values);
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


        public IActionResult ArticleCategori(int page = 1)
        {
            var values = acm.GetListTAdmin().ToPagedList(page, 10);
            return View(values);
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
