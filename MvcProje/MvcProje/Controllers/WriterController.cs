using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
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
    public class WriterController : Controller
    {
        // GET: Writer

        WriterManager wm = new WriterManager(new EfWriterDal());

        WriterValidator writervalidator = new WriterValidator();    //kural oluşturmak için nesne ürettik.
        
        public ActionResult Index()
        {
            var WriterValues = wm.GetList();
            return View(WriterValues);
        }

        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddWriter(Writer p)
        {
           
            ValidationResult results = writervalidator.Validate(p);
            if (results.IsValid)
            {
                wm.WriterAdd(p);
                return RedirectToAction("Index");
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
        [HttpGet]
        public ActionResult EditWriter(int id)
        {
            var writervalue = wm.GetById(id);
            return View(writervalue);
        }
        [HttpPost]
        public ActionResult EditWriter(Writer p)
        {
            ValidationResult results = writervalidator.Validate(p);
            if (results.IsValid)
            {
                wm.WriterUpdate(p);
                return RedirectToAction("Index");
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
        public ActionResult DeleteWriter(int id)
        {
            HeadingManager hm = new HeadingManager(new EfHeadingDal());
            ContentManager cm = new ContentManager(new EfContentDal());
            var WriterValue = wm.GetById(id);
            var HeadingValue = hm.GetListHeadingDeleteByWriter(id);
            var ContentValue = cm.GetListByWriter(id);
            wm.WriterDelete(WriterValue);
            foreach (var item in HeadingValue)
            {
                if (WriterValue.WriterStatus != true)
                {
                    item.HeadingStatus = false;
                }
                else
                {
                    item.HeadingStatus = true;
                }
                
                hm.HeadingUpdate(item);
            }
            foreach (var item2 in ContentValue)
            {
                if (WriterValue.WriterStatus != true)
                {
                    item2.ContentStatus = false;
                }
                else
                {
                    item2.ContentStatus = true;
                }
                cm.ContentUpdate(item2);
            }
            
            return RedirectToAction("Index");
        }
    }
}