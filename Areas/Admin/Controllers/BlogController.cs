using ClosedXML.Excel;
using DataAccessLayer.Concrete;
using HealthProject.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {  
        public IActionResult ExtractListExcel()
        {
            return View();
        }
        public IActionResult ExporDinamicExcelBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Başlık";
                worksheet.Cell(1, 3).Value = "Blog İçerik";
                worksheet.Cell(1, 4).Value = "Blog Kısa İçerik";
                worksheet.Cell(1, 5).Value = "Blog Durumu";
                worksheet.Cell(1, 6).Value = "Blog Tarihi";
                worksheet.Cell(1, 7).Value = "Blog Kategori Id";
                worksheet.Cell(1, 8).Value = "Blog Yazar ID";

                int BlogRowCount = 2;
                foreach (var item in GetBlogList())
                {
                    worksheet.Cell(BlogRowCount, 1).Value = item.ID;
                    worksheet.Cell(BlogRowCount, 2).Value = item.BlogTitle;
                    worksheet.Cell(BlogRowCount, 3).Value = item.BlogContent;
                    worksheet.Cell(BlogRowCount, 4).Value = item.BlogShortContent;
                    worksheet.Cell(BlogRowCount, 5).Value = item.BlogStatus;
                    worksheet.Cell(BlogRowCount, 6).Value = item.BlogCreateDate;
                    worksheet.Cell(BlogRowCount, 7).Value = item.CategoryID;
                    worksheet.Cell(BlogRowCount, 8).Value = item.UserID;
                    BlogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Bloglist.xlsx");
                }
            }

        }
        public List<BlogModel> GetBlogList()
        {
            List<BlogModel> bm = new List<BlogModel>();
            using (var c = new Context())
            {
                bm = c.Blogs.Select(x => new BlogModel
                {
                    ID = x.BlogID,
                    BlogContent = x.BlogContent,
                    BlogShortContent = x.BlogShortContent,
                    BlogStatus = x.BlogStatus,
                    BlogCreateDate = x.BlogCreateDate,
                    CategoryID = x.CategoryID,
                    UserID = x.UserID,
                    BlogTitle = x.BlogTitle
                }).ToList();
            }
            return bm;
        }

        public IActionResult ExporDinamicExcelArticleList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Makale Listesi");
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "İçerik";
                worksheet.Cell(1, 3).Value = "Kısa İçerik";
                worksheet.Cell(1, 4).Value = "Durum";
                worksheet.Cell(1, 5).Value = "Tarih";
                worksheet.Cell(1, 6).Value = "Kategori Id";
                worksheet.Cell(1, 7).Value = "Yazar ID";
                worksheet.Cell(1, 8).Value = "Başlık";       
                worksheet.Cell(1, 9).Value = "Yazarı";
                worksheet.Cell(1, 10).Value = "Type";

                int BlogRowCount = 2;
                foreach (var item in GetArticleList())
                {
                    worksheet.Cell(BlogRowCount, 1).Value = item.ArticlesID;
                    worksheet.Cell(BlogRowCount, 2).Value = item.ArticlesContent;
                    worksheet.Cell(BlogRowCount, 3).Value = item.ArticlesShortContent;
                    worksheet.Cell(BlogRowCount, 4).Value = item.ArticlesStatus;
                    worksheet.Cell(BlogRowCount, 5).Value = item.ArticlesPublishDate;
                    worksheet.Cell(BlogRowCount, 6).Value = item.ArticleCategoryID;
                    worksheet.Cell(BlogRowCount, 7).Value = item.UserID;
                    worksheet.Cell(BlogRowCount, 8).Value = item.ArticlesTitle;    
                    worksheet.Cell(BlogRowCount, 9).Value = item.ArticlesWritersName;
                    worksheet.Cell(BlogRowCount, 10).Value = item.ArticlesType;
                    BlogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Articlelist.xlsx");
                }
            }

        }
        public List<ArticleModel> GetArticleList()
        {
            List<ArticleModel> arm = new List<ArticleModel>();
            using (var c = new Context())
            {
                arm = c.Articles.Select(x => new ArticleModel
                {
                    ArticlesID = x.ArticlesID,
                    ArticlesContent = x.ArticlesContent,
                    ArticlesShortContent = x.ArticlesShortContent,
                    ArticlesStatus = x.ArticlesStatus,
                    ArticlesPublishDate = x.ArticlesPublishDate,
                    ArticleCategoryID = x.ArticleCategoryID,
                    UserID = x.UserID,
                    ArticlesTitle = x.ArticlesTitle,
                    ArticlesWritersName = x.ArticlesWritersName,
                    ArticlesType = x.ArticlesType
                }).ToList();
            }
            return arm;
        }

        public IActionResult ExporDinamicExcelMedicalProductsList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Medikal Ürün Listesi");
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "İçerik";
                worksheet.Cell(1, 3).Value = "Kısa İçerik";
                worksheet.Cell(1, 4).Value = "Durum";
                worksheet.Cell(1, 5).Value = "Tarih";
                worksheet.Cell(1, 6).Value = "Kategori Id";
                worksheet.Cell(1, 7).Value = "Yazar ID";
                worksheet.Cell(1, 8).Value = "Başlık";
                worksheet.Cell(1, 9).Value = "Web Sayfası";
                worksheet.Cell(1, 10).Value = "Style";

                int BlogRowCount = 2;
                foreach (var item in GetMedicalProductsList())
                {
                    worksheet.Cell(BlogRowCount, 1).Value = item.ProductID;
                    worksheet.Cell(BlogRowCount, 2).Value = item.ProductContent;
                    worksheet.Cell(BlogRowCount, 3).Value = item.ProductShortContent;
                    worksheet.Cell(BlogRowCount, 4).Value = item.ProductStatus;
                    worksheet.Cell(BlogRowCount, 5).Value = item.ProductRealiseDate;
                    worksheet.Cell(BlogRowCount, 6).Value = item.ProductCategoryID;
                    worksheet.Cell(BlogRowCount, 7).Value = item.UserID;
                    worksheet.Cell(BlogRowCount, 8).Value = item.ProductTitle;
                    worksheet.Cell(BlogRowCount, 9).Value = item.ProductCompanyWebsite;
                    worksheet.Cell(BlogRowCount, 10).Value = item.ProductStyle;
                    BlogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MedicalProductslist.xlsx");
                }
            }

        }
        public List<MedicalProductsModel> GetMedicalProductsList()
        {
            List<MedicalProductsModel> mpm = new List<MedicalProductsModel>();
            using (var c = new Context())
            {
                mpm = c.MedicalProducts.Select(x => new MedicalProductsModel
                {
                    ProductID = x.ProductID,
                    ProductContent = x.ProductContent,
                    ProductShortContent = x.ProductShortContent,
                    ProductStatus = x.ProductStatus,
                    ProductRealiseDate = x.ProductRealiseDate,
                    ProductCategoryID = x.ProductCategoryID,
                    UserID = x.UserID,
                    ProductTitle = x.ProductTitle,
                    ProductCompanyWebsite = x.ProductCompanyWebsite,
                    ProductStyle = x.ProductStyle
                }).ToList();
            }
            return mpm;
        }


        public IActionResult ExporDinamicExcelUsersList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Kullanıcı Listesi");
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "İsim Soyisim";
                worksheet.Cell(1, 3).Value = "Hakkında";
                worksheet.Cell(1, 4).Value = "Email";
                worksheet.Cell(1, 5).Value = "Durum";


                int BlogRowCount = 2;
                foreach (var item in GetUsersList())
                {
                    worksheet.Cell(BlogRowCount, 1).Value = item.Id;
                    worksheet.Cell(BlogRowCount, 2).Value = item.NameSurname;
                    worksheet.Cell(BlogRowCount, 3).Value = item.About;
                    worksheet.Cell(BlogRowCount, 4).Value = item.Email;
                    worksheet.Cell(BlogRowCount, 5).Value = item.Status;

                    BlogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Userslist.xlsx");
                }
            }

        }
        public List<UserModel> GetUsersList()
        {
            List<UserModel> um = new List<UserModel>();
            using (var c = new Context())
            {
                um = c.Users.Select(x => new UserModel
                {
                    Id = x.Id,
                    NameSurname = x.NameSurname,
                    About = x.About,
                    Email = x.Email,
                    Status = x.Status,

                }).ToList();
            }
            return um;
        }
    }
}
