﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Blog
{
    public class BlogBannerPresentation:ViewComponent
    {

        PresentationManeger pm = new PresentationManeger(new EfPresentationDal()); 
        AboutManeger abm = new AboutManeger(new EfAboutDal());
        public IViewComponentResult Invoke()
        {
            var values = pm.GetListT().Where(x => x.PresentationStatus == true).ToList();
            ViewBag.About = abm.GetListT().FirstOrDefault().AboutDetails;
            return View(values);
        }
    }
}
