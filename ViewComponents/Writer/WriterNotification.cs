﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Writer
{
    public class WriterNotification : ViewComponent
    {
        NotificationManeger nm = new NotificationManeger(new EfNotificationDal());
        public IViewComponentResult Invoke()
        {
           
            var values = nm.GetListT();
            return View(values);
        }
    }
}
