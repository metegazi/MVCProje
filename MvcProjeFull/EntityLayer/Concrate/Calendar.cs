using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Calendar
    {
        [Key]
        public int id { get; set; }

        [StringLength(100)]
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }

        [StringLength(10)]
        public string color { get; set; }

        [StringLength(10)]
        public string textColor { get; set; }

        public bool allDay { get; set; }


    }
}
