using EntityLayer.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MessageValidation : AbstractValidator<Message>
    {
        public MessageValidation()
        {

            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu Boş Bırakılamaz").MaximumLength(250).WithMessage("Konu En Fazla 250 Karekter Yazılabilir").MinimumLength(10).WithMessage("Konu En Az 10 Karekter Yazılabilir");
            RuleFor(x => x.MessageDetails).NotEmpty().WithMessage("İçerik Boş Bırakılamaz").MinimumLength(50).WithMessage("İçerik En Az 50 Karekter Yazılabilir").MaximumLength(100000).WithMessage("İçerik En Fazla 100000 Karekter Yazılabilir");

        }
    }
}
