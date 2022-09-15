using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
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

      

    }
}