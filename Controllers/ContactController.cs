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

namespace HealthProject.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        ContactManeger cm = new ContactManeger(new EfContactDal());
        AboutManeger abm = new AboutManeger(new EfAboutDal());
        [HttpGet]
        public IActionResult Index()
        {
            var values = abm.GetListT().FirstOrDefault();
            return View(values);
        }
        [HttpPost]
        public IActionResult Index(Contact p)
        {
            ContactValidation cv = new ContactValidation();
            ValidationResult result = cv.Validate(p);
            if (result.IsValid)
            {
                p.ContactDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.ContactStatus = true;
                cm.TAdd(p);
                TempData["AlertMessage"] = "Mesajınız Başarı ile Gönderilmiştir...!";
                return View();
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    TempData["AlertMessageFalse"] = item.ErrorMessage;

                }
                return View();
            }

        }
    }
}
