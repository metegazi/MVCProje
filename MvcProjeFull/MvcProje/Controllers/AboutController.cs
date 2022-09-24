using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        AboutManager abm = new AboutManager(new EfAboutDal());

        public ActionResult Index()
        {
            var aboutvalues = abm.GetList();
            return View(aboutvalues);
        }
        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAbout(About p)
        {
            abm.AboutAdd(p);
            return RedirectToAction("Index");
        }
        //Bunun görevi Popup kullandığımızda aynı sayfada hem ekleme hem listeleme 
        //olmasını engellemek ve solid'i ezmemek için PARTİALVİEW oluşturuldu.
        //Böylece aynı sayfada gibi görünüp farklı sayfada çalışacaklar.
        public PartialViewResult AboutPartial() 
        {
            return PartialView();
        }
        public ActionResult AboutDelete(int id)
        {
            var aboutValue = abm.GetByID(id);
            abm.AboutDelete(aboutValue);
            return RedirectToAction("Index");
        }
    }
}