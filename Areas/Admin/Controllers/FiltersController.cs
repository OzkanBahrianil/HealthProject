using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
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
    public class FiltersController : Controller
    {
        PageVisitManeger pvm = new PageVisitManeger(new EfPageVisitDal());
        public IActionResult Index(string sortOrder, string SearchString, int page =1)
        {
            ViewData["CurrentFilterSearch"] = SearchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["UrlSortParam"] = sortOrder == "Url" ? "UrlDesc" : "Url";
            ViewData["VisitSortParam"] = sortOrder == "Visit" ? "VisitDesc" : "Visit";
            if (!String.IsNullOrEmpty(SearchString))
            {

                var values = pvm.Search(SearchString);
                switch (sortOrder)
                {
                    case "Url":
                        values = values.OrderBy(x => x.PageUrl).ToList();
                        break;
                    case "UrlDesc":
                        values = values.OrderByDescending(x => x.PageUrl).ToList();
                        break;
                    case "Visit":
                        values = values.OrderBy(s => s.Visits).ToList();
                        break;
                    case "VisitDesc":
                        values = values.OrderByDescending(s => s.Visits).ToList();
                        break;
                    default:
                        values = values.OrderByDescending(s => s.PageID).ToList();
                        break;


                }
                return View(values.ToPagedList(page, 12));
            }
            else
            {
                var values = pvm.GetListT();
                switch (sortOrder)
                {
                    case "Url":
                        values = values.OrderBy(x => x.PageUrl).ToList();
                        break;
                    case "UrlDesc":
                        values = values.OrderByDescending(x => x.PageUrl).ToList();
                        break;
                    case "Visit":
                        values = values.OrderBy(s => s.Visits).ToList();
                        break;
                    case "VisitDesc":
                        values = values.OrderByDescending(s => s.Visits).ToList();
                        break;

                    default:
                        values = values.OrderByDescending(s => s.PageID).ToList();
                        break;


                }
                return View(values.ToPagedList(page, 12));
            }
        }
    }
}
