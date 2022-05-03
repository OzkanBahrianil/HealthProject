using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using HealthProject.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class AdminDashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManeger;
        BlogManeger bm = new BlogManeger(new EfBlogDal());
        ArticlesManeger arm = new ArticlesManeger(new EfArticlesDal());
        MedicalProductManeger mpm = new MedicalProductManeger(new EfMedicalProductDal());

        WriterManeger wm = new WriterManeger(new EfWriterDal());

        CommentManeger cmt = new CommentManeger(new EfCommentDal());
        CommentProductManeger cpm = new CommentProductManeger(new EfCommentProductDal());

        ContactManeger cm = new ContactManeger(new EfContactDal());


        CategoryManeger ctm = new CategoryManeger(new EfCategoryDal());
        ArticleCategoryManeger actm = new ArticleCategoryManeger(new EfArticleCategoryDal());
        ProductCategoryManeger pctm = new ProductCategoryManeger(new EfProductCategoryDal());


        NewsLetterManeger nwm = new NewsLetterManeger(new EfNewsLetterDal());
        BlogRaytingManeger rm = new BlogRaytingManeger(new EfBlogRaytingDal());

        public AdminDashboardController(UserManager<AppUser> userManeger)
        {
            _userManeger = userManeger;
        }


        public IActionResult Index()
        {
            ViewBag.AllUsers = wm.GetListT().Count();
            ViewBag.AllUsersMainFalse = wm.GetListT().Where(x => x.Status == false).Count();
            ViewBag.Writers = _userManeger.GetUsersInRoleAsync("Writer").Result.Count();

            ViewBag.BlogCount = bm.GetListT().Count();
            ViewBag.BlogFalseCount = bm.GetListTAdmin().Where(x => x.BlogStatus == false).Count();


            ViewBag.ArticleCount = arm.GetListT().Count();
            ViewBag.ArticleFalseCount = arm.GetListTAdmin().Where(x => x.ArticlesStatus == false).Count();

            ViewBag.ProductCount = mpm.GetListT().Count();
            ViewBag.ProductFalseCount = mpm.GetListTAdmin().Where(x => x.ProductStatus == false).Count();

            ViewBag.AllContactTrue = cm.GetListT().Where(x => x.ContactStatus == true).Count();

            ViewBag.AllComments = cpm.GetListT().Count() + cmt.GetListT().Count();

            ViewBag.ProductCommentFalse = cpm.GetListTAdmin().Where(x => x.CommentProductStatus == false).Count();
            ViewBag.BlogCommentFalse = cmt.GetListTAdmin().Where(x => x.CommentStatus == false).Count();

            ViewBag.SubscribeActive = nwm.GetListT().Where(x => x.MailStatus == true).Count();

            ViewBag.AllActiveCategoryBlog = ctm.GetListT().Count();
            ViewBag.AllActiveCategoryArticle = actm.GetListT().Count();
            ViewBag.AllActiveCategoryProduct = pctm.GetListT().Count();
            return View();
        }




        public IActionResult GetPiechartBlogJSON()
        {
            var countBlog = bm.GetBlogListWithCategoryWithCommentsAdmin();
            var countCategory = ctm.GetListTAdmin();
            List<CategoryClass> list = new List<CategoryClass>();
            foreach (var item in countCategory)
            {

                list.Add(new CategoryClass { categoryname = item.CategoryName, categorycount = countBlog.Where(x => x.CategoryID == item.CategoryID).Count() });
            }
            return Json(new { jsonlist = list });
        }
        public IActionResult GetPiechartProductJSON()
        {
            var countBlog = mpm.GetProductListWithCategoryWithCommentsAdmin();
            var countCategory = pctm.GetListTAdmin();
            List<CategoryProductClass> list = new List<CategoryProductClass>();
            foreach (var item in countCategory)
            {

                list.Add(new CategoryProductClass { categorynameproduct = item.ProductCategoryName, categorycountproduct = countBlog.Where(x => x.ProductCategoryID == item.ProductCategoryID).Count() });
            }
            return Json(new { jsonlist = list });
        }
        public IActionResult GetPiechartArticleJSON()
        {
            var countBlog = arm.GetArticlesListWithArticlesCategoryAdmin();
            var countCategory = actm.GetListTAdmin();
            List<CategoryArticleClass> list = new List<CategoryArticleClass>();
            foreach (var item in countCategory)
            {

                list.Add(new CategoryArticleClass { categorynamearticle = item.ArticleCategoryName, categorycountarticle = countBlog.Where(x => x.ArticleCategoryID == item.ArticleCategoryID).Count() });
            }
            return Json(new { jsonlist = list });
        }


        public IActionResult GetLineBlogCommentJSON()
        {
            var countBlog = bm.GetBlogListWithCategoryWithCommentsAdmin().OrderByDescending(x => x.Comments.Count()).Take(5).ToList();
            List<CommentClass> list = new List<CommentClass>();
            foreach (var item in countBlog)
            {

                list.Add(new CommentClass { name = item.BlogTitle, commentcount = item.Comments.Count() });
            }
            return Json(new { jsonlist = list });
        }
        public IActionResult GetLineProductCommentJSON()
        {
            var countBlog = mpm.GetProductListWithCategoryWithCommentsAdmin().OrderByDescending(x => x.CommentProducts.Count()).Take(5).ToList();
            List<CommentClass> list = new List<CommentClass>();
            foreach (var item in countBlog)
            {

                list.Add(new CommentClass { name = item.ProductTitle, commentcount = item.CommentProducts.Count() });
            }
            return Json(new { jsonlist = list });
        }


        public IActionResult GetchartBlogRaytingJSON()
        {


            var rayting = rm.GetListT().OrderByDescending(x => x.BlogTotalScore).Take(5).ToList();
            List<RaytingChartClass> list = new List<RaytingChartClass>();
            foreach (var item in rayting)
            {
                Decimal raytingstar = (Decimal)item.BlogTotalScore / (Decimal)item.BlogRaytingCount;
                raytingstar = Decimal.Round(raytingstar, 2);

                list.Add(new RaytingChartClass { blogname = bm.GetByIDT(item.BlogID).BlogTitle, raytingcount = raytingstar });
            }
            return Json(new { jsonlist = list });
        }


        public IActionResult GetchartBlogJSON()
        {

        
            var blog = bm.GetBlogListWithCategoryWithComments().OrderBy(x=>x.BlogCreateDate).Take(10).ToList();
            List<ListAdminClass> list = new List<ListAdminClass>();
            foreach (var item in blog)
            {

                list.Add(new ListAdminClass { year = item.BlogCreateDate.Year, month = item.BlogCreateDate.Month, day= item.BlogCreateDate.Day, title = item.BlogTitle });
            }
            return Json(new { jsonlist = list });
        }
        public IActionResult GetchartArticleJSON()
        {


            var article = arm.GetArticlesListWithArticlesCategory().OrderBy(x => x.ArticlesPublishDate).Take(10).ToList();
            List<ListAdminClass> list = new List<ListAdminClass>();
            foreach (var item in article)
            {

                list.Add(new ListAdminClass { year = item.ArticlesPublishDate.Year, month = item.ArticlesPublishDate.Month, day = item.ArticlesPublishDate.Day, title = item.ArticlesTitle });
            }
            return Json(new { jsonlist = list });
        }
        public IActionResult GetchartProductJSON()
        {


            var product = mpm.GetProductListWithCategoryWithComments().OrderBy(x => x.ProductRealiseDate).Take(10).ToList();
            List<ListAdminClass> list = new List<ListAdminClass>();
            foreach (var item in product)
            {

                list.Add(new ListAdminClass { year = item.ProductRealiseDate.Year , month = item.ProductRealiseDate.Month, day = item.ProductRealiseDate.Day, title = item.ProductTitle });
            }
            return Json(new { jsonlist = list });
        }
    }
}
