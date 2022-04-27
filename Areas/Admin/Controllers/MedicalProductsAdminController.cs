using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using FluentValidation.Results;
using HealthProject.Areas.MedicalProducts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class MedicalProductsAdminController : Controller
    {
        ProductCategoryManeger pcm = new ProductCategoryManeger(new EfProductCategoryDal());
        MedicalProductManeger mpm = new MedicalProductManeger(new EfMedicalProductDal());
        WriterManeger wm = new WriterManeger(new EfWriterDal());

        public IActionResult Index(int page = 1)
        {
            var values = mpm.GetProductListWithCategoryWithCommentsAdmin().OrderByDescending(x => x.ProductID).ToPagedList(page, 9);
            return View(values);
        }
        public IActionResult ProductReadAll(int id)
        {
     
            ViewBag.i = id;
            var values = mpm.TGetByFilterAdmin(x => x.ProductID == id);
            return View(values);
        }

        [HttpGet]
        public IActionResult DisableProduct(int id)
        {

            var values = mpm.GetByIDT(id);
            values.ProductStatus = false;
            mpm.TUpdate(values);
            TempData["AletrMessage"] = "Deactive İşlemi Başarılı...!" + id;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EnableProduct(int id)
        {

            var values = mpm.GetByIDT(id);
            values.ProductStatus = true;
            mpm.TUpdate(values);
            TempData["AletrMessage"] = "active İşlemi Başarılı...!" + id;
            return RedirectToAction("Index");
        }
        public IActionResult DeleteProduct(int id)
        {
            var blogvalue = mpm.GetByIDT(id);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MedicalProductImageFiles/", blogvalue.ProductImage);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            var path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MedicalProductImageFiles/", blogvalue.ProductThumbnailImage);
            if (System.IO.File.Exists(path2))
            {
                System.IO.File.Delete(path2);
            }
            mpm.TDelete(blogvalue);
            TempData["AletrMessage"] = "Silme İşlemi Başarılı...!";
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult EditProducts(int id)
        {
            var blogvalue = mpm.GetByIDT(id);


            List<SelectListItem> CategoryValue = (from x in pcm.GetListT()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.ProductCategoryName,
                                                      Value = x.ProductCategoryID.ToString()
                                                  }).ToList();
            ViewBag.cv = CategoryValue;
            AddProductImage addProductImage = new AddProductImage();
            addProductImage.ProductID = blogvalue.ProductID;
            addProductImage.ProductTitle = blogvalue.ProductTitle;
            addProductImage.ProductImageString = blogvalue.ProductImage;
            addProductImage.ProductRealiseDate = blogvalue.ProductRealiseDate;
            addProductImage.ProductStyle = blogvalue.ProductStyle;
            addProductImage.ProductThumbnailImageString = blogvalue.ProductThumbnailImage;
            addProductImage.ProductShortContent = blogvalue.ProductShortContent;
            addProductImage.ProductContent = blogvalue.ProductContent;
            addProductImage.ProductCategoryID = blogvalue.ProductCategoryID;
            addProductImage.ProductCompanyWebsite = blogvalue.ProductCompanyWebsite;
            return View(addProductImage);
        }
        [HttpPost]
        public IActionResult EditProducts(AddProductImage p)
        {
            MedicalProduct w = new MedicalProduct();
            var usermail = User.Identity.Name;
            var CompanyID = wm.TGetByFilter(x => x.Email == usermail).Id;
            List<SelectListItem> CategoryValue = (from x in pcm.GetListT()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.ProductCategoryName,
                                                      Value = x.ProductCategoryID.ToString()
                                                  }).ToList();
            w.ProductID = p.ProductID;
            w.ProductShortContent = p.ProductShortContent;
            w.ProductContent = p.ProductContent;
            w.ProductTitle = p.ProductTitle;
            w.ProductCategoryID = p.ProductCategoryID;
            w.ProductStatus = false;
            w.ProductRealiseDate = p.ProductRealiseDate;
            w.ProductStyle = p.ProductStyle;
            w.ProductRealiseDate = p.ProductRealiseDate;
            w.ProductCompanyWebsite = p.ProductCompanyWebsite;
            w.UserID = CompanyID;



            MedicalProductValidation bv = new MedicalProductValidation();
            ValidationResult result = bv.Validate(w);


            if (result.IsValid)
            {
                if (p.ProductImage != null)
                {
                    if (p.ProductImage.FileName.Contains(".png"))
                    {


                        var extension = Path.GetExtension(p.ProductImage.FileName);
                        var newImageName = Guid.NewGuid() + extension;
                        var Location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MedicalProductImageFiles/", newImageName);
                        var stream = new FileStream(Location, FileMode.Create);
                        p.ProductImage.CopyTo(stream);
                        w.ProductImage = newImageName;

                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MedicalProductImageFiles/", p.ProductImageString);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    else
                    {
                        TempData["AlertMessageAdd"] = "Sadece .png uzantılı resimler kabul edilir.";

                        ViewBag.cv = CategoryValue;
                        return View();
                    }

                }
                else
                {
                    w.ProductImage = p.ProductImageString;

                }
                if (p.ProductThumbnailImage != null)
                {
                    if (p.ProductImage.FileName.Contains(".png"))
                    {
                        var extensionThumbnail = Path.GetExtension(p.ProductThumbnailImage.FileName);
                        var newImageNameThumbnail = Guid.NewGuid() + extensionThumbnail;
                        var LocationThumbnail = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MedicalProductImageFiles/", newImageNameThumbnail);
                        var streamThumbnail = new FileStream(LocationThumbnail, FileMode.Create);
                        p.ProductThumbnailImage.CopyTo(streamThumbnail);
                        w.ProductThumbnailImage = newImageNameThumbnail;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MedicalProductImageFiles/", p.ProductThumbnailImageString);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    else
                    {
                        TempData["AlertMessageAdd"] = "Sadece .png uzantılı resimler kabul edilir.";

                        ViewBag.cv = CategoryValue;
                        return View();
                    }
                }
                else
                {
                    w.ProductThumbnailImage = p.ProductThumbnailImageString;
                }

                mpm.TUpdate(w);
                TempData["AletrMessage"] = "Güncelleme İşlemi Başarılı...!";
                return RedirectToAction("ProductsListByCompany", "Company");

            }
            else
            {
                foreach (var item in result.Errors)
                {
                    TempData["AlertMessageAdd"] = item.ErrorMessage;

                }
                ViewBag.cv = CategoryValue;
                return View();
            }
        }
    }
}
