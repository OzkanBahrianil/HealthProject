using EntityLayer.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
   public class ProductCategoryValidation : AbstractValidator<ProductCategory>
    {
        public ProductCategoryValidation()
        {
            RuleFor(x => x.ProductCategoryName).NotEmpty().WithMessage("İsim Yeri Boş Bırakılamaz").MaximumLength(100).WithMessage("İsim En Fazla 100 Karekter Yazılabilir");
            RuleFor(x => x.ProductCategoryDescription).NotEmpty().WithMessage("Açıklama Yeri Boş Bırakılamaz").MinimumLength(20).WithMessage("Hakkında En Az 20 Karekter Yazılabilir").MaximumLength(500).WithMessage("Hakkında En Fazla 500 Karekter Yazılabilir");
        }
    }
}
