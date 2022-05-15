using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using HealthProject.Filters;
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
    [PageVisitCountFilter]
    public class ArticleController : Controller
    {
        ArticlesManeger atm = new ArticlesManeger(new EfArticlesDal());
        ArticleCategoryManeger acm = new ArticleCategoryManeger(new EfArticleCategoryDal());
        PageVisitManeger pvm = new PageVisitManeger(new EfPageVisitDal());

        public IActionResult Index(int page = 1)
        {
            var values = atm.GetArticlesListWithArticlesCategory().OrderByDescending(x => x.ArticlesID).ToPagedList(page, 9);
            return View(values);
        }
        public IActionResult ArticlesReadAll(int id)
        {
            var Urlstring = HttpContext.Request.Path;
            var viewvalue = pvm.GetByUrl(Urlstring);
            if (viewvalue != null)
            {
                ViewBag.viewvalue = viewvalue.Visits;
            }
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
