using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.About
{
    public class AboutFooter : ViewComponent
    {
        AboutManeger abm = new AboutManeger(new EfAboutDal());
        public IViewComponentResult Invoke()
        {
            var values = abm.GetListT();
            return View(values);
        }
    }
}
