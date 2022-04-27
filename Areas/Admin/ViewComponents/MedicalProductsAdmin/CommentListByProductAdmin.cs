using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Areas.Admin.ViewComponents.MedicalProductsAdmin
{
    public class CommentListByProductAdmin : ViewComponent
    {
        CommentProductManeger cpm = new CommentProductManeger(new EfCommentProductDal());
        public IViewComponentResult Invoke(int id)
        {
            var values = cpm.GetCommentListByIdProductAdmin(id).OrderByDescending(y => y.CommentProductDate);
            return View(values);
        }
    }
}
