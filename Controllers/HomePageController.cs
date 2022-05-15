using HealthProject.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Controllers
{
    public class HomePageController : Controller
    {
        [AllowAnonymous]
        [PageVisitCountFilter]
        public IActionResult Index(int pageblog = 1, int pagearticle = 1, int pageproduct = 1)
        {
            ViewBag.pageblog = pageblog;
            ViewBag.pagearticle = pagearticle;
            ViewBag.pageproduct = pageproduct;
            return View();
        }
    }
}
