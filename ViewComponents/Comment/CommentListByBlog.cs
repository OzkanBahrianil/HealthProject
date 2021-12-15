using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Comment
{
    public class CommentListByBlog: ViewComponent
    {
        CommentManeger cm = new CommentManeger(new EfCommentDal());
        public IViewComponentResult Invoke(int id)
        {
            var values = cm.GetCommentListByIdBlog(id);
            return View(values);
        }
    }
}
