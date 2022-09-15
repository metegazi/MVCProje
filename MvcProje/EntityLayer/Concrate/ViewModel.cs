using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class ViewModel
    {
        public IEnumerable<Writer> WriterModel { get; set; }
        public IEnumerable <Heading> HeadingModel { get; set; }
    }
}
