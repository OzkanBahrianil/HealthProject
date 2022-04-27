﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Notification
{
    public class MessageNotificationAdminHeader : ViewComponent
    {
        WriterManeger wm = new WriterManeger(new EfWriterDal());
        MessageManeger mm = new MessageManeger(new EfMessageDal());
        public IViewComponentResult Invoke()
        {
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;
            var values = mm.GetInboxLinstByWriter(writerID).OrderByDescending(x=>x.MessageDate).Take(3).ToList();
            @ViewBag.NewMessageCount = mm.GetInboxLinstByWriter(writerID).Where(x => x.MessageStatus == true).ToList().Count();
            return View(values);
        }
    }
}
