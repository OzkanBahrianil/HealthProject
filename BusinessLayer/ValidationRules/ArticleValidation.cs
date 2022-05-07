using EntityLayer.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
  public  class ArticleValidation : AbstractValidator<Articles>
    {
        public ArticleValidation()
        {
            RuleFor(x => x.ArticlesTitle).NotEmpty().WithMessage("Başlık Boş Bırakılamaz").MaximumLength(150).WithMessage("Başlık En Fazla 150 Karekter Yazılabilir").MinimumLength(10).WithMessage("Başlık En Az 10 Karekter Yazılabilir");
            RuleFor(x => x.ArticlesContent).NotEmpty().WithMessage("İçerik Boş Bırakılamaz").MinimumLength(300).WithMessage("İçerik En Az 300 Karekter Yazılabilir").MaximumLength(100000).WithMessage("İçerik En Fazla 100000 Karekter Yazılabilir");
            RuleFor(x => x.ArticlesWritersName).NotEmpty().WithMessage("Yazar İsmi Boş Bırakılamaz").MaximumLength(150).WithMessage("Yazar İsmi En Fazla 150 Karekter Yazılabilir");
            RuleFor(x => x.ArticlesType).NotEmpty().WithMessage("Makale Tipi Bırakılamaz").MaximumLength(150).WithMessage("Makale Tipi En Fazla 150 Karekter Yazılabilir");
            RuleFor(x => x.ArticlesPublishDate).NotEmpty().WithMessage("İçerik Boş Bırakılamaz");
            RuleFor(x => x.ArticlesShortContent).NotEmpty().WithMessage("Kısa İçerik Boş Bırakılamaz").MaximumLength(1000).WithMessage("İçerik kısa açıklama En Fazla 400 Karekter Yazılabilir").MinimumLength(160).WithMessage("İçerik kısa açıklama En Az 160 Karekter Yazılabilir");
        }
    }
}
