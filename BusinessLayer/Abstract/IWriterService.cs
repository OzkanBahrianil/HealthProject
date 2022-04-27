using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
   public interface IWriterService : IGenericService<AppUser>
    {
        List<AppUser> GetWriterByID(int id);

        AppUser TGetByFilter(Expression<Func<AppUser, bool>> filter);


    }
}
