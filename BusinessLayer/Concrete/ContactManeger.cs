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
