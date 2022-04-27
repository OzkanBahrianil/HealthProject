using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Writer
{
    public class WriterNavbarPartial : ViewComponent
    {
        WriterManeger wm = new WriterManeger(new EfWriterDal());
        public IViewComponentResult Invoke()
        {
            var usermail = User.Identity.Name;
            var WriterName = wm.TGetByFilter(x => x.Email == usermail);
            ViewBag.WriterName = WriterName.NameSurname;
            ViewBag.WriterImage = WriterName.Image;
            return View();
        }
    }
}
