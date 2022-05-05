using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.Areas.Admin.ViewComponents.AdminBlog
{
    public class CommentListByAdmin :ViewComponent
    {
        CommentManeger cmm = new CommentManeger(new EfCommentDal());
        public IViewComponentResult Invoke(int id, int page)
        {
            var values = cmm.GetCommentListByIdBlogAdmin(id).OrderByDescending(y => y.CommentDate).ToPagedList(page, 5);
            return View(values);
        }
    }
    }

