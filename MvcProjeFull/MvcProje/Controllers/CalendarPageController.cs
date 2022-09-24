using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using MvcProje.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class CalendarPageController : Controller
    {
        //CalendarManager cm = new CalendarManager(new EfCalendarDal());
        ContentManager ctm = new ContentManager(new EfContentDal());
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult WriterIndex()
        {
            return View();

        }
        public JsonResult GetAllCalendarEvents()
        {
            List<CalendarEvent> eventItems = new List<CalendarEvent>();
           
            //var calendarValue = cm.GetByDate(start, end);
            foreach (var a in hm.GetListAdmin())
            {
                foreach (var b in ctm.GetListByHeadingID(a.HeadingID))
                {
                    CalendarEvent item = new CalendarEvent();

                    item.id = int.Parse(a.HeadingID.ToString());
                    item.title = a.HeadingName.ToString();
                    item.start = string.Format("{0:s}", a.HeadingDate);
                    item.color = "#00FFFF";
                    item.allDay = true;
                    item.description = b.Writer.WriterName +"\n Açıklama :" + b.ContentValue;

                    eventItems.Add(item);
                }

                //eventItems.Add(item);
            }
            
            return Json(eventItems, JsonRequestBehavior.AllowGet);
        }
       


    }
}