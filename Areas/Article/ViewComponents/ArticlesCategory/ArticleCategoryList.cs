using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Areas.Article.ViewComponents.ArticlesCategory
{
    [Area("Article")]
    public class ArticleCategoryList:ViewComponent
    {
        ArticleCategoryManeger apm = new ArticleCategoryManeger(new EfArticleCategoryDal());
        public IViewComponentResult Invoke()
        {
            var values = apm.GetListT();
            return View(values);
        }
    }
}
