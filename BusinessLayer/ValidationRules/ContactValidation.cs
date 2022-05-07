using EntityLayer.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
   public class ContactValidation : AbstractValidator<Contact>
    {
        public ContactValidation()
        {
            RuleFor(x => x.ContactUserName).NotEmpty().WithMessage("Kullanıcı Adı Boş Bırakılamaz").MinimumLength(3).WithMessage("Kullanıcı Adı En Az 3 Karekter Yazılabilir").MaximumLength(40).WithMessage("Kullanıcı Adı En Fazla 40 Karakter İle Yazılır"); ;
            RuleFor(x => x.ContactMail).NotEmpty().WithMessage("Mail xBoş Bırakılamaz").EmailAddress().WithMessage("Lütfen Mail Adresi Giriniz");
            RuleFor(x => x.ContactSubject).NotEmpty().WithMessage("Konu Başlığı Boş Bırakılamaz").MinimumLength(3).WithMessage("Konu Başlık En Az 3 Karekter Yazılabilir").MaximumLength(100).WithMessage("Konu Başlık En Fazla 100 Karakter İle Yazılır");
            RuleFor(x => x.ContactMessage).NotEmpty().WithMessage("Mesaj İçeriği Boş Bırakılamaz").MinimumLength(3).WithMessage("Mesaj İçeriği En Az 3 Karekter Yazılabilir").MaximumLength(5000).WithMessage("Mesaj İçeriği En Fazla 5000 Karakter İle Yazılır");
        }
    }
}
