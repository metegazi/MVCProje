using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
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
    public class WriterController : Controller
    {
        // GET: Writer

        WriterManager wm = new WriterManager(new EfWriterDal());
        ImageFileManager ifm = new ImageFileManager(new EfImageFileDal());


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
            ImageFile imf = new ImageFile();
            if (results.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    string url = "~/Images/";

                    //Resmin fonksiyonla önce server'a sonra veri tabanına yüklenmesini gerçekleştirdik
                    p.WriterImage = resimKucult(Request.Files[0].InputStream, url);
                }

                //Eklenen resmin galeri sayfasınada otomatik eklenmesi için verileri aldık
                imf.ImageName = p.WriterName;
                imf.ImagePath = p.WriterImage;

                wm.WriterAdd(p);
                ifm.ImageAdd(imf);


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
            //ImageFile imf = new ImageFile();
            if (results.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    string url = "~/Images/";
                    //Resmin fonksiyonla önce server'a sonra veri tabanına yüklenmesini gerçekleştirdik
                    p.WriterImage = resimKucult(Request.Files[0].InputStream, url);
                }
                //***GALERİ SAYFASINA UPDATE YAPILAMADI SEBEBİ ID GEREKLİ IMAGEFİLE TABLASOUNA WRİTERID EKLEMEK GEREKİYOR
                //***                                   (DAHA SONRA BAKILACAK)  

                //Eklenen resmin galeri sayfasınada otomatik eklenmesi için verileri aldık
                //imf.ImageName = p.WriterName;

                //imf.ImagePath = p.WriterImage;

                wm.WriterUpdate(p);
                //ifm.ImageUpdate(imf);

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
