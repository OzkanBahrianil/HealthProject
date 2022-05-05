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
   public class ContactManeger : IContactService
    {

        IContactDal _ContactDal;

        public ContactManeger(IContactDal contactDal)
        {
            _ContactDal = contactDal;
        }

        public Contact GetByIDT(int id)
        {
            return _ContactDal.Get(x => x.ContactID == id);
        }

        public List<Contact> GetListT()
        {
            return _ContactDal.List();
        }
        public List<Contact> Search(string key)
        {
            key = key.ToLower();
            return _ContactDal.List().Where(p => p.ContactUserName.ToLower().Contains(key)
            || p.ContactMail.ToLower().Contains(key)
            || p.ContactStatus.ToString().ToLower().Contains(key)
            || p.ContactSubject.ToString().ToLower().Contains(key)).ToList();

        }
        public void TAdd(Contact t)
        {
            _ContactDal.Insert(t);
        }

        public void TDelete(Contact t)
        {
            _ContactDal.Delete(t);
        }

        public void TUpdate(Contact t)
        {
            _ContactDal.Update(t);
        }

     
    }
}
