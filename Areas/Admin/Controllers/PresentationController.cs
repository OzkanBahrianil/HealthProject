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
    [Authorize(Roles = "Admin,Moderator")]
    public class PresentationController : Controller
    {
        PresentationManeger pm = new PresentationManeger(new EfPresentationDal());

        public IActionResult Index()
        {

            var values = pm.GetListT();
            return View(values);
        }
        [HttpGet]
        public IActionResult PresentationAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PresentationAdd(AddPresentationImage p)
        {
            Presentation w = new Presentation();
            w.PresentationDetails = p.PresentationDetails;
            w.PresentationTitle = p.PresentationTitle;
            w.PresentationShortDetails = p.PresentationShortDetails;
            w.PresentationStatus = false;
            PresentationValidation bv = new PresentationValidation();
            ValidationResult result = bv.Validate(w);
            if (result.IsValid)
            {
                if (p.PresentationImage != null)
                {
                    var extension = Path.GetExtension(p.PresentationImage.FileName);
                    if (extension == ".png")
                    {

                        var newImageName = Guid.NewGuid() + extension;
                        var Location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PresentationImageFiles/", newImageName);
                        var stream = new FileStream(Location, FileMode.Create);
                        p.PresentationImage.CopyTo(stream);
                        w.PresentationImage = newImageName;
                    }
                    else
                    {
                        TempData["AlertMessageAdd"] = "Sadece .png uzantılı resimler kabul edilir.";
                    }


                }


                if (w.PresentationImage != null)
                {

                    if (w.PresentationImage.Contains(".png"))
                    {
                        pm.TAdd(w);
                        TempData["AletrMessage"] = "Ekleme İşlemi Başarılı...!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["AlertMessageAdd"] = "Sadece .png uzantılı resimler kabul edilir.";

                    }
                }
                else
                {
                    TempData["AlertMessageAdd"] = "Sadece .png uzantılı resimler kabul edilir.";


                }
                return View();

            }
            else
            {
                foreach (var item in result.Errors)
                {
                    TempData["AlertMessageAdd"] = item.ErrorMessage;

                }

                return View();
            }

        }

        public IActionResult DeletePresentation(int id)
        {
            var Presentationvalue = pm.GetByIDT(id);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PresentationImageFiles/", Presentationvalue.PresentationImage);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            pm.TDelete(Presentationvalue);
            TempData["AletrMessage"] = "Silme İşlemi Başarılı...!";
            return RedirectToAction("Index");

        }



        [HttpGet]
        public IActionResult EditPresentation(int id)
        {
            var presentationvalue = pm.GetByIDT(id);

            AddPresentationImage addPresentationImage = new AddPresentationImage();
            addPresentationImage.PresentationID = presentationvalue.PresentationID;
            addPresentationImage.PresentationTitle = presentationvalue.PresentationTitle;
            addPresentationImage.PresentationImageString = presentationvalue.PresentationImage;
            addPresentationImage.PresentationDetails = presentationvalue.PresentationDetails;
            addPresentationImage.PresentationShortDetails = presentationvalue.PresentationShortDetails;

            return View(addPresentationImage);
        }
        [HttpPost]
        public IActionResult EditPresentation(AddPresentationImage p)
        {
            Presentation w = new Presentation();
            w.PresentationID = p.PresentationID;
            w.PresentationTitle = p.PresentationTitle;
            w.PresentationDetails = p.PresentationDetails;
            w.PresentationShortDetails = p.PresentationShortDetails;
            w.PresentationStatus = false;

            PresentationValidation bv = new PresentationValidation();
            ValidationResult result = bv.Validate(w);

            if (result.IsValid)
            {
                if (p.PresentationImage != null)
                {
                    var extension = Path.GetExtension(p.PresentationImage.FileName);
                    if (extension == ".png")
                    {

                        var newImageName = Guid.NewGuid() + extension;
                        var Location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PresentationImageFiles/", newImageName);
                        var stream = new FileStream(Location, FileMode.Create);
                        p.PresentationImage.CopyTo(stream);
                        w.PresentationImage = newImageName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/PresentationImageFiles/", p.PresentationImageString);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    else
                    {
                        TempData["AlertMessageAdd"] = "Sadece .png uzantılı resimler kabul edilir.";
                        return View();
                    }

                }
                else
                {
                    w.PresentationImage = p.PresentationImageString;

                }

                pm.TUpdate(w);
                TempData["AletrMessage"] = "Güncelleme İşlemi Başarılı...!";
                return RedirectToAction("Index");


            }
            else
            {
                foreach (var item in result.Errors)
                {
                    TempData["AlertMessageAdd"] = item.ErrorMessage;

                }

                return View();
            }


        }
        [HttpGet]
        public IActionResult DisablePresentation(int id)
        {

            var values = pm.GetByIDT(id);
            values.PresentationStatus = false;
            pm.TUpdate(values);
            TempData["AletrMessage"] = "Deactive İşlemi Başarılı...!";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EnablePresentation(int id)
        {

            var values = pm.GetByIDT(id);
            values.PresentationStatus = true;
            pm.TUpdate(values);
            TempData["AletrMessage"] = "active İşlemi Başarılı...!";
            return RedirectToAction("Index");
        }
    }
}
