﻿using EntityLayer.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
   public class CategoryValidation : AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("İsim Yeri Boş Bırakılamaz").MaximumLength(100).WithMessage("İsim En Fazla 100 Karekter Yazılabilir");
            RuleFor(x => x.CategoryDescription).NotEmpty().WithMessage("Açıklama Yeri Boş Bırakılamaz").MinimumLength(20).WithMessage("Hakkında En Az 20 Karekter Yazılabilir").MaximumLength(500).WithMessage("Hakkında En Fazla 500 Karekter Yazılabilir");
        }
    }
}
