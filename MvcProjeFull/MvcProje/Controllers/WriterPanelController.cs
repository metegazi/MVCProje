using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using FluentValidation.Results;
using BusinessLayer.ValidationRules;
using System.IO;
using System.Web.Helpers;

namespace MvcProje.Controllers
{
    public class WriterPanelController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());

        WriterValidator writervalidator = new WriterValidator();    //kural oluşturmak için nesne ürettik.

        Context c = new Context();
        // GET: WriterPanel
        [HttpGet]
        public ActionResult WriterProfile(int id=0)
        {
            string p = (string)Session["WriterMail"];
            id = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();

            var writervalue = wm.GetById(id);
            return View(writervalue);
        }
        [HttpPost]
        public ActionResult WriterProfile(Writer p)
        {
            ValidationResult results = writervalidator.Validate(p);
            if (results.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    string url = "~/Images/";
                    //Resmin fonksiyonla önce server'a sonra veri tabanına yüklenmesini gerçekleştirdik
                    p.WriterImage = resimKucult(Request.Files[0].InputStream, url);
                }
                wm.WriterUpdate(p);
                return RedirectToAction("AllHeading","WriterPanel");
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
        public ActionResult MyHeading(string p)
        {
            p = (string)Session["WriterMail"];
            var writeridinfo = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            //ViewBag.d = writeridinfo;

            var values = hm.GetListByWriter(writeridinfo);
            return View(values);
        }
        [HttpGet]
        public ActionResult NewHeading()
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            return View();
        }

        [HttpPost]
        public ActionResult NewHeading(Heading p)
        {
            string writermailinfo = (string)Session["WriterMail"];
            var writeridinfo = c.Writers.Where(x => x.WriterMail == writermailinfo).Select(y => y.WriterID).FirstOrDefault();
            ViewBag.m = writeridinfo;
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterID = writeridinfo;
            p.HeadingStatus = true;
            hm.HeadingAdd(p);
            return RedirectToAction("MyHeading");
        }
        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            var HeadingValue = hm.GetByID(id);
            return View(HeadingValue);
        }
        [HttpPost]
        public ActionResult EditHeading(Heading p)
        {
            hm.HeadingUpdate(p);
            return RedirectToAction("MyHeading");
        }
        public ActionResult DeleteHeading(int id)
        {
            var HeadingValue = hm.GetByID(id);
            //HeadingValue.HeadingStatus = false;
            hm.HeadingDelete(HeadingValue);
            return RedirectToAction("MyHeading");
        }
        public ActionResult AllHeading(int p=1)
        {
            //Sayfalama işlemi yaptık girilen boyuttan fazla olan kaydı başka sayfaya aktarır.
            var headings = hm.GetListWriter().ToPagedList(p, 4);      
            return View(headings);
        }
        public PartialViewResult WriterProfileGetImage()
        {
            string p = (string)Session["WriterMail"];
            var writeridinfo = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var value = wm.GetById(writeridinfo);
            //var writerinfo = wm.GetList();
            return PartialView(value);
        }
        public string resimKucult(Stream stream, string dizin)
        {
            if (stream != null)
            {
                try
                {
                    WebImage resim = new WebImage(stream); //Burada resmi webimage çevirdim.
                    if (resim.Width > 500) //Ben resimlerimin boyutunu 500px ile sınırlandırdım. 
                        resim = resim.Resize(500, 500, preserveAspectRatio: true);
                    else
                        if (resim.Height > 500)//Width değeri 500den küçükse height değerini kontrol ediyoruz. Eğer iki koşul da sağlanmazsa zaten küçük bir resim olduğu için bir işlem yapmıyorum.
                        resim = resim.Resize(500, 500, preserveAspectRatio: true);

                    //Resmi yeniden adlandırdım.
                    string resimAdi = DateTime.UtcNow.AddHours(3).ToString("yyyyMMddHHmmss") + "." + resim.ImageFormat;

                    if (!Directory.Exists(Server.MapPath(dizin))) // Dizinin var olup olmadığını kontrol ediyorum.
                    {
                        Directory.CreateDirectory(Server.MapPath(dizin)); //Dizin yoksa oluşturuyorum.
                    }
                    //---------Kayıt işlemleri------------

                    //Resmin url ile server'a yüklenmesini gerçekleştirdik

                    resim.Save(Server.MapPath(dizin + resimAdi), resim.ImageFormat, false);

                    //Veri tabanında tilda işareti olmaması gerektiğinden return etmeden önce Trim komutuyla sildim.
                    string yeniDizin = dizin.TrimStart(new char[] { ' ', '~' });

                    return yeniDizin + resimAdi; // resim yolumuzu döndürüyoruz.

                }
                catch (Exception)
                {
                    //Catch kısmı tamamen özelleştirilebilir hata durumunda ne yapmasını dilerseniz o şekilde bir uyarı döndürebilirsiniz.                  
                    return "hata";
                }

            }
            else
                return "hata";

        }




    }
}