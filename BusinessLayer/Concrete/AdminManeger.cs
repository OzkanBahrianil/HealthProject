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
   public class AdminManeger : IAdminService
    {
        IAdminDal _AdminDal;

        public AdminManeger(IAdminDal adminDal)
        {
            _AdminDal = adminDal;
        }

        public Admin GetByIDT(int id)
        {
            return _AdminDal.Get(x => x.AdminID == id);
        }

        public List<Admin> GetListT()
        {
            return _AdminDal.List();
        }

        public void TAdd(Admin t)
        {
            _AdminDal.Insert(t);
        }

        public void TDelete(Admin t)
        {
            _AdminDal.Delete(t);
        }

        public void TUpdate(Admin t)
        {
            _AdminDal.Update(t);
        }
    }
}
