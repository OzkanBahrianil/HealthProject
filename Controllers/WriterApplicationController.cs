using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
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
    public class WriterApplicationController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        WriterApplicationManeger wap = new WriterApplicationManeger(new EfWriterApplicationDal());
        WriterManeger wm = new WriterManeger(new EfWriterDal());

        public WriterApplicationController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;
            var values = wap.GetByIDUser(writerID);
            if (values.FirstOrDefault() != null)
            {
                ViewBag.count = values.Count;
                return View(values);
            }
            else
            {
                return RedirectToAction("WriterApplicationSend", "WriterApplication");
            }


        }
        [HttpGet]
        public IActionResult WriterApplicationSend()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WriterApplicationSend(WriterApplicationPdf p)
        {
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;
            WriterApplication w = new WriterApplication();
            w.ApplicationCoverLetter = p.ApplicationCoverLetter;
            w.ApplicationUniversity = p.ApplicationUniversity;
            w.ApplicationUniversityDepartment = p.ApplicationUniversityDepartment;
            w.ApplicationStatus = true;
            w.ApplicationDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            w.UserID = writerID;

            WriterApplicationValidation bv = new WriterApplicationValidation();
            ValidationResult result = bv.Validate(w);

            if (result.IsValid)
            {
                if (p.ApplicationCV != null)
                {
                    var extension = Path.GetExtension(p.ApplicationCV.FileName);
                    if (extension == ".pdf")
                    {
                        var newImageName = Guid.NewGuid() + extension;
                        var Location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterApplicationPdf/", newImageName);
                        var stream = new FileStream(Location, FileMode.Create);
                        p.ApplicationCV.CopyTo(stream);
                        w.ApplicationCV = newImageName;
                    }
                    else
                    {
                        TempData["AlertMessageAdd"] = "Sadece .pdf kabul edilir.";
                    }



                }


                if (w.ApplicationCV != null)
                {

                    if (w.ApplicationCV.Contains(".pdf"))
                    {
                        wap.TAdd(w);

                        if (p.PhoneNumber != null)
                        {
                            var user = await _userManager.FindByIdAsync(writerID.ToString());
                            user.PhoneNumber = p.PhoneNumber;
                            await _userManager.UpdateAsync(user);
                            await _userManager.UpdateSecurityStampAsync(user);
                        }
                        TempData["AletrMessage"] = "Başvuru İşlemi Başarılı...!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["AlertMessageAdd"] = "Sadece .pdf kabul edilir.";


                    }
                }
                else
                {
                    TempData["AlertMessageAdd"] = "Sadece .pdf kabul edilir.";


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
    }
}
