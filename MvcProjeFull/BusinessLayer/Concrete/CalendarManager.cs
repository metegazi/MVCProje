using BusinessLayer.Abstact;
using DataAccessLayer.Abstract;
using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CalendarManager : ICalendarService
    {
        ICalendarDal _calendarDal;

        public CalendarManager(ICalendarDal calendarDal)
        {
            _calendarDal = calendarDal;
        }

        public void CalendarAdd(Calendar calendar)
        {
            _calendarDal.Insert(calendar);
        }

        public void CalendarDelete(Calendar calendar)
        {
            throw new NotImplementedException();
        }

        public void CalendarUpdate(Calendar calendar)
        {
            throw new NotImplementedException();
        }

        public Calendar GetByDate(string start, string end)
        {
            return _calendarDal.Get(x => x.start >= DateTime.Parse(start) && x.end <= DateTime.Parse(end));
        }

        public Calendar GetByID(int id)
        {
            //return _calendarDal.Get(x => x.CategoryID == id);
            throw new NotImplementedException();
        }

        public List<Calendar> GetList()
        {
            return _calendarDal.List();
        }
    }
}
