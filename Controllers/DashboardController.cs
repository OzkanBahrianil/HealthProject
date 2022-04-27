using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;
            ViewBag.v1 = c.Blogs.Count().ToString();
            ViewBag.v2 = c.Blogs.Where(x => x.UserID == writerID).Count().ToString();
            ViewBag.v3 = c.Categories.Count().ToString();   
            
            ViewBag.v4 = c.MedicalProducts.Count().ToString();
            ViewBag.v5 = c.MedicalProducts.Where(x => x.UserID == writerID).Count().ToString();
            ViewBag.v6 = c.ProductCategory.Count().ToString();        
            
            
            ViewBag.v7 = c.Articles.Count().ToString();
            ViewBag.v8 = c.Articles.Where(x => x.UserID == writerID).Count().ToString();
            ViewBag.v9 = c.ArticleCategories.Count().ToString(); 
            return View();
        }
    }
}
