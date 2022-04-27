﻿using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
   public interface IMedicalProductService : IGenericService<MedicalProduct>
    {
      
        List<MedicalProduct> TGetByFilter(Expression<Func<MedicalProduct, bool>> filter);
    }
}
