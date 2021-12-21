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
            RuleFor(x => x.BlogTitle).NotEmpty().WithMessage("Başlık Boş Bırakılamaz");
            //RuleFor(x => x.BlogContent).NotEmpty().WithMessage("İçerik Boş Bırakılamaz").MinimumLength(200).WithMessage("İçerik En Az 200 Karekter Yazılabilir");
            //RuleFor(x => x.BlogTitle).MaximumLength(150).WithMessage("Başlık En Fazla 150 Karekter Yazılabilir").MinimumLength(10).WithMessage("Başlık En Az 10 Karekter Yazılabilir");
           
        }

      
    }
}
