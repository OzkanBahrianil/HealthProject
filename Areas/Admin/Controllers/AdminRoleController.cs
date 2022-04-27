﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using HealthProject.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]

    public class AdminRoleController : Controller
    {

        WriterManeger wm = new WriterManeger(new EfWriterDal());
        private readonly RoleManager<AppRole> _roleManeger;
        private readonly UserManager<AppUser> _userManeger;

        public AdminRoleController(RoleManager<AppRole> roleManeger, UserManager<AppUser> userManeger)
        {
            _roleManeger = roleManeger;
            _userManeger = userManeger;
        }
    

        public IActionResult Index()
        {
            var values = _roleManeger.Roles.ToList();
            return View(values);
        }    
        public async Task<IActionResult> AddRole(RoleViewModel p )
        {
            if (ModelState.IsValid)
            {
                AppRole role = new AppRole
                {
                    Name = p.Name
                };
                var result = await _roleManeger.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["AlertMessage"] = "Hatalı Giriş Yaptınız";
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRole(int Id)
        {
            var values = _roleManeger.Roles.FirstOrDefault(x => x.Id == Id);
            var result = await _roleManeger.DeleteAsync(values);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["AlertMessage"] = "Hatalı Giriş Yaptınız";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateRole(int Id)
        {
            var values = _roleManeger.Roles.FirstOrDefault(x=>x.Id == Id);
            RoleUpdateViewModel model = new RoleUpdateViewModel{
                Id = values.Id,
                Name = values.Name
            };
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleUpdateViewModel p)
        {
            var values = _roleManeger.Roles.Where(x => x.Id == p.Id).FirstOrDefault();
            values.Name = p.Name;
            var result = await _roleManeger.UpdateAsync(values);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["AlertMessage"] = "Hatalı Giriş Yaptınız";
              
            }
            return View(values);
        }

        public IActionResult UserRoleList(int page = 1)
        {
            var values = wm.GetListT().ToPagedList(page, 10);
            return View(values);
        }
      
 

        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> model)
        {
            
            
            foreach (var item in model)
            {
                var userId = item.UserId ;
                var user = _userManeger.Users.FirstOrDefault(x => x.Id == userId);
                if (item.Exists)
                {
                    await _userManeger.AddToRoleAsync(user, item.Name);
                }
                else
                {
                    await _userManeger.RemoveFromRoleAsync(user, item.Name);
                }
            }

            return RedirectToAction("UserRoleList");
        }
    }
}
