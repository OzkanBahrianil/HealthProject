using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Articles
{
    public class ArticlesListDashboard:ViewComponent
    {
       ArticlesManeger atm = new ArticlesManeger(new EfArticlesDal());
        public IViewComponentResult Invoke()
        {
            var values = atm.GetArticlesListWithArticlesCategory().OrderByDescending(x => x.ArticlesID).Take(10).ToList();
            return View(values);
        }
    }
}
