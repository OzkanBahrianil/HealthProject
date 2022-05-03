using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using FluentValidation.Results;
using HealthProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        private readonly UserManager<AppUser> _userManeger;

        public WriterController(UserManager<AppUser> userManeger)
        {
            _userManeger = userManeger;
        }

        public IActionResult Index()
        {
            var usermail = User.Identity.Name;
            ViewBag.v = usermail;
            var writerName = wm.TGetByFilter(x => x.Email == usermail);
            ViewBag.WriterName = writerName.NameSurname;
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
            var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;
            var writervalues = wm.GetByIDT(writerID);
            AddProfileImage addProfileImage = new AddProfileImage();
            addProfileImage.WriterId = writervalues.Id;
            addProfileImage.WriterAbout = writervalues.About;
            addProfileImage.WriterImageString = writervalues.Image;
            addProfileImage.WriterVideoUrl = writervalues.VideoUrl;
            addProfileImage.WriterPassword = writervalues.PasswordHash;
            addProfileImage.WriterMail = writervalues.Email;
            addProfileImage.WriterName = writervalues.NameSurname;

            return View(addProfileImage);
        }

        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(AddProfileImage p)
        {
            var usermail = User.Identity.Name;
            var w = await _userManeger.FindByEmailAsync(usermail);
            w.About = p.WriterAbout;
            w.VideoUrl = p.WriterVideoUrl;

            w.NameSurname = p.WriterName;
            WriterValidation writerValidation = new WriterValidation();
            ValidationResult result = writerValidation.Validate(w);
            if (result.IsValid)
            {
                if (p.WriterImage != null)
                {
                    if (p.WriterImage.FileName.Contains(".png"))
                    {


                        var extension = Path.GetExtension(p.WriterImage.FileName);
                        var newImageName = Guid.NewGuid() + extension;
                        var Location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/", newImageName);
                        var stream = new FileStream(Location, FileMode.Create);
                        p.WriterImage.CopyTo(stream);
                        w.Image = newImageName;

                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/", p.WriterImageString);
                        if (System.IO.File.Exists(path) && p.WriterImageString != "User.png")
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    else
                    {
                        TempData["AlertMessage"] = "Sadece .png uzantılı resimler kabul edilir.";
                        return View();
                    }

                }
                else
                {
                    w.Image = p.WriterImageString;

                }


                if (w.Image.Contains(".png"))
                {
                    w.Status = false;
                    var resultasyc = await _userManeger.UpdateAsync(w);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    TempData["AlertMessage"] = "Sadece .png uzantılı resimler kabul edilir.";

                }
                return View();

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
        public async Task<IActionResult> WriterEditEmail(AddProfileImage profileImage)
        {

            var usermail = User.Identity.Name;
            var p = await _userManeger.FindByEmailAsync(usermail);



            p.Email = profileImage.WriterMail;
            p.UserName = profileImage.WriterMail;
       

            WriterValidationEmailChange writerValidation = new WriterValidationEmailChange();
            ValidationResult result = writerValidation.Validate(p);
            if (result.IsValid)
            {

                var resultasyc = await _userManeger.UpdateAsync(p);
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
        public async Task<IActionResult> WriterEditPassword(AddProfileImage profileImage)
        {

            var usermail = User.Identity.Name;
            var p = await _userManeger.FindByEmailAsync(usermail);

            p.PasswordHash = profileImage.WriterPassword;

            WriterValidationPasswordChange writerValidation = new WriterValidationPasswordChange();
            ValidationResult result = writerValidation.Validate(p);
            if (result.IsValid)
            {
                p.PasswordHash = _userManeger.PasswordHasher.HashPassword(p, profileImage.WriterPassword);

                var resultasyc = await _userManeger.UpdateAsync(p);
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


    }
}
