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
{   [AllowAnonymous]
    public class NewsLetterController : Controller
    {
        
        NewsLetterManeger nlm = new NewsLetterManeger(new EfNewsLetterDal());
        [HttpGet]
        public PartialViewResult SubscribeMail()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult SubscribeMail(NewsLetter p)
        {
            NewsLetterValidation bv = new NewsLetterValidation();
            ValidationResult result = bv.Validate(p);
            if (result.IsValid)
            {
                p.MailStatus = true;
                nlm.TAdd(p);
                TempData["AlertSubscribe"] = "Abone Olma İşlemi Başarılı...!";
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    TempData["AlertSubscribeError"] = item.ErrorMessage;
                }
            }
            return RedirectToAction("Index", "Blog");
        }


    }
}
