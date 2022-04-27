using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using FluentValidation.Results;
using HealthProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserSignUpViewModel p)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    Email = p.Mail,
                    UserName = p.Mail,
                    NameSurname = p.NameSurname,
                    Image = "User.png",
                    Status = true,
                    About = "-"
                };

                var result = await _userManager.CreateAsync(user, p.Password);
                if (result.Succeeded)
                {

                    var resetToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(resetToken);
                    var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
                    var token1 = codeEncoded.Substring(0, 25);
                    var token2 = codeEncoded.Substring(25);

                    MailMessage mail = new MailMessage();
                    mail.IsBodyHtml = true;
                    mail.To.Add(user.Email);
                    mail.From = new MailAddress("healthprojectblog@gmail.com", "Email Doğrulama", Encoding.UTF8);
                    mail.Subject = "Email Doğrulama Talebi";
                    mail.Body = $"<a target=\"_blank\" href=\"https://localhost:44380{Url.Action("Verify", "Login", new { userId = user.Id, token = token1, token2 = token2 })}\">Email Doğrulamak İçin Lütfen Tıklayınız.</a>";
                    mail.IsBodyHtml = true;
                    SmtpClient smp = new SmtpClient();
                    smp.Credentials = new NetworkCredential("healthprojectblog@gmail.com", "11051998Fb.");
                    smp.Port = 587;
                    smp.Host = "smtp.gmail.com";
                    smp.EnableSsl = true;
                    smp.Send(mail);
                    await _userManager.AddToRoleAsync(user, "Visitor");
                    TempData["AlertUpdate"] = "Kayıt İşlemi Başarılı. Lütfen Giriş Yapmak İçin Email Adresinizi Doğrulayınız..!";
                    return RedirectToAction("Index", "Login");
                }

                else
                {
                   
                       
                        result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                    
                }
            }
            return View(p);
        }
    }
}
