﻿using EntityLayer.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
   public class ArticleCategoryValidation : AbstractValidator<ArticleCategory>
    {
        public ArticleCategoryValidation()
        {

        }
    }
}