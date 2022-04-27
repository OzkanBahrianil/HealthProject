using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.ViewComponents.Writer
{

    public class UserAdminHeader : ViewComponent
    {
        private readonly UserManager<AppUser> _userManeger;

        public UserAdminHeader(UserManager<AppUser> userManeger)
        {
            _userManeger = userManeger;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var usermail = User.Identity.Name;
            AppUser Users = await _userManeger.FindByEmailAsync(usermail);
            var get = await _userManeger.GetRolesAsync(Users);
            ViewBag.RoleList = get.FirstOrDefault();
            return View(Users);
        }
    }
}
