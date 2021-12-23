using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using FluentValidation.Results;
using HealthProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Controllers
{ 
    public class WriterController : Controller
    {
        WriterManeger wm = new WriterManeger(new EfWriterDal());
       
        public IActionResult Index()
        {
            var usermail = User.Identity.Name;
            ViewBag.v = usermail;
            var writerName =  wm.TGetByFilter(x => x.WriterMail == usermail);
            ViewBag.WriterName = writerName.WriterName;
            return View();
        }
  
  
     
        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }
      
        [HttpGet]
        public IActionResult WriterEditProfile()
        {
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.WriterMail == usermail).WriterId;
            ViewBag.Password = wm.TGetByFilter(x => x.WriterMail == usermail).WriterPassword.ToString();
            var writervalues = wm.GetByIDT(writerID);
            return View(writervalues);
        }
      
        [HttpPost]
        public IActionResult WriterEditProfile(Writer p)
        {
            WriterValidation writerValidation = new WriterValidation();
            ValidationResult result = writerValidation.Validate(p);
            if (result.IsValid)
            {
                wm.TUpdate(p);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult WriterEditEmail(Writer p)
        {

            WriterValidationEmailChange writerValidation = new WriterValidationEmailChange();
            ValidationResult result = writerValidation.Validate(p);
            if (result.IsValid)
            {
                var usermail = User.Identity.Name;
                var writer = wm.TGetByFilter(x => x.WriterMail == usermail);
                p.WriterAbout = writer.WriterAbout;
                p.WriterImage = writer.WriterImage;
                p.WriterId = writer.WriterId;
                p.WriterName = writer.WriterName;
                p.WriterPassword = writer.WriterPassword;
                p.WriterStatus = true;
                wm.TUpdate(p);
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    TempData["AlertMessage"] = item.ErrorMessage;
                }
            }
            return RedirectToAction("WriterEditProfile", "Writer");
        }

        [HttpPost]
        public IActionResult WriterEditPassword(Writer p)
        {

            WriterValidationPasswordChange writerValidation = new WriterValidationPasswordChange();
            ValidationResult result = writerValidation.Validate(p);
            if (result.IsValid)
            {
                var usermail = User.Identity.Name;
                var writer = wm.TGetByFilter(x => x.WriterMail == usermail);
                p.WriterAbout = writer.WriterAbout;
                p.WriterImage = writer.WriterImage;
                p.WriterId = writer.WriterId;
                p.WriterName = writer.WriterName;
                p.WriterMail = writer.WriterMail;
                p.WriterStatus = true;
                wm.TUpdate(p);
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    TempData["AlertMessage"] = item.ErrorMessage;
                }
            }
            return RedirectToAction("WriterEditProfile", "Writer");
        }




        //Writeradd gereksiz 


        [AllowAnonymous]
        [HttpGet]
        public IActionResult WriterAdd()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult WriterAdd(AddProfileImage p)
        {
            Writer w = new Writer();
            if (p.WriterImage !=null)
            {
                var extension = Path.GetExtension(p.WriterImage.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var Location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/", newImageName);
                var stream = new FileStream(Location, FileMode.Create);
                p.WriterImage.CopyTo(stream);
                w.WriterImage = newImageName;
            }
            w.WriterMail = p.WriterMail;
            w.WriterName = p.WriterName;
            w.WriterPassword = p.WriterPassword;
            w.WriterAbout = p.WriterAbout;
            w.WriterStatus = true;
            wm.TAdd(w);
            return RedirectToAction("Index","Dashboard");
        }
    }
}
