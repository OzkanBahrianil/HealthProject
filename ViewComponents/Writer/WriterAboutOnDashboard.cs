using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Writer
{
    public class WriterAboutOnDashboard:ViewComponent
    {
        WriterManeger wm = new WriterManeger(new EfWriterDal());
        public IViewComponentResult Invoke()
        {
            var values = wm.GetWriterByID(1);
            return View(values);
        }
    }
}
