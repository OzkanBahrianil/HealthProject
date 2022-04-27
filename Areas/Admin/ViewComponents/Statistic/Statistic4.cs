using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic4: ViewComponent
    {
      
        public IViewComponentResult Invoke()
        {
            //ViewBag.ALN = adm.GetByIDT(1).AdminName;
            //ViewBag.ALIMG = adm.GetByIDT(1).AdminImageURL;
            //ViewBag.ALD = adm.GetByIDT(1).AdminShortDescription;
            return View();
        }
    }
}
