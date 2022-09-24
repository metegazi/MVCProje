using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class SkillProfile
    {
        [Key]
        public int SkillID { get; set; }

        [StringLength(50)]
        public string SkillName { get; set; }
        public int SkillValue { get; set; } 

    }
}
