using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
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
    public class WriterPanelMessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();
      
        // GET: WriterPanelMessage
        public ActionResult Inbox()
        {
            string p = (string)Session["WriterMail"];
            
            var messagelist = mm.GetListInbox(p);
            return View(messagelist);

        }
        public ActionResult Sendbox()
        {
            string p = (string)Session["WriterMail"];
            var messagelist = mm.GetListSendInbox(p);
            return View(messagelist);
        }
        public PartialViewResult MessageListMenu()
        {
            string p = (string)Session["WriterMail"];
            //MessageManageri dahil edip veri listesini partialviewe gönderdim
            //Sebebi: Okunmamış mesaj adedini listelemek
            var messagevalues = mm.GetListInbox(p);
            return PartialView(messagevalues);
        }
        public ActionResult GetInBoxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            values.ReadInfo = true;        //okundu durumunu true yaptım
            mm.MessageUpdate(values);       //Bu durumu güncelledim
            return View(values);
        }
        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }
        public ActionResult ReadMessages()
        {
            string p = (string)Session["WriterMail"];
            //Okunan Mesajların gönderildiği View
            var messagelist = mm.GetListInbox(p);
            return View(messagelist);
        }
        public ActionResult GetFindMessage(string w)
        {
            string p = (string)Session["WriterMail"];
            var values = mm.GetListByWord(p, w);   //Kelimeye göre getirir.
            if (string.IsNullOrEmpty(w))
            {
                return View(mm.GetListInbox(p));
            }
            return View(values.ToList());
        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message p)
        {
            ValidationResult results = messagevalidator.Validate(p);
            if (results.IsValid)
            {
                string user = (string)Session["WriterMail"];
                p.SenderMail = user;
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.MessageAdd(p);
                return RedirectToAction("Sendbox");
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
    }
}