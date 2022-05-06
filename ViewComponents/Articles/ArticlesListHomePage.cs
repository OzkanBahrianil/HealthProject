using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.ViewComponents.Articles
{
    public class ArticlesListHomePage:ViewComponent
    {
      
        ArticlesManeger atm = new ArticlesManeger(new EfArticlesDal());
        public IViewComponentResult Invoke(int page)
        {
            var values = atm.GetArticlesListWithArticlesCategory().OrderByDescending(x => x.ArticlesID).ToPagedList(page, 9);
            return View(values);
        }
    }
}
