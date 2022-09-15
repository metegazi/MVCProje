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
    public class HeadingManager : IHeadingService
    {
        IHeadingDal _headingDal;

        public HeadingManager(IHeadingDal headingDal)
        {
            _headingDal = headingDal;
        }

        public Heading GetByCategoryID(int id)
        {
            return _headingDal.Get2(x => x.Category.CategoryID == id);
        }

        public Heading GetByID(int id)
        {
            return _headingDal.Get(x => x.HeadingID == id);
        }
        public List<Heading> GetListAdmin()
        {
            return _headingDal.List();
        }
        public List<Heading> GetListWriter()
        {
            return _headingDal.List(x=>x.HeadingStatus== true);
        }

        public List<Heading> GetListByCategoryID(int id)
        {
            return _headingDal.List(x => x.CategoryID == id);
        }

        public List<Heading> GetListByWriter(int id)
        {
            return _headingDal.List(x => x.WriterID == id && x.HeadingStatus == true);
        }

        public void HeadingAdd(Heading heading)
        {
            _headingDal.Insert(heading);
        }

        public void HeadingByCategoryDelete(Heading heading)
        {
            _headingDal.Delete(heading);
        }

        public void HeadingDelete(Heading heading)
        {
            if (heading.HeadingStatus != true)
            {
                heading.HeadingStatus = true;
            }
            else
            {
                heading.HeadingStatus = false;
            }

            _headingDal.Update(heading);
        }

        public void HeadingUpdate(Heading heading)
        {
            _headingDal.Update(heading);
        }

        public List<Heading> GetListHeadingDeleteByWriter(int id)
        {
            return _headingDal.List(x => x.WriterID == id);
        }
    }
}
