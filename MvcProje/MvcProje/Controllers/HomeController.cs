using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class HomeController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterDal());

        WriterValidator writervalidator = new WriterValidator();    //kural oluşturmak için nesne ürettik.
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult HomePage()
        {
            Context c = new Context();
            //var deger = c.Categories.ToList();
            //var deger = c.Categories.Where(x => x.CategoryName == "Film").ToList();
            var countHeading = c.Headings.Count(x=>x.HeadingStatus != false).ToString();
            var countEntry = c.Contents.Count(x=>x.ContentStatus != false).ToString();
            var countWriter = c.Writers.Count(x=>x.WriterStatus != false).ToString();
            var countMessage = c.Messages.Count().ToString();
            ViewBag.baslik = countHeading;
            ViewBag.entry = countEntry;
            ViewBag.writer = countWriter;
            ViewBag.message = countMessage;
            //var deger = (from categori in c.Categories
            //            where categori.CategoryName == "Film"
            //            select categori).ToList();
            return View();
        }
        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddWriter(Writer p)
        {

            ValidationResult results = writervalidator.Validate(p);
            if (results.IsValid)
            {
                wm.WriterAdd(p);
                return RedirectToAction("HomePage");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}