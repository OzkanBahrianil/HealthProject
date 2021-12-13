﻿using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
   public interface IWriterService : IGenericService<Writer>
    {
        List<Writer> GetWriterByID(int id);

        Writer TGetByFilter(Expression<Func<Writer, bool>> filter);


    }
}
