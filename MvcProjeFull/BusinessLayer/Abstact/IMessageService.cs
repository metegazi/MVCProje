using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstact
{
    interface IMessageService
    {
        List<Message> GetListInbox(string p);   
        List<Message> GetListSendInbox(string p);
        List<Message> GetListByWord(string p,string w);
        void MessageAdd(Message message);
        Message GetByID(int id);
        void MessageDelete(Message message);
        void MessageUpdate(Message message);
    }
}
