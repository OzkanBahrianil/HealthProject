using EntityLayer.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
  public  class PresentationValidation : AbstractValidator<Presentation>
    {
        public PresentationValidation()
        {
            RuleFor(x => x.PresentationTitle).NotEmpty().WithMessage("Detay Boş Bırakılamaz").MinimumLength(10).WithMessage("10 Harften kısa başlık yazılamaz.").MaximumLength(250).WithMessage("250 Harften uzun başlık yazılamaz.");
            RuleFor(x => x.PresentationDetails).NotEmpty().WithMessage("Detay Boş Bırakılamaz").MinimumLength(1000).WithMessage("1000 Harften kısa detay yazılamaz.").MaximumLength(100000).WithMessage("100000 Harften uzun detay yazılamaz.");
            RuleFor(x => x.PresentationShortDetails).NotEmpty().WithMessage("Detay Boş Bırakılamaz").MinimumLength(800).WithMessage("800 Harften kısa detay yazılamaz.").MaximumLength(1200).WithMessage("1200 Harften uzun detay yazılamaz.");
        }
    }
}
