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
   public class BlogValidation : AbstractValidator<Blog>
    {
        public BlogValidation()
        {
            RuleFor(x => x.BlogTitle).NotEmpty().WithMessage("Başlık Boş Bırakılamaz").MaximumLength(150).WithMessage("Başlık En Fazla 150 Karekter Yazılabilir").MinimumLength(10).WithMessage("Başlık En Az 10 Karekter Yazılabilir");
            RuleFor(x => x.BlogContent).NotEmpty().WithMessage("İçerik Boş Bırakılamaz").MinimumLength(300).WithMessage("İçerik En Az 300 Karekter Yazılabilir").MaximumLength(100000).WithMessage("Başlık En Fazla 100000 Karekter Yazılabilir");
            RuleFor(x => x.BlogShortContent).NotEmpty().WithMessage("Kısa İçerik Boş Bırakılamaz").MaximumLength(400).WithMessage("Kısa İçerik açıklama En Fazla 400 Karekter Yazılabilir").MinimumLength(160).WithMessage("İçerik kısa açıklama En Az 160 Karekter Yazılabilir");
      

        }


    }

}
