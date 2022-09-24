using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstact
{
    public interface IHeadingService
    {
       List<Heading> GetListWriter();
       List<Heading> GetListAdmin();
       List<Heading> GetListByWriter(int id);
       List<Heading> GetListHeadingDeleteByWriter(int id);
       List<Heading> GetListByCategoryID(int id);

        void HeadingAdd(Heading heading);
        Heading GetByID(int id);
        void HeadingDelete(Heading heading);
        void HeadingUpdate(Heading heading);

        void HeadingByCategoryDelete(Heading heading);
        Heading GetByCategoryID(int id);
    }
}
