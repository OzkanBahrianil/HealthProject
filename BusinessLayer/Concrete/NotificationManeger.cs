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
   public class NotificationManeger : INotificationService
    {
        INotificationDal _NotificationDal;

        public NotificationManeger(INotificationDal NotificationDal)
        {
            _NotificationDal = NotificationDal;
        }

        public Notification GetByIDT(int id)
        {
            return _NotificationDal.Get(x => x.NotificationID == id);
        }

        public List<Notification> GetListT()
        {
            return _NotificationDal.List();
        }

        public void TAdd(Notification t)
        {
            _NotificationDal.Insert(t);
        }

        public void TDelete(Notification t)
        {
            _NotificationDal.Delete(t);
        }

        public void TUpdate(Notification t)
        {
            _NotificationDal.Update(t);
        }
    }
}
