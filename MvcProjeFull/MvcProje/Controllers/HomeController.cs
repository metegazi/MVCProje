using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class HomeController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterDal());
        ImageFileManager img = new ImageFileManager(new EfImageFileDal());

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
            var countHeading = c.Headings.Count(x => x.HeadingStatus != false).ToString();
            var countEntry = c.Contents.Count(x => x.ContentStatus != false).ToString();
            var countWriter = c.Writers.Count(x => x.WriterStatus != false).ToString();
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
            ImageFile imf = new ImageFile();
            if (results.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    //-------Boyutlandırma işlemleri----------

                    WebImage resim = new WebImage(Request.Files[0].InputStream);   //Burada resmi webimage çevirdim.

                    if (resim.Width > 500) //Ben resimlerimin boyutunu 500px ile sınırlandırdım.
                    {
                        resim = resim.Resize(500, 500, preserveAspectRatio: true);
                    }
                    else
                    {
                        if (resim.Height > 500)//Width değeri 500den küçükse height değerini kontrol ediyoruz. Eğer iki koşul da sağlanmazsa zaten küçük bir resim olduğu için bir işlem yapmıyorum.
                            resim = resim.Resize(500, 500, preserveAspectRatio: true);
                    }
                    //Resmi yeniden adlandırdım.
                    string resimAdi = DateTime.UtcNow.AddHours(3).ToString("yyyyMMddHHmmss") + "." + resim.ImageFormat;

                    //---------Kayıt işlemleri------------

                    //Resmin url ile server'a yüklenmesini gerçekleştirdik
                    string url = "~/Images/" + resimAdi;
                    resim.Save(Server.MapPath(url), resim.ImageFormat, false);

                    //Resmin veri tabanına yüklenmesini gerçekleştirdik
                    p.WriterImage = "/Images/" + resimAdi;



                }

                //Eklenen resmin galeriyede otomatik eklendmesi için verileri aldık
                imf.ImageName = p.WriterName;
                imf.ImagePath = p.WriterImage;

                wm.WriterAdd(p);
                img.ImageAdd(imf);


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



        // if (Request.Files.Count > 0)
        //        {
        //            string fileName = Path.GetFileName(Request.Files[0].FileName);
        ////string uzanti = Path.GetExtension(Request.Files[0].FileName);
        //string url = "~/Images/" + fileName;
        //Request.Files[0].SaveAs(Server.MapPath(url));
        //p.WriterImage = "/Images/" + fileName;


        //        }






    }
}