using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using FluentValidation.Results;
using HealthProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class WriterController : Controller
    {
        private readonly UserManager<AppUser> _userManeger;
        WriterManeger wm = new WriterManeger(new EfWriterDal());

        public WriterController(UserManager<AppUser> userManeger)
        {
            _userManeger = userManeger;
        }
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Index(string sortOrder, string SearchString, int page = 1)
        {
            ViewData["CurrentFilterSearch"] = SearchString;
            ViewData["CurrentSort"] = sortOrder;

            ViewData["NameSortParam"] = sortOrder == "Name" ? "NameDesc" : "Name";
            ViewData["MailSortParam"] = sortOrder == "Mail" ? "MailDesc" : "Mail";
            ViewData["StatusSortParam"] = sortOrder == "Status" ? "StatusDesc" : "Status";
            if (!String.IsNullOrEmpty(SearchString))
            {
                var values = wm.Search(SearchString);
                switch (sortOrder)
                {

                    case "Mail":
                        values = values.OrderBy(x => x.Email).ToList();
                        break;
                    case "MailDesc":
                        values = values.OrderByDescending(x => x.Email).ToList();
                        break;
                    case "Name":
                        values = values.OrderBy(s => s.NameSurname).ToList();
                        break;
                    case "NameDesc":
                        values = values.OrderByDescending(s => s.NameSurname).ToList();
                        break;
                    case "Status":
                        values = values.OrderBy(s => s.Status).ToList();
                        break;
                    case "StatusDesc":
                        values = values.OrderByDescending(s => s.Status).ToList();
                        break;
                    default:
                        values = values.OrderByDescending(s => s.Id).ToList();
                        break;


                }

                return View(values.ToPagedList(page, 9));
            }
            else
            {
                var values = wm.GetListT();
                switch (sortOrder)
                {

                    case "Mail":
                        values = values.OrderBy(x => x.Email).ToList();
                        break;
                    case "MailDesc":
                        values = values.OrderByDescending(x => x.Email).ToList();
                        break;
                    case "Name":
                        values = values.OrderBy(s => s.NameSurname).ToList();
                        break;
                    case "NameDesc":
                        values = values.OrderByDescending(s => s.NameSurname).ToList();
                        break;
                    case "PhoneStatus":
                        values = values.OrderBy(s => s.PhoneNumberConfirmed).ToList();
                        break;
                    case "PhoneStatusDesc":
                        values = values.OrderByDescending(s => s.PhoneNumberConfirmed).ToList();
                        break;
                    case "Status":
                        values = values.OrderBy(s => s.Status).ToList();
                        break;
                    case "StatusDesc":
                        values = values.OrderByDescending(s => s.Status).ToList();
                        break;
                    default:
                        values = values.OrderByDescending(s => s.Id).ToList();
                        break;


                }

                return View(values.ToPagedList(page, 9));
            }


        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public ActionResult WriterVideo(int id)
        {
            var model = wm.GetByIDT(id);
            return PartialView(model);
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public ActionResult WriterApprove(int id)
        {
            var values = wm.GetByIDT(id);
            values.Status = true;
            wm.TUpdate(values);
            TempData["AlertMessage"] = "Aktif Yapma İşlemi Başarılı...!";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public ActionResult WriterDeactive(int id)
        {
            var values = wm.GetByIDT(id);
            values.Status = false;
            wm.TUpdate(values);
            TempData["AlertMessage"] = "Pasif Yapma İşlemi Başarılı...!";
            return RedirectToAction("Index");
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult WriterBan(int id)
        {
            var values = wm.GetByIDT(id);

            _userManeger.DeleteAsync(values);
            TempData["AlertMessage"] = "Banlama İşlemi Başarılı. Kullanıcı Silindi..!";
            return RedirectToAction("Index");
        }


        [HttpGet, Authorize(Roles = "Admin, Moderator")]
        public IActionResult AdminEditProfile()
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

        [HttpPost, Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> AdminEditProfile(AddProfileImage p)
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
                    var extension = Path.GetExtension(p.WriterImage.FileName);
                    if (extension == ".png")
                    {



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
                    return RedirectToAction("Index", "AdminDashboard");
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

        [HttpPost, Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> AdminEditEmail(AddProfileImage profileImage)
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
            return RedirectToAction("AdminEditProfile", "Writer");
        }

        [HttpPost, Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> AdminEditPassword(AddProfileImage profileImage)
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
            return RedirectToAction("AdminEditProfile", "Writer");
        }














        //public IActionResult WriterList()
        //{
        //    var values = cm.GetListT();
        //    var jsonWriters = JsonConvert.SerializeObject(values);
        //    return Json(jsonWriters);
        //}     
        //public IActionResult WriterDelete(int id)
        //{
        //    var values = cm.GetListT().FirstOrDefault(x=>x.Id==id);
        //    cm.TDelete(values);
        //    return Json(values);
        //}     
        //public IActionResult WriterListByID(int WriterId)
        //{

        //    var values = cm.GetByIDT(WriterId);
        //    var jsonWriters = JsonConvert.SerializeObject(values);
        //    return Json(jsonWriters);
        //}

    }
}
