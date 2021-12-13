using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Controllers
{  [AllowAnonymous]
    public class MessageController : Controller
    {
        MessageManeger mm = new MessageManeger(new EfMessageDal());
        public IActionResult InBox()
        {
            int id = 2;
            var values = mm.GetInboxLinstByWriter(id);
            return View(values);
        }
        
        public IActionResult MessageDetails(int id)
        {
            var value = mm.GetByIDT(id);
            return View(value);
        }
    }
}
