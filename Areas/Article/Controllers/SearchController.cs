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
    [AllowAnonymous, Area("Article")]
    public class SearchController : Controller
    {
        ArticlesManeger atm = new ArticlesManeger(new EfArticlesDal());
        public IActionResult Search(string Search, int page = 1)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                ViewBag.Search = Search;
                var SearchValues = atm.TGetByFilter(x => x.ArticlesTitle.Contains(Search)).OrderByDescending(x => x.ArticlesID).ToList().ToPagedList(page, 9);
                return View(SearchValues);
            }
            else
            {
                var Values = atm.GetArticlesListWithArticlesCategory().OrderByDescending(x => x.ArticlesID).ToList().ToPagedList(page, 9);
                return View(Values);
            }


        }
    }
}
