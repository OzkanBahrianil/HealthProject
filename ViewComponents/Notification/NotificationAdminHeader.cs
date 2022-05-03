using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Notification
{
    public class NotificationAdminHeader : ViewComponent
    {
        NotificationManeger nm = new NotificationManeger(new EfNotificationDal());
        public IViewComponentResult Invoke()
        {
            var values = nm.GetListT().Where(x=>x.NotificationStatus==true).Take(3).ToList();
            return View(values);
        }
    }
}
