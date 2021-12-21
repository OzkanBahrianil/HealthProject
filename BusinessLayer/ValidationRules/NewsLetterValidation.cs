using DataAccessLayer.Concrete;
using EntityLayer.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
   public class NewsLetterValidation : AbstractValidator<NewsLetter>
    {
        public NewsLetterValidation()
        {

            RuleFor(x => x.Mail).NotEmpty().WithMessage("Email Boş Bırakılamaz").Must(UniqueMail).WithMessage("Email zaten kayıtlı."); ;
         

        }
        private bool UniqueMail(string mail)
        {
            Context _c = new Context();
            var checkMail = _c.NewsLetters.Where(x => x.Mail.ToLower() == mail.ToLower()).SingleOrDefault();

            if (checkMail == null)
            {
                return true;
            }
            else
            {
                return false;

            }

        }
    }
}
