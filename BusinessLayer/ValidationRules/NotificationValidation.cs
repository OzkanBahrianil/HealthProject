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

            RuleFor(x => x.NotificationDetails).NotEmpty().WithMessage("Detay Boş Bırakılamaz").MinimumLength(22).WithMessage("22 Harften kısa detay yazılamaz.");


        }
    }
}
