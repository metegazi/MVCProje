using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        [StringLength(200)]
        public string CategoryDescription { get; set; } //Category bilgisi


        public bool CategoryStatus { get; set; }    //Category information -- Silme yok aç kapa var

        public ICollection<Heading> Headings { get; set; }
    }
}
