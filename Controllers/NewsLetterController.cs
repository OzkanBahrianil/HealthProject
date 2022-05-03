using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HealthProject.Controllers
{
    [AllowAnonymous]
    public class NewsLetterController : Controller
    {

        NewsLetterManeger nlm = new NewsLetterManeger(new EfNewsLetterDal());

        [HttpGet]
        public PartialViewResult SubscribeMail()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult SubscribeMail(NewsLetter p)
        {
            NewsLetterValidation bv = new NewsLetterValidation();
            ValidationResult result = bv.Validate(p);
            if (result.IsValid)
            {
                p.MailStatus = false;
                nlm.TAdd(p);
                TempData["AlertSubscribe"] = "Abone Olma İşlemi Başarılı.! Lütfen E posta Adresinize gelen mail ile e-posta adresinizi doğrulayınız.";

                var getıdnews = nlm.GetListT().Where(x => x.Mail == p.Mail).FirstOrDefault();
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.To.Add(p.Mail);
                mail.From = new MailAddress("healthprojectblog@gmail.com", "Aylık Haber Bülten", Encoding.UTF8);
                mail.Subject = "Aylık Haber Bülteni (HealthProject)";
                mail.Body = $" <a target=\"_blank\" href=\"https://localhost:44380{Url.Action("SubscribeMailCheck", "NewsLetter", new { userId = getıdnews.MailID })}\">Mail Bülteni Aboneliği İçin Lütfen Mail Adresinizi Doğrulayınız.</a>";
                mail.IsBodyHtml = true;
                SmtpClient smp = new SmtpClient();
                smp.Credentials = new NetworkCredential("healthprojectblog@gmail.com", "11051998Fb.");
                smp.Port = 587;
                smp.Host = "smtp.gmail.com";
                smp.EnableSsl = true;
                smp.Send(mail);

                return RedirectToAction("Index", "Blog");

            }
            else
            {
                foreach (var item in result.Errors)
                {
                    TempData["AlertSubscribeError"] = item.ErrorMessage;
                }
            }
            return RedirectToAction("Index", "Blog");
        }

        [HttpGet("[action]/{userId}")]
        public IActionResult SubscribeMailCheck(int userId)
        {
            var values = nlm.GetByIDT(userId);
            if (values != null)
            {
                values.MailStatus = true;
                nlm.TUpdate(values);
                TempData["AlertSubscribe"] = "E-mail Başarı İle Doğrulandı.";
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                TempData["AlertSubscribe"] = "Hata.!";
                return RedirectToAction("Index", "Blog");
            }

        }



        [HttpGet("[action]/{userId}")]
        public IActionResult SubscribeMailDeactive(int userId)
        {
            var values = nlm.GetByIDT(userId);
            if (values != null)
            {
                values.MailStatus = false;
                nlm.TUpdate(values);
                TempData["AlertSubscribe"] = "Abonelikten başarı ile çıkıldı.";
                return RedirectToAction("Index", "Blog");

            }
            else
            {
                TempData["AlertSubscribe"] = "Hata.!";
                return RedirectToAction("Index", "Blog");
            }
        }
        public IActionResult SubscribeMailDeactiveByUser()
        {

            var usermail = User.Identity.Name;
            var values = nlm.GetListT().Where(x => x.Mail == usermail).FirstOrDefault();
            if (values != null)
            {
                values.MailStatus = false;
                nlm.TUpdate(values);
                TempData["AlertMessage"] = "Abonelikten başarı ile çıkıldı.";
                return RedirectToAction("Index", "Dashboard");

            }
            else
            {
                TempData["AlertMessage"] = "Hata.!";
                return RedirectToAction("Index", "Dashboard");
            }

        }

    }



}

