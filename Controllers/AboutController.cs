using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Controllers
{
    public class AboutController : Controller
    {
        AboutManeger abm = new AboutManeger(new EfAboutDal());
        public IActionResult Index()
        { var values = abm.GetListT();
            return View(values);
        }
        public PartialViewResult SocialMediaAbout()
        {
           
            return PartialView();
        }
    }
}
