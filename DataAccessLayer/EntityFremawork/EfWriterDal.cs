using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFremawork
{
    public class EfWriterDal:GenericRepository<AppUser>, IWriterDal
    {
        public AppUser GetByFilterFL(Expression<Func<AppUser, bool>> filter = null)
        {
            using var c = new Context();
            if (filter == null)
                return c.Set<AppUser>().FirstOrDefault();
            else
                return c.Set<AppUser>().FirstOrDefault(filter);
        }
    }
}
