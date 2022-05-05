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
   public class WriterApplicationManeger : IWriterApplicationService
    {
        IWriterApplicationDal _WriterApplicationDal;

        public WriterApplicationManeger(IWriterApplicationDal writerApplicationDal)
        {
            _WriterApplicationDal = writerApplicationDal;
        }

        public WriterApplication GetByIDT(int id)
        {
            return _WriterApplicationDal.Get(x => x.ApplicationID == id);
        }
        public List<WriterApplication> GetByIDUser(int id)
        {
            return _WriterApplicationDal.List().Where(x => x.UserID == id).ToList();
        }
        public List<WriterApplication> GetListT()
        {
            return _WriterApplicationDal.List();
        }
        public List<WriterApplication> GetListTWithUser()
        {
            return _WriterApplicationDal.GetListWithUser();
        }
        public List<WriterApplication> Search(string key)
        {
            key = key.ToLower();
            return _WriterApplicationDal.GetListWithUser().Where(p => p.User.NameSurname.ToLower().Contains(key)
            || p.User.Email.ToString().ToLower().Contains(key)
            || p.ApplicationStatus.ToString().ToLower().Contains(key)
            || p.ApplicationUniversity.ToString().ToLower().Contains(key)
            || p.ApplicationUniversityDepartment.ToString().ToLower().Contains(key)
            || p.ApplicationDate.ToString().ToLower().Contains(key)
            || p.ApplicationCoverLetter.ToString().ToLower().Contains(key)).ToList();
        }

        public void TAdd(WriterApplication t)
        {
            _WriterApplicationDal.Insert(t);
        }

        public void TDelete(WriterApplication t)
        {
            _WriterApplicationDal.Delete(t);
        }

        public void TUpdate(WriterApplication t)
        {
            _WriterApplicationDal.Update(t);
        }
    }
}
