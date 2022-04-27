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
    public class WriterValidation : AbstractValidator<AppUser>
    {

        public WriterValidation()
        {
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("İsim Boş Bırakılamaz").MinimumLength(6).WithMessage("Lütfen tam isminizi giriniz.");
            RuleFor(x => x.VideoUrl).NotEmpty().WithMessage("Video Boş Bırakılamaz");
            RuleFor(x => x.About).NotEmpty().WithMessage("Hakkında Boş Bırakılamaz");


        }


    }
    public class WriterValidationRegister : AbstractValidator<AppUser>
    {

        public WriterValidationRegister()
        {
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("İsim Boş Bırakılamaz.").MinimumLength(6).WithMessage("Lütfen tam isminizi giriniz");
            RuleFor(x => x.PasswordHash).NotEmpty().WithMessage("Şifre Boş Bırakılamaz.")
    .MinimumLength(8).WithMessage("Şifre en az 8 karakterden oluşmalıdır.")
    .Matches("[A-Z]+").WithMessage("Şifre en az 1 büyük karakter içermelidir")
    .Matches("[a-z]+").WithMessage("'Şifre en az 1 küçük karakter içermelidir")
    .Matches(@"(\d)+").WithMessage("Şifre en az 1 sayı içermelidir")
    .Matches("(?!.*[£# “”])").WithMessage("Şifre £ # “” veya boşluk karakterlerini içeremez.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail Boş Bırakılamaz");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen Geçerli Bir Mail Adresi Giriniz").Must(UniqueMail).WithMessage("Email zaten kayıtlı");


        }
        private bool UniqueMail(string mail)
        {
            Context _c = new Context();

            var checkMail = _c.Users.Where(x => x.Email.ToLower() == mail.ToLower()).FirstOrDefault();

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

    public class WriterValidationPasswordChange : AbstractValidator<AppUser>
    {

        public WriterValidationPasswordChange()
        {
            RuleFor(x => x.PasswordHash).NotEmpty().WithMessage("Şifre Boş Bırakılamaz.")
   .MinimumLength(8).WithMessage("Şifre en az 8 karakterden oluşmalıdır.")
   .Matches("[A-Z]+").WithMessage("Şifre en az 1 büyük karakter içermelidir")
   .Matches("[a-z]+").WithMessage("'Şifre en az 1 küçük karakter içermelidir")
   .Matches(@"(\d)+").WithMessage("Şifre en az 1 sayı içermelidir")
   .Matches("(?!.*[£# “”])").WithMessage("Şifre £ # “” veya boşluk karakterlerini içeremez.");


        }


    }
    public class WriterValidationEmailChange : AbstractValidator<AppUser>
    {
        Context _c = new Context();
        public WriterValidationEmailChange()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail Boş Bırakılamaz.").Must(UniqueMail).WithMessage("Email zaten kayıtlı.");


        }
        private bool UniqueMail(string mail)
        {

            var checkMail = _c.Users.Where(x => x.Email.ToLower() == mail.ToLower()).SingleOrDefault();

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
