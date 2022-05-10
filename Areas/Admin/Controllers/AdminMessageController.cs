using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class AdminMessageController : Controller
    {
        MessageManeger mm = new MessageManeger(new EfMessageDal());
        WriterManeger wm = new WriterManeger(new EfWriterDal());
        public IActionResult InBox(string SearchString, int page = 1)
        {
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;
            ViewData["CurrentFilter"] = SearchString;
            if (!String.IsNullOrEmpty(SearchString))
            {
                var valuesSearch = mm.SearchInbox(writerID, SearchString).ToPagedList(page, 12);
                var values = mm.GetInboxLinstByWriter(writerID).ToPagedList(page, 12);
                ViewBag.InboxR = values.Count();
                var valuesSend = mm.GetInboxLinstByWriterSend(writerID).ToPagedList(page, 12);
                ViewBag.InboxRS = valuesSend.Count();
                return View(valuesSearch);
            }
            else
            {

                var values = mm.GetInboxLinstByWriter(writerID).ToPagedList(page, 12);
                ViewBag.InboxR = values.Count();
                var valuesSend = mm.GetInboxLinstByWriterSend(writerID).ToPagedList(page, 12);
                ViewBag.InboxRS = valuesSend.Count();
                return View(values);
            }
        }
        public IActionResult InBoxSend(string SearchString, int page = 1)
        {
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;
            ViewData["CurrentFilter"] = SearchString;
            if (!String.IsNullOrEmpty(SearchString))
            {
                var valuesSearch = mm.SearchSend(writerID, SearchString).ToPagedList(page, 12);
                var values = mm.GetInboxLinstByWriterSend(writerID).ToPagedList(page, 12);
                var valuesReceived = mm.GetInboxLinstByWriter(writerID).ToPagedList(page, 12);
                ViewBag.InboxR = valuesReceived.Count();
                ViewBag.InboxRS = values.Count();
                return View(valuesSearch);
            }
            else
            {

                var values = mm.GetInboxLinstByWriterSend(writerID).ToPagedList(page, 12);
                var valuesReceived = mm.GetInboxLinstByWriter(writerID).ToPagedList(page, 12);
                ViewBag.InboxR = valuesReceived.Count();
                ViewBag.InboxRS = values.Count();
                return View(values);
            }
        }

        public IActionResult MessageDetails(int id)
        {
            var value = mm.GetMessageListById(id);
            var usermail = User.Identity.Name;
            var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;
            if (value.FirstOrDefault().ReceiverID == writerID)
            {

                var ChangeStatus = value.FirstOrDefault();
                ChangeStatus.MessageStatus = false;
                mm.TUpdate(ChangeStatus);

            }
            return View(value);
        }
        [HttpGet]
        public IActionResult SendMessage(int? id)
        {
            if (id.HasValue)
            {
                var ReceiverMail = wm.TGetByFilter(x => x.Id == id);
                ViewBag.sender = ReceiverMail.Email;
                ViewBag.senderName = ReceiverMail.NameSurname;

            }
            else
            {
                List<SelectListItem> WriterValue = (from x in wm.GetListT()
                                                    select new SelectListItem
                                                    {
                                                        Text = x.NameSurname,
                                                        Value = x.Id.ToString()
                                                    }).ToList();
                ViewBag.wv = WriterValue;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage(Message p, string NameMail, int? id)
        {
            if (id.HasValue)
            {
                var ReceiverMail = wm.TGetByFilter(x => x.Id == id);
                ViewBag.sender = ReceiverMail.Email;
                ViewBag.senderName = ReceiverMail.NameSurname;

            }
            else
            {
                List<SelectListItem> WriterValue = (from x in wm.GetListT()
                                                    select new SelectListItem
                                                    {
                                                        Text = x.NameSurname,
                                                        Value = x.Id.ToString()
                                                    }).ToList();
                ViewBag.wv = WriterValue;
            }
            MessageValidation bv = new MessageValidation();
            ValidationResult result = bv.Validate(p);
            if (result.IsValid)
            {
                var usermail = User.Identity.Name;
                var writerID = wm.TGetByFilter(x => x.Email == usermail).Id;

                if (p.ReceiverID == null)
                {
                    var WriterCheck = wm.TGetByFilter(x => x.Email == NameMail);
                    if (WriterCheck == null)
                    {
                        var WriterCheckLast = wm.TGetByFilter(x => x.NameSurname == NameMail);

                        if (WriterCheckLast == null)
                        {
                            TempData["AlertSame"] = "Kullanıcı Bulunamadı!!!";
                            return View();
                        }
                        else
                        {
                            p.ReceiverID = WriterCheckLast.Id;
                        }
                    }
                    else
                    {
                        p.ReceiverID = WriterCheck.Id;
                    }
                }

                p.SenderID = writerID;
                p.MessageStatus = true;
                p.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortTimeString());

                if (p.SenderID == p.ReceiverID)
                {
                    TempData["AlertSame"] = "Kendinize Mesaj Gönderemezsiniz!!!";
                    return View();
                }
                else
                {
                    mm.TAdd(p);

                }

                return RedirectToAction("InBoxSend", "AdminMessage");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    TempData["AlertSame"] = item.ErrorMessage;

                }
                return View();
            }
        }
    }
}
