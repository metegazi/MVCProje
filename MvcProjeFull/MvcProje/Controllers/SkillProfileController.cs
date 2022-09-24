using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class SkillProfileController : Controller
    {
        SkillProfileManager spm = new SkillProfileManager(new EfSkillProfileDal());
        // GET: SkillProfile
        public ActionResult Index()
        {
            var skillvalues = spm.GetList();
            return View(skillvalues);
        }
        [HttpGet]
        public ActionResult AddSkill()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSkill(SkillProfile skill)
        {
            spm.SkillProfileAdd(skill);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditSkill(int id)
        {
            //List<SelectListItem> skilllist = (from x in spm.GetList()
            //                                  select new SelectListItem
            //                                  {
            //                                      Text = x.SkillName,
            //                                      Value = x.SkillID.ToString()
            //                                  }).ToList();
            //var distinct = adminrole.Distinct();
            //var skilllist = adminrole.GroupBy(x => x.Text).Select(g => g.First()).ToList();
            //var id = skilllist.Select(x => x.Value);
            //ViewBag.vlc = skilllist;

            //var skillValue = spm.GetByID(Convert.ToInt32(id));
            var skillvalue = spm.GetByID(id);
            //ViewBag.id = id;
            return View(skillvalue);
        }

        [HttpPost]
        public ActionResult EditSkill(SkillProfile skill)
        {
            spm.SkillProfileUpdate(skill);
            return RedirectToAction("Index");
        }

    }
}