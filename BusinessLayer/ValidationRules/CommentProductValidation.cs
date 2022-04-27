using EntityLayer.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
   public class CommentProductValidation : AbstractValidator<CommentProduct>
    {
        public CommentProductValidation()
        {
            RuleFor(x => x.CommentProductUserName).NotEmpty().WithMessage("Başlık Boş Bırakılamaz").MinimumLength(3).WithMessage("İsim En Az 3 Karekter Yazılabilir");
        }
    }
}
