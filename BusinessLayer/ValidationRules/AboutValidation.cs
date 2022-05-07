using EntityLayer.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
  public  class AboutValidation : AbstractValidator<About>
    {
        public AboutValidation()
        {
            RuleFor(x => x.AboutDetails).NotEmpty().WithMessage("Hakkında Boş Bırakılamaz").MinimumLength(200).WithMessage("Hakkında En Az 180 Karekter Yazılabilir").MaximumLength(100000).WithMessage("İsim En Fazla 100000 Karekter Yazılabilir");
            RuleFor(x => x.AboutMapLocation).NotEmpty().WithMessage("Map Yeri Boş Bırakılamaz").MaximumLength(100000).WithMessage("Map En Fazla 100000 Karekter Yazılabilir");
        }
    }
}
