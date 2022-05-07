using EntityLayer.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class NotificationValidation : AbstractValidator<Notification>
    {
        public NotificationValidation()
        {

            RuleFor(x => x.NotificationDetails).NotEmpty().WithMessage("Detay Boş Bırakılamaz").MinimumLength(50).WithMessage("50 Harften kısa detay yazılamaz.").MaximumLength(2000).WithMessage("2000 Harften uzun detay yazılamaz.");
            RuleFor(x => x.NotificationType).NotEmpty().WithMessage("Type Boş Bırakılamaz").MinimumLength(1).WithMessage("1 Harften kısa Type yazılamaz.").MaximumLength(2000).WithMessage("2000 Harften uzun Type yazılamaz.");
            RuleFor(x => x.NotificationTypeSymbol).NotEmpty().WithMessage("Type Sembolü Bırakılamaz").MinimumLength(1).WithMessage("1 Harften kısa Type Sembolü yazılamaz.").MaximumLength(2000).WithMessage("2000 Harften uzun Type Sembolü yazılamaz.");
            RuleFor(x => x.NotificationColor).NotEmpty().WithMessage("Renk Boş Bırakılamaz").MinimumLength(1).WithMessage("1 Harften kısa detay yazılamaz.").MaximumLength(2000).WithMessage("2000 Harften uzun Renk yazılamaz.");


        }
    }
}
