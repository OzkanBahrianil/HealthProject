using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using FluentValidation.Results;
using HealthProject.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpGet]
        public IActionResult EditAbout(int id)
        {
            var value = abm.GetByIDT(id);

            AddAboutImage addAboutImage = new AddAboutImage();
            addAboutImage.AboutID = value.AboutID;
            addAboutImage.AboutDetails = value.AboutDetails;
            addAboutImage.AboutImageString = value.AboutImage;
            addAboutImage.AboutMapLocation = value.AboutMapLocation;
            return View(addAboutImage);
        }
        [HttpPost]
        public IActionResult EditAbout(AddAboutImage p)
        {
            About w = new About();
            w.AboutDetails = p.AboutDetails;
            w.AboutID = p.AboutID;
            w.AboutMapLocation = p.AboutMapLocation;


            AboutValidation writerValidation = new AboutValidation();
            ValidationResult result = writerValidation.Validate(w);
            if (result.IsValid)
            {
                if (p.AboutImage != null)
                {
                    var extension = Path.GetExtension(p.AboutImage.FileName);
                    if (extension == ".png")
                    {



                        var newImageName = Guid.NewGuid() + extension;
                        var Location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AboutImageFiles/", newImageName);
                        var stream = new FileStream(Location, FileMode.Create);
                        p.AboutImage.CopyTo(stream);
                        w.AboutImage = newImageName;

                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AboutImageFiles/", p.AboutImageString);
                        if (System.IO.File.Exists(path) && p.AboutImageString != "b5.jpg")
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
                    w.AboutImage = p.AboutImageString;

                }


                if (w.AboutImage.Contains(".png") || w.AboutImage.Contains(".jpg"))
                {
                    w.Status = true;
                    abm.TUpdate(w);
                    return RedirectToAction("Index");
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
    }
}
