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
            //Context c = new Context();
            List<CategoryClass> ct = new List<CategoryClass>();

            //var count = c.Headings.Count(x => x.CategoryID>0).ToString();

            //foreach (var item in c.Headings)
            //{
            //    //var id = item.CategoryID;
                
            //    ct.Add(new CategoryClass()
            //    {
            //        CategoryName = item.Category.CategoryName,
            //        CategoryCount = item.CategoryID
            //    });
            //}
            ct.Add(new CategoryClass()
            {
                CategoryName = "Seyehat",
                CategoryCount = 4
            });
            ct.Add(new CategoryClass()
            {
                CategoryName = "Seyehat",
                CategoryCount = 4
            });
            ct.Add(new CategoryClass()
            {
                CategoryName = "Teknoloji",
                CategoryCount = 7
            });
            ct.Add(new CategoryClass()
            {
                CategoryName = "Spor",
                CategoryCount = 1
            });
            return ct;
        }
    }
}