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

namespace HealthProject.Areas.MedicalProducts.Controllers
{
    [Area("MedicalProducts"),AllowAnonymous]
    public class CommentProductController : Controller
    {
        CommentProductManeger cm = new CommentProductManeger(new EfCommentProductDal());

        [HttpGet]
        public PartialViewResult PartialAddCommentProduct()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult PartialAddCommentProduct(CommentProduct p)
        {
            int id = p.ProductID;
            CommentProductValidation bv = new CommentProductValidation();
            ValidationResult result = bv.Validate(p);
            if (result.IsValid)
            {
                p.CommentProductDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.CommentProductStatus = false;
                cm.TAdd(p);
                TempData["AlertMessage"] = "Yorum Ekleme Başarılı. Onay Verildikten Sonra Yorum'u Görebilirsiniz.!!!";
                return RedirectToAction("ProductReadAll", "Home", new { @id = id });
            }
            else
            {

                foreach (var item in result.Errors)
                {
                    TempData["AletrMessage"] = item.ErrorMessage;
                }
            }
            return RedirectToAction("ProductReadAll", "Home", new { @id = id });




        }
    }
}
