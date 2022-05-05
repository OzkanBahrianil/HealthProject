using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
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

namespace HealthProject.Areas.MedicalProducts.Controllers
{
    [Area("MedicalProducts")]
    [Authorize(Roles = "CompanyWriter")]
    public class CompanyController : Controller
    {
        ProductCategoryManeger pcm = new ProductCategoryManeger(new EfProductCategoryDal());
        MedicalProductManeger mpm = new MedicalProductManeger(new EfMedicalProductDal());
        WriterManeger wm = new WriterManeger(new EfWriterDal());

        public IActionResult ProductsListByCompany(int page = 1)
        {
            var usermail = User.Identity.Name;
            var CompanyID = wm.TGetByFilter(x => x.Email == usermail).Id;
            var values = mpm.GetListWithCategoryCommentBCompanybmF(CompanyID).ToPagedList(page, 10);
            return View(values);

        }

        [HttpGet]
        public IActionResult ProductsAddByCompany()
        {
            List<SelectListItem> CategoryValue = (from x in pcm.GetListT()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.ProductCategoryName,
                                                      Value = x.ProductCategoryID.ToString()
                                                  }).ToList();
            ViewBag.cv = CategoryValue;
            return View();
        }
        [HttpPost]
        public IActionResult ProductsAddByCompany(AddProductImage p)
        {
            MedicalProduct w = new MedicalProduct();
            var usermail = User.Identity.Name;
            var CompanyID = wm.TGetByFilter(x => x.Email == usermail).Id;

            w.ProductShortContent = p.ProductShortContent;
            w.ProductContent = p.ProductContent;
            w.ProductTitle = p.ProductTitle;
            w.ProductCategoryID = p.ProductCategoryID;
            w.ProductStyle = p.ProductStyle;
            w.ProductCompanyWebsite = p.ProductCompanyWebsite;
            w.ProductStatus = false;
            w.ProductRealiseDate = p.ProductRealiseDate;
            w.UserID = CompanyID;

            List<SelectListItem> CategoryValue = (from x in pcm.GetListT()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.ProductCategoryName,
                                                      Value = x.ProductCategoryID.ToString()
                                                  }).ToList();
            MedicalProductValidation bv = new MedicalProductValidation();
            ValidationResult result = bv.Validate(w);
            if (result.IsValid)
            {
                if (p.ProductImage != null && p.ProductImage.FileName.Contains(".png"))
                {
                    var extension = Path.GetExtension(p.ProductImage.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var Location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MedicalProductImageFiles/", newImageName);
                    var stream = new FileStream(Location, FileMode.Create);
                    p.ProductImage.CopyTo(stream);
                    w.ProductImage = newImageName;

                }

                if (p.ProductThumbnailImage != null && p.ProductThumbnailImage.FileName.Contains(".png"))
                {
                    var extensionThumbnail = Path.GetExtension(p.ProductThumbnailImage.FileName);
                    var newImageNameThumbnail = Guid.NewGuid() + extensionThumbnail;
                    var LocationThumbnail = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MedicalProductImageFiles/", newImageNameThumbnail);
                    var streamThumbnail = new FileStream(LocationThumbnail, FileMode.Create);
                    p.ProductThumbnailImage.CopyTo(streamThumbnail);
                    w.ProductThumbnailImage = newImageNameThumbnail;
                }
                if (w.ProductImage != null && w.ProductThumbnailImage != null)
                {


                    if (w.ProductImage.Contains(".png") && w.ProductThumbnailImage.Contains(".png"))
                    {
                        mpm.TAdd(w);
                        TempData["AletrMessage"] = "Ekleme İşlemi Başarılı...!";
                        return RedirectToAction("ProductsListByCompany", "Company");
                    }
                    else
                    {
                        TempData["AlertMessageAdd"] = "Sadece .png uzantılı resimler kabul edilir.";

                        ViewBag.cv = CategoryValue;
                    }
                }
                else
                {
                    TempData["AlertMessageAdd"] = "Sadece .png uzantılı resimler kabul edilir.";

                    ViewBag.cv = CategoryValue;
                }
                return View();

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
        public IActionResult DeleteProducts(int id)
        {
            var productvalue = mpm.GetByIDT(id);
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;


            if (productvalue != null)
            {


                if (productvalue.UserID == writerID)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MedicalProductImageFiles/", productvalue.ProductImage);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    var path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MedicalProductImageFiles/", productvalue.ProductThumbnailImage);
                    if (System.IO.File.Exists(path2))
                    {
                        System.IO.File.Delete(path2);
                    }
                    mpm.TDelete(productvalue);
                    TempData["AletrMessage"] = "Silme İşlemi Başarılı...!";
                    return RedirectToAction("ProductsListByCompany");
                }
                else
                {
                    TempData["AletrMessage"] = "Yetkiniz Yoktur...!";
                    return RedirectToAction("ProductsListByCompany");
                }
            }
            else
            {
                TempData["AletrMessage"] = "Yetkiniz Yoktur...!";
                return RedirectToAction("ProductsListByCompany");
            }
        }

        [HttpGet]
        public IActionResult EditProducts(int id)
        {
            var value = mpm.GetByIDT(id);
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;
            if (value != null)
            {
                if (value.UserID == writerID)
                {

                    List<SelectListItem> CategoryValue = (from x in pcm.GetListT()
                                                          select new SelectListItem
                                                          {
                                                              Text = x.ProductCategoryName,
                                                              Value = x.ProductCategoryID.ToString()
                                                          }).ToList();
                    ViewBag.cv = CategoryValue;
                    AddProductImage addProductImage = new AddProductImage();
                    addProductImage.ProductID = value.ProductID;
                    addProductImage.ProductTitle = value.ProductTitle;
                    addProductImage.ProductImageString = value.ProductImage;
                    addProductImage.ProductRealiseDate = value.ProductRealiseDate;
                    addProductImage.ProductStyle = value.ProductStyle;
                    addProductImage.ProductThumbnailImageString = value.ProductThumbnailImage;
                    addProductImage.ProductShortContent = value.ProductShortContent;
                    addProductImage.ProductContent = value.ProductContent;
                    addProductImage.ProductCategoryID = value.ProductCategoryID;
                    addProductImage.ProductCompanyWebsite = value.ProductCompanyWebsite;
                    return View(addProductImage);
                }
                else
                {
                    return RedirectToAction("ProductsListByCompany");
                }
            }
            else
            {
                return RedirectToAction("ProductsListByCompany");
            }
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
