using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Writer
{
    public class UserWelcome : ViewComponent
    {
        

        WriterManeger wm = new WriterManeger(new EfWriterDal());
         
    

        public IViewComponentResult Invoke()
        { 
            var usermail = User.Identity.Name;
            if (usermail !=null)
            {
               
                var writerID = wm.TGetByFilter(x => x.Email == usermail).NameSurname;
                ViewBag.LoginName = writerID;
            }
     
          
            return View();
        }
    }
}
