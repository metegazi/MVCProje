using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstact
{
    public interface ICalendarService
    {
        List<Calendar> GetList();
        void CalendarAdd(Calendar calendar);
        Calendar GetByID(int id);

        Calendar GetByDate(string start, string end);
        void CalendarDelete(Calendar calendar);

        void CalendarUpdate(Calendar calendar);
    }
}
