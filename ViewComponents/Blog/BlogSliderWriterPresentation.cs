using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Blog
{
    public class BlogSliderWriterPresentation : ViewComponent
    {

        private readonly UserManager<AppUser> _userManeger;

        WriterManeger wm = new WriterManeger(new EfWriterDal());

        public BlogSliderWriterPresentation(UserManager<AppUser> userManeger)
        {

            _userManeger = userManeger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userRoles = await _userManeger.GetUsersInRoleAsync("Writer");
            ViewBag.MostrarTarefas = userRoles.Where(x => x.Status == true).ToList();
            return View();
        }
    }
}
