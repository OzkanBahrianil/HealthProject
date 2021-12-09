using EntityLayer.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
   public class WriterValidation: AbstractValidator<Writer>
    {
        public WriterValidation()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("İsim Boş Bırakılamaz.");
        }
    }
}
