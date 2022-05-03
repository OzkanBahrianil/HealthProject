using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using HealthProject.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class SubscribeController : Controller
    {
        NewsLetterManeger nlm = new NewsLetterManeger(new EfNewsLetterDal());
        public IActionResult Index(int page = 1)
        {
          var values = nlm.GetListT().OrderByDescending(x=>x.MailStatus).ToPagedList(page, 9);
            return View(values);
        }
        [HttpPost]
        public IActionResult MailSend(NewsLetterMail p)
        {


            var getmail = nlm.GetListT().Where(x => x.MailStatus == true).ToList();
            foreach (var item in getmail)
            {

                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.To.Add(item.Mail);
                mail.From = new MailAddress("healthprojectblog@gmail.com", "Aylık Haber Bülten", Encoding.UTF8);
                mail.Subject = "Aylık Haber Bülteni (HealthProject)";
                mail.Body = $"{p.MailContents}" + $" <a target=\"_blank\" href=\"https://localhost:44380{Url.Action("SubscribeMailDeactive", "NewsLetter", new { userId = item.MailID })}\">Aboneliği İptal Edin veya Eğer kayıtlı isteniz ayarlardan aboneliği kapatabilirsiniz.</a>";
                mail.IsBodyHtml = true;
                SmtpClient smp = new SmtpClient();
                smp.Credentials = new NetworkCredential("healthprojectblog@gmail.com", "11051998Fb.");
                smp.Port = 587;
                smp.Host = "smtp.gmail.com";
                smp.EnableSsl = true;
                smp.Send(mail);
                
            }
            TempData["AletrMessage"] = "Mail Gönderme İşlemi Başarılı...!";
            return RedirectToAction("Index");
            
        }
    }
}
