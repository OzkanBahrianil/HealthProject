using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Areas.MedicalProducts.ViewComponents.CommentProduct
{
    public class CommentListByProduct:ViewComponent
    {
        CommentProductManeger cpm = new CommentProductManeger(new EfCommentProductDal());
        public IViewComponentResult Invoke(int id)
        {
            var values = cpm.GetCommentListByIdProduct(id);
            return View(values);
        }
    }
}
