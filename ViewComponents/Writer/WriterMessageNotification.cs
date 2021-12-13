﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Writer
{
    public class WriterMessageNotification : ViewComponent
    {

        MessageManeger mm = new MessageManeger(new EfMessageDal());
        public IViewComponentResult Invoke()
        {
            int id = 1;
            var values = mm.GetInboxLinstByWriter(id);
            return View(values);
        }
    }
}
