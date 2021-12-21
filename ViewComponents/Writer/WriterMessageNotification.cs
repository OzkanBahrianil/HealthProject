using BusinessLayer.Concrete;
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
        WriterManeger wm = new WriterManeger(new EfWriterDal());
        MessageManeger mm = new MessageManeger(new EfMessageDal());
        public IViewComponentResult Invoke()
        {
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.WriterMail == usermail).WriterId;
            var values = mm.GetInboxLinstByWriter(writerID);
            return View(values);
        }
    }
}
