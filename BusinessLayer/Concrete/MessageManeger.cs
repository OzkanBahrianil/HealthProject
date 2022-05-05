using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
  public  class MessageManeger : IMessageService
    {

        IMessageDal _MessageDal;

        public MessageManeger(IMessageDal MessageDal)
        {
            _MessageDal = MessageDal;
        }

        public List<Message> GetMessageListById(int id)
        {
            return _MessageDal.GetListWithWriter().Where(p => p.MessageID == id).ToList();
        }

        public Message GetByIDT(int id)
        {
            return _MessageDal.Get(x => x.MessageID == id);
        }

        public List<Message> GetInboxLinstByWriter(int id)
        {
            return _MessageDal.GetListWithMessageByWriter(id);


        }
        public List<Message> SearchInbox(int id, string key)
        {
            key = key.ToLower();
            return _MessageDal.GetListWithMessageByWriter(id).Where(p => p.Subject.ToLower().Contains(key)
            || p.SenderUser.NameSurname.ToLower().Contains(key)
            || p.MessageStatus.ToString().ToLower().Contains(key)
            || p.MessageDetails.ToLower().Contains(key)).ToList();

        }
        public List<Message> GetInboxLinstByWriterSend(int id)
        {
            return _MessageDal.GetListWithMessageByWriterSend(id);


        }
        public List<Message> SearchSend(int id, string key)
        {
            key = key.ToLower();
            return _MessageDal.GetListWithMessageByWriterSend(id).Where(p => p.Subject.ToLower().Contains(key)
            || p.SenderUser.NameSurname.ToLower().Contains(key)
            || p.MessageStatus.ToString().ToLower().Contains(key)
            || p.MessageDetails.ToLower().Contains(key)).ToList();
        }
        public List<Message> GetListT()
        {
            return _MessageDal.List();
        }

        public void TAdd(Message t)
        {
            _MessageDal.Insert(t);
        }

        public void TDelete(Message t)
        {
            _MessageDal.Delete(t);
        }

        public void TUpdate(Message t)
        {
            _MessageDal.Update(t);
        }
    }
}
