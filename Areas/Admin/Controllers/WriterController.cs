using BusinessLayer.Concrete;
using DataAccessLayer.EntityFremawork;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WriterController : Controller
    {
        WriterManeger cm = new WriterManeger(new EfWriterDal());
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult WriterList()
        {
            var values = cm.GetListT();
            var jsonWriters = JsonConvert.SerializeObject(values);
            return Json(jsonWriters);
        }     
        public IActionResult WriterDelete(int id)
        {
            var values = cm.GetListT().FirstOrDefault(x=>x.Id==id);
            cm.TDelete(values);
            return Json(values);
        }     
        public IActionResult WriterListByID(int WriterId)
        {

            var values = cm.GetByIDT(WriterId);
            var jsonWriters = JsonConvert.SerializeObject(values);
            return Json(jsonWriters);
        }

    }
}
