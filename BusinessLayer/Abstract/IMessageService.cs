using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
   public interface IMessageService : IGenericService<Message>
    {
        List<Message> GetInboxLinstByWriter(int id);
        List<Message> GetInboxLinstByWriterSend(int id);
        List<Message> GetMessageListById(int id);

    }
}
