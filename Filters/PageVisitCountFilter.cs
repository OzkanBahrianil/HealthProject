using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;

using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Filters
{
    public class PageVisitCountFilter : ActionFilterAttribute
    {
        PageVisitManeger pvm = new PageVisitManeger(new EfPageVisitDal());
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var pageUrl = filterContext.HttpContext.Request.Path;

            var pageS = pvm.GetByUrl(pageUrl);
            if (pageS == null)
            {
                var page = new PageVisit { PageUrl = pageUrl, Visits = 1 };

                pvm.TAdd(page);
            }
            else
            {
                pageS.Visits++;
                pvm.TUpdate(pageS);
            }
        }
    }
}
