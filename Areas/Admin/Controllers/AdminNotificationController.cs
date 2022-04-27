using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class AdminNotificationController : Controller
    {
        NotificationManeger nm = new NotificationManeger(new EfNotificationDal());
        public IActionResult Index(int page = 1)
        {
            var values = nm.GetListT().OrderByDescending(x => x.NotificationID).ToPagedList(page, 20);
            return View(values);

        }
        [HttpGet]
        public IActionResult DeleteNotification(int id)
        {
            var values = nm.GetByIDT(id); 
            nm.TDelete(values);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult ActiveNotification(int id)
        {
            var values = nm.GetByIDT(id);
            values.NotificationTime = DateTime.Parse(DateTime.Now.ToShortDateString());
            values.NotificationStatus = true;
            nm.TUpdate(values);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult PasiveNotification(int id)
        {
            var values = nm.GetByIDT(id);
            values.NotificationStatus = false;
            nm.TUpdate(values);
            return RedirectToAction("Index");

        }
      
        [HttpPost]
        public IActionResult AddNotification(Notification p)
        {

            NotificationValidation bv = new NotificationValidation();
            ValidationResult result = bv.Validate(p);
            if (result.IsValid)
            {
                p.NotificationTime = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.NotificationStatus = false;
                nm.TAdd(p);
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    TempData["AlertAdd"] = item.ErrorMessage;
                }

            }
            
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult UpdateNotification(int id)
        {
            var values = nm.GetByIDT(id);
            return View(values);

        }
        [HttpPost]
        public IActionResult UpdateNotification(Notification p)
        {
            NotificationValidation bv = new NotificationValidation();
            ValidationResult result = bv.Validate(p);
            if (result.IsValid)
            {
                p.NotificationTime = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.NotificationStatus = false;
                nm.TUpdate(p);
                TempData["AlertUpdate"] = "Güncelleme İşlemi Başarılı";
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    TempData["AlertUpdate"] = item.ErrorMessage;
                } 
                return View();

            }
          

        }
    }
}
