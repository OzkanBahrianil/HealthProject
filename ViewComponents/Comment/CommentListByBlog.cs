using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.ViewComponents.Comment
{
    public class CommentListByBlog: ViewComponent
    {
        CommentManeger cm = new CommentManeger(new EfCommentDal());
        public IViewComponentResult Invoke(int id, int page)
        {

            var values = cm.GetCommentListByIdBlog(id).Where(x => x.CommentStatus == true).OrderByDescending(y=>y.CommentDate).ToPagedList(page, 5);
           
            return View(values);
        }
    }
}
