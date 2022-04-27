using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Articles
{
    public class CategoryListDashboardArticle : ViewComponent
    {
        ArticleCategoryManeger acm = new ArticleCategoryManeger(new EfArticleCategoryDal());
        public IViewComponentResult Invoke()
        {
            var values = acm.GetCategoryListWithArticle();
            return View(values);
        }
    }
}
