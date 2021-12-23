using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.Controllers
{
    public class NotificationController : Controller
    {
        NotificationManeger nm = new NotificationManeger(new EfNotificationDal());
        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult AllNotification(int page = 1)
        {
            var values = nm.GetListT().OrderByDescending(x=>x.NotificationID).ToPagedList(page, 20);
            return View(values);
        }
    }
}
