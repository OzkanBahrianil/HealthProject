using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class ContactController : Controller
    {
        ContactManeger cm = new ContactManeger(new EfContactDal());
        public IActionResult Index(string sortOrder, string SearchString, int page = 1)
        {
            ViewData["CurrentFilterSearch"] = SearchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = sortOrder == "Name" ? "NameDesc" : "Name";
            ViewData["MailSortParam"] = sortOrder == "Mail" ? "MailDesc" : "Mail";
            ViewData["SubjectSortParam"] = sortOrder == "Subject" ? "SubjectDesc" : "Subject";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "DateDesc" : "Date";

            if (!String.IsNullOrEmpty(SearchString))
            {
                var values = cm.Search(SearchString);
                switch (sortOrder)
                {

                    case "Mail":
                        values = values.OrderBy(x => x.ContactMail).ToList();
                        break;
                    case "MailDesc":
                        values = values.OrderByDescending(x => x.ContactMail).ToList();
                        break;
                    case "Name":
                        values = values.OrderBy(s => s.ContactUserName).ToList();
                        break;
                    case "NameDesc":
                        values = values.OrderByDescending(s => s.ContactUserName).ToList();
                        break;
                    case "Subject":
                        values = values.OrderBy(s => s.ContactSubject).ToList();
                        break;
                    case "SubjectDesc":
                        values = values.OrderByDescending(s => s.ContactSubject).ToList();
                        break;
                    case "Date":
                        values = values.OrderBy(s => s.ContactDate).ToList();
                        break;
                    case "DateDesc":
                        values = values.OrderByDescending(s => s.ContactDate).ToList();
                        break;
                    default:
                        values = values.OrderByDescending(s => s.ContactID).ToList();
                        break;


                }
                return View(values.ToPagedList(page, 20));
            }
            else
            {
                var values = cm.GetListT();
                switch (sortOrder)
                {
                    case "Mail":
                        values = values.OrderBy(x => x.ContactMail).ToList();
                        break;
                    case "MailDesc":
                        values = values.OrderByDescending(x => x.ContactMail).ToList();
                        break;
                    case "Name":
                        values = values.OrderBy(s => s.ContactUserName).ToList();
                        break;
                    case "NameDesc":
                        values = values.OrderByDescending(s => s.ContactUserName).ToList();
                        break;
                    case "Subject":
                        values = values.OrderBy(s => s.ContactSubject).ToList();
                        break;
                    case "SubjectDesc":
                        values = values.OrderByDescending(s => s.ContactSubject).ToList();
                        break;
                    case "Date":
                        values = values.OrderBy(s => s.ContactDate).ToList();
                        break;
                    case "DateDesc":
                        values = values.OrderByDescending(s => s.ContactDate).ToList();
                        break;
                    default:
                        values = values.OrderByDescending(s => s.ContactID).ToList();
                        break;


                }
                return View(values.ToPagedList(page, 20));
            }


        }
        [HttpGet]
        public IActionResult ReadContact(int id)
        {
            var values = cm.GetByIDT(id);
            values.ContactStatus = false;
            cm.TUpdate(values);
            TempData["AlertMessage"] = "Okuma İşlemi Başarılı...!";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult DeleteContact(int id)
        {
            var values = cm.GetByIDT(id);
            cm.TDelete(values);
            TempData["AlertMessage"] = "Silme İşlemi Başarılı...!";
            return RedirectToAction("Index");
        }
    }
}
