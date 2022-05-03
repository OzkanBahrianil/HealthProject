using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Controllers
{[AllowAnonymous]
    public class PresentationController : Controller
    {
        PresentationManeger pm = new PresentationManeger(new EfPresentationDal());
        
        public IActionResult PresentationRead(int id)
        {
           var values= pm.GetByIDT(id);
            return View(values);
        }
    }
}
