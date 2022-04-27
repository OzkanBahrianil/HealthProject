using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
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
    public class ContactController : Controller
    {
        ContactManeger cm = new ContactManeger(new EfContactDal());
        public IActionResult Index(int page = 1)
        {
            var values = cm.GetListT().OrderByDescending(x => x.ContactStatus).ToPagedList(page, 20);
            return View(values);
        }
        [HttpGet]
        public IActionResult ReadContact(int id)
        {
            var values = cm.GetByIDT(id);
            values.ContactStatus = false;
            cm.TUpdate(values);
            TempData["AlertMessage"] = "Okuma İşlemi Başarılı...!";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult DeleteContact(int id)
        {
            var values = cm.GetByIDT(id);
            cm.TDelete(values);
            TempData["AlertMessage"] = "Silme İşlemi Başarılı...!";
            return RedirectToAction("Index");
        }
    }
}
