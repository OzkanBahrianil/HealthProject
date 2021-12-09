using EntityLayer.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
   public class BlogValidation : AbstractValidator<Blog>
    {
        public BlogValidation()
        {
            RuleFor(x => x.BlogTitle).NotEmpty().WithMessage("Başlık Boş Bırakılamaz");
            RuleFor(x => x.BlogContent).NotEmpty().WithMessage("İçerik Boş Bırakılamaz");
            RuleFor(x => x.BlogImage).NotEmpty().WithMessage("Resim Boş Bırakılamaz");
            RuleFor(x => x.BlogTitle).MaximumLength(150).WithMessage("Başlık En Fazla 150 Karekter Yazılabilir");
            RuleFor(x => x.BlogTitle).MinimumLength(5).WithMessage("Başlık En Az 5 Karekter Yazılabilir");
        }
    }
}
