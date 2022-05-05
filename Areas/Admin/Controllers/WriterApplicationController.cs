using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WriterApplicationController : Controller
    {
        WriterApplicationManeger wap = new WriterApplicationManeger(new EfWriterApplicationDal());
        readonly UserManager<AppUser> _userManager;

        public WriterApplicationController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(int page = 1)
        {
            var values = wap.GetListTWithUser().Where(x => x.ApplicationStatus == true).OrderByDescending(x => x.ApplicationDate).ToList().ToPagedList(page, 10);
            return View(values);
        }

        public IActionResult WriterApplicationRefuse(int id)
        {
            var value = wap.GetByIDT(id);

            if (value != null)
            {

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterApplicationPdf/", value.ApplicationCV);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                value.ApplicationStatus = false;
                wap.TUpdate(value);
                TempData["AletrMessageApp"] = "Reddetme İşlemi Başarılı...!";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["AletrMessageApp"] = "Böyle bir veri yoktur.!";
                return RedirectToAction("Index");
            }

        }
        public async Task<IActionResult> GiveWriterTag(int id)
        {
            AppUser User = await _userManager.FindByIdAsync(id.ToString());
            if (User != null)
            {
                await _userManager.AddToRoleAsync(User, "Writer");
                var values = wap.GetByIDUser(id);
                TempData["AletrMessageApp"] = "İşlem Başarılı...!";
                foreach (var item in values)
                {
                    if (item != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterApplicationPdf/", item.ApplicationCV);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                        wap.TDelete(item);
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
    }

}
