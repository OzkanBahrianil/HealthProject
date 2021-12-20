﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
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
            p.MailStatus = true;
            nlm.TAdd(p);
            return RedirectToAction("Index", "Blog");
        }
    }
}
