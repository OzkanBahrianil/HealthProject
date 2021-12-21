using EntityLayer.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CommentValidation : AbstractValidator<Comment>
    {
        public CommentValidation()
        {
            RuleFor(x => x.CommentUserName).NotEmpty().WithMessage("Başlık Boş Bırakılamaz").MinimumLength(3).WithMessage("İsim En Az 3 Karekter Yazılabilir");
        }
    }
}
