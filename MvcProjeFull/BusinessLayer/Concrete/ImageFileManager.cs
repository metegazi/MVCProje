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
    public class ImageFileManager : IImageFileService
    {
        IImageFileDal _imagefileDal;

        public ImageFileManager(IImageFileDal imagefileDal)
        {
            _imagefileDal = imagefileDal;
        }

        public List<ImageFile> GetList()
        {
           return _imagefileDal.List();
        }

        public void ImageAdd(ImageFile image)
        {
            _imagefileDal.Insert(image);
        }

        public void ImageUpdate(ImageFile image)
        {
            _imagefileDal.Update(image);
        }
    }
}
