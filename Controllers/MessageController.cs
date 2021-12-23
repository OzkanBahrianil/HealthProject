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
    public class MessageController : Controller
    {
        MessageManeger mm = new MessageManeger(new EfMessageDal());
        WriterManeger wm = new WriterManeger(new EfWriterDal());
        public IActionResult InBox(int page = 1)
        {
         
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.WriterMail == usermail).WriterId;
            var values = mm.GetInboxLinstByWriter(writerID).ToPagedList(page, 12);
            return View(values);
        }
        
        public IActionResult MessageDetails(int id)
        {
            var value = mm.GetMessageListById(id);
            return View(value);
        }
    }
}
