using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        WriterManeger wm = new WriterManeger(new EfWriterDal());
        [HttpGet]
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Index(Writer p)
        {
            WriterValidationRegister writerValidation = new WriterValidationRegister();
            ValidationResult result = writerValidation.Validate(p);
            if (result.IsValid)
            {
            p.WriterImage = "User.png";
            p.WriterStatus = true;
            p.WriterAbout = "-";
            wm.TAdd(p);
            return RedirectToAction("Index","Login");

            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName,item.ErrorMessage);
                }
            }
            return View();
          
        }
    }
}
