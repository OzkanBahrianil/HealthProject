using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.Controllers
{
    [AllowAnonymous]
    public class SearchController : Controller
    {
        BlogManeger bm = new BlogManeger(new EfBlogDal());
        public IActionResult Search(string Search, int page = 1)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                ViewBag.Search = Search;
                var SearchValues = bm.TGetByFilter(x => x.BlogTitle.Contains(Search)).OrderByDescending(x => x.BlogCreateDate).ToList().ToPagedList(page, 9);
                return View(SearchValues);
            }
            else
            {
                var Values = bm.GetBlogListWithCategoryWithComments().OrderByDescending(x => x.BlogCreateDate).ToList().ToPagedList(page, 9);
                return View(Values);
            }


        }
    }
}
