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
    public class WriterValidation : AbstractValidator<Writer>
    {

        public WriterValidation()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("İsim Boş Bırakılamaz").MinimumLength(6).WithMessage("Lütfen tam isminizi giriniz.");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkında Boş Bırakılamaz");


        }


    }
    public class WriterValidationRegister : AbstractValidator<Writer>
    {
        Context _c = new Context();
        public WriterValidationRegister()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("İsim Boş Bırakılamaz.").MinimumLength(6).WithMessage("Lütfen tam isminizi giriniz");
            RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Şifre Boş Bırakılamaz.")
    .MinimumLength(8).WithMessage("Şifre en az 8 karakterden oluşmalıdır.")
    .Matches("[A-Z]+").WithMessage("Şifre en az 1 büyük karakter içermelidir")
    .Matches("[a-z]+").WithMessage("'Şifre en az 1 küçük karakter içermelidir")
    .Matches(@"(\d)+").WithMessage("Şifre en az 1 sayı içermelidir")
    .Matches("(?!.*[£# “”])").WithMessage("Şifre £ # “” veya boşluk karakterlerini içeremez.");
            RuleFor(x => x.WriterMail).NotEmpty().WithMessage("Mail Boş Bırakılamaz.").Must(UniqueMail).WithMessage("Email zaten kayıtlı.");


        }
        private bool UniqueMail(string mail)
        {

            var checkMail = _c.Writers.Where(x => x.WriterMail.ToLower() == mail.ToLower()).SingleOrDefault();

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

    public class WriterValidationPasswordChange : AbstractValidator<Writer>
    {

        public WriterValidationPasswordChange()
        {
            RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Şifre Boş Bırakılamaz.")
   .MinimumLength(8).WithMessage("Şifre en az 8 karakterden oluşmalıdır.")
   .Matches("[A-Z]+").WithMessage("Şifre en az 1 büyük karakter içermelidir")
   .Matches("[a-z]+").WithMessage("'Şifre en az 1 küçük karakter içermelidir")
   .Matches(@"(\d)+").WithMessage("Şifre en az 1 sayı içermelidir")
   .Matches("(?!.*[£# “”])").WithMessage("Şifre £ # “” veya boşluk karakterlerini içeremez.");


        }


    }
    public class WriterValidationEmailChange : AbstractValidator<Writer>
    {
        Context _c = new Context();
        public WriterValidationEmailChange()
        {
            RuleFor(x => x.WriterMail).NotEmpty().WithMessage("Mail Boş Bırakılamaz.").Must(UniqueMail).WithMessage("Email zaten kayıtlı.");


        }
        private bool UniqueMail(string mail)
        {

            var checkMail = _c.Writers.Where(x => x.WriterMail.ToLower() == mail.ToLower()).SingleOrDefault();

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
