using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Controllers
{

    public class DashboardController : Controller
    {
        WriterManeger wm = new WriterManeger(new EfWriterDal());
        public IActionResult Index()
        {
            Context c = new Context();
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.WriterMail == usermail).WriterId;
            ViewBag.v1 = c.Blogs.Count().ToString();
            ViewBag.v2 = c.Blogs.Where(x => x.WriterID == writerID).Count().ToString();
            ViewBag.v3 = c.Categories.Count().ToString(); 
            return View();
        }
    }
}
