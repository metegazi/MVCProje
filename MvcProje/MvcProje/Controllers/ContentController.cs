using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content

        ContentManager cm = new ContentManager(new EfContentDal());
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult GetAllContent(string p)
        {
            //Context c = new Context();
            //var values = from x in c.Contents select x;
            //values = values.Where(y => y.ContentValue.Contains(p));

            var values = cm.GetListByWord(p);   //Kelimeye göre getirir.
            if (string.IsNullOrEmpty(p))
            {
                return View(cm.GetList());
            }
            return View(values.ToList());
        }
        public ActionResult ContentByHeading(int id)
        {
            var contentvalues = cm.GetListByHeadingID(id);
            return View(contentvalues);
        }

    }
}