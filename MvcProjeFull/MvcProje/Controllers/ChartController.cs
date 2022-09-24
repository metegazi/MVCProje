using DataAccessLayer.Concrete;
using MvcProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CategoryChart()
        {

            return Json(BlogList(), JsonRequestBehavior.AllowGet); //başka bir methottan veri çektik.
        }
        public ActionResult Deneme()
        {
            //Context c = new Context();
            //var deger = c.Categories.ToList();
            //var deger = c.Categories.Where(x => x.CategoryName == "Film").ToList();
            /*var deger = (from categori in c.Categories
                        where categori.CategoryName == "Film"
                        select categori).ToList();*/


            return View();
        }
        public List<CategoryClass> BlogList()
        {
            Context c = new Context();
            List<CategoryClass> categoryclass = new List<CategoryClass>();

            var value = c.Headings.GroupBy(g => g.CategoryID).Where(w => w.Count() > 1).Select(s => new { KategoriAdı = s.FirstOrDefault().Category.CategoryName, adet = s.Count() }).Distinct().OrderByDescending(o => o.adet).ToList();
           
            foreach (var a in value)
            {

                CategoryClass item = new CategoryClass();

                item.CategoryName = a.KategoriAdı;
                item.CategoryCount = a.adet;
                //item.start = string.Format("{0:s}", a.HeadingDate);
                

                categoryclass.Add(item);
            }

            //return Json(eventItems, JsonRequestBehavior.AllowGet);

            //ct.Add(new CategoryClass()
            //{
            //    CategoryName = "Seyehat",
            //    CategoryCount = 4
            //});
            //ct.Add(new CategoryClass()
            //{
            //    CategoryName = "Seyehat",
            //    CategoryCount = 4
            //});
            //ct.Add(new CategoryClass()
            //{
            //    CategoryName = "Teknoloji",
            //    CategoryCount = 7
            //});
            //ct.Add(new CategoryClass()
            //{
            //    CategoryName = "Spor",
            //    CategoryCount = 1
            //});
            //return ct;
            return categoryclass;
        }
    }
}