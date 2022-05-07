using EntityLayer.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
  public  class WriterApplicationValidation : AbstractValidator<WriterApplication>
    {
        public WriterApplicationValidation()
        {
            RuleFor(x => x.ApplicationCoverLetter).NotEmpty().WithMessage("Ön Yazı Boş Bırakılamaz").MinimumLength(100).WithMessage("100 Harften kısa Ön Yazı yazılamaz.").MaximumLength(10000).WithMessage("10000 Harften uzun Ön Yazı yazılamaz.");
            RuleFor(x => x.ApplicationUniversity).NotEmpty().WithMessage("Üniversite Boş Bırakılamaz").MinimumLength(5).WithMessage("5 Harften kısa Üniversite yazılamaz.").MaximumLength(500).WithMessage("500 Harften uzun Üniversite yazılamaz.");
            RuleFor(x => x.ApplicationUniversityDepartment).NotEmpty().WithMessage("Bölüm Boş Bırakılamaz").MinimumLength(5).WithMessage("5 Harften kısa Bölüm yazılamaz.").MaximumLength(500).WithMessage("500 Harften uzun Bölüm yazılamaz.");
        }
    }
}
