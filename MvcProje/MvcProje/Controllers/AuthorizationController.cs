using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
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
    public class AuthorizationController : Controller
    {
        // GET: Authorization

        AdminManager adm = new AdminManager(new EfAdminDal());
        AdminValidator adminvalidator = new AdminValidator();
        public ActionResult Index()
        {
            var adminvalues = adm.GetList();
            return View(adminvalues);
        }
        [HttpGet]
        public ActionResult AddAdmin()
        {            
            return View();
        }
        [HttpPost]
        public ActionResult AddAdmin(Admin p)
        {
            ValidationResult results = adminvalidator.Validate(p);
            if (results.IsValid)
            {
                //Şifreyi eklerken hashleme işlemi yaptım.Öyle ekledim
                string hashPassword = Hash.MD5Create(p.AdminPassword);
                p.AdminPassword = hashPassword;
                adm.AdminAdd(p);
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
        public ActionResult EditAdmin(int id)
        {
             List<SelectListItem> adminrole = (from x in adm.GetList()
                                              select new SelectListItem
                                              {
                                                  Text = x.AdminRole,
                                                  //Value = x.id.ToString()
                                              }).ToList();
            //var distinct = adminrole.Distinct();
            var tekilRole = adminrole.GroupBy(x => x.Text).Select(g => g.First()).ToList();

            ViewBag.vlc = tekilRole;

            var adminvalue = adm.GetByID(id);
            return View(adminvalue);
        }
        [HttpPost]
        public ActionResult EditAdmin(Admin p)
        {

            ValidationResult results = adminvalidator.Validate(p);
            if (results.IsValid)
            {
                string hashPassword = Hash.MD5Create(p.AdminPassword);
                p.AdminPassword = hashPassword;
                adm.AdminUpdate(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return RedirectToAction("EditAdmin");
        }
        public ActionResult DeleteAdmin(int id)
        {
            var admin = adm.GetByID(id);
            adm.AdminDelete(admin);
            return RedirectToAction("Index");
        }
    }
}