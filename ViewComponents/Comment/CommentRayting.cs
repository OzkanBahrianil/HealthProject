using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Comment
{
    public class CommentRayting : ViewComponent
    {
        BlogRaytingManeger brm = new BlogRaytingManeger(new EfBlogRaytingDal());
        public IViewComponentResult Invoke(int id)
        {

            var rayting = brm.GetListT().Where(x => x.BlogID == id).FirstOrDefault();
            if (rayting != null)
            {
                if (rayting.BlogRaytingCount == 0)
                {
                    ViewBag.raytingstar = 0;
                }
                else
                {
                    Decimal raytingstar = (Decimal)rayting.BlogTotalScore / (Decimal)rayting.BlogRaytingCount;

                    raytingstar = Decimal.Round(raytingstar, 2);
                    ViewBag.raytingstar = raytingstar;
                }



            }
            else
            {
                ViewBag.raytingstar = 0;
            }

            return View();
        }
    }
}
