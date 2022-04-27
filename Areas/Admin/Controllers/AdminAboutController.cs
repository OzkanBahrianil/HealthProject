using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminAboutController : Controller
    {
        AboutManeger abm = new AboutManeger(new EfAboutDal());
        public IActionResult Index()
        {
            var values = abm.GetListT();
            return View(values);
        }
    }
}
