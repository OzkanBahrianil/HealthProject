using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.Areas.Article.Controllers
{
    [AllowAnonymous,Area("Article")]
   
    public class ArticleController : Controller
    {
        ArticlesManeger atm = new ArticlesManeger(new EfArticlesDal());
        ArticleCategoryManeger acm = new ArticleCategoryManeger(new EfArticleCategoryDal());


        public IActionResult Index(int page = 1)
        {
            var values = atm.GetArticlesListWithArticlesCategory().OrderByDescending(x => x.ArticlesID).ToPagedList(page, 9);
            return View(values);
        }
        public IActionResult ArticlesReadAll(int id)
        {

            var values = atm.TGetByFilter(x => x.ArticlesID == id);
            return View(values);
        }
        public IActionResult ArticlesListByCategory(int id, int page = 1)
        {
            ViewBag.CategoryName = acm.GetByIDT(id).ArticleCategoryName;
            var values = atm.TGetByFilter(x => x.ArticleCategoryID == id).OrderByDescending(x => x.ArticlesID).ToPagedList(page, 9);
            return View(values);
        }
    }
}
