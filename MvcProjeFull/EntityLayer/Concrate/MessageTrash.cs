using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class MessageTrash
    {
        [Key]
        public int TrashMessageID { get; set; }

        [StringLength(50)]
        public string TrashSenderMail { get; set; }

        [StringLength(50)]
        public string TrashReceiverMail { get; set; }

        [StringLength(100)]
        public string TrashSubject { get; set; }
        public string TrashMessageContent { get; set; }
        public DateTime TrashMessageDate { get; set; }

    }
}
