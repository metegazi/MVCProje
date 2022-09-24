using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik
        public ActionResult Index()
        {
            Context c = new Context();
            //1. Sorgu --> Toplam kategori sayısı
            var sorgu1 = c.Categories.Count().ToString();
            ViewBag.sorgu1 = sorgu1;

            //2. Sorgu --> Başlık tablosunda "yazılım" kategorisine ait başlık sayısı
            var sorgu2 = c.Headings.Count(x => x.HeadingStatus != false && x.Category.CategoryName == "Teknoloji");
            ViewBag.sorgu2 = sorgu2;

            //3.Sorgu Yazar adında 'a' harfi geçen yazar sayısı
            //var sorgu3 = (from a in c.Writers where SqlMethods.Like(a.WriterName, "%a%") select a).Count().ToString();
            var sorgu3 = (from x in c.Writers where x.WriterName.Contains("a") select x).Count().ToString();
            ViewBag.sorgu3 = sorgu3;

            //4.Sorgu En fazla başlığa sahip kategori adı            
            //var sorgu4 = c.Headings.GroupBy(g => g.Category.CategoryID).Where(w => w.Count() > 1).Select(s => new { s.FirstOrDefault().Category.CategoryName, adet = s.Count() }).Distinct().Take(1).OrderByDescending(o => o.adet).ToString();
            var sorgu4 = c.Headings.GroupBy(g => g.CategoryID).Where(w => w.Count() > 1).Select(s => new { KategoriAdı = s.FirstOrDefault().Category.CategoryName, adet = s.Count() }).Distinct().OrderByDescending(o => o.adet).Take(1).Max(o=>o.KategoriAdı).ToString();
            ViewBag.sorgu4 = sorgu4;

            //5.Sorgu Başlıklar tablosunda durumu true olan başlıklar ile false olan başlıklar arasındaki sayısal fark
            //var sorgu5 = (c.Categories.Count(x => x.CategoryStatus == true) - c.Categories.Count(x => x.CategoryStatus != true)).ToString();
            var sorgu5 = (c.Headings.Count(x => x.HeadingStatus == true) - c.Headings.Count(x => x.HeadingStatus != true)).ToString();
            ViewBag.sorgu5 = sorgu5;

            return View();
        }
    }
}