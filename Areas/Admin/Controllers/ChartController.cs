using HealthProject.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class ChartController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult GetPiechartJSON()
        {
            List<CategoryClass> list = new List<CategoryClass>();

            list.Add(new CategoryClass { categoryname = "Technology", categorycount = 11 });
            list.Add(new CategoryClass { categoryname = "Health", categorycount = 5 });
            list.Add(new CategoryClass { categoryname = "Windows", categorycount = 2 });

            return Json(new { jsonlist = list });
        }
    }
}
