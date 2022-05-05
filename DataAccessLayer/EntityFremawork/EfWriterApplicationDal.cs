using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFremawork
{
    public class EfWriterApplicationDal : GenericRepository<WriterApplication>, IWriterApplicationDal
    {
        public List<WriterApplication> GetListWithUser()
        {
            using (var c = new Context())
            {
                return c.WriterApplications.Include(x => x.User).ToList();
            }
        }
    }
}
