using EntityLayer.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MedicalProductValidation : AbstractValidator<MedicalProduct>
    {
        public MedicalProductValidation()
        {
            RuleFor(x => x.ProductTitle).NotEmpty().WithMessage("Başlık Boş Bırakılamaz").MaximumLength(150).WithMessage("Başlık En Fazla 150 Karekter Yazılabilir").MinimumLength(10).WithMessage("Başlık En Az 10 Karekter Yazılabilir");
            RuleFor(x => x.ProductContent).NotEmpty().WithMessage("İçerik Boş Bırakılamaz").MinimumLength(300).WithMessage("İçerik En Az 300 Karekter Yazılabilir").MaximumLength(100000).WithMessage("İçerik En Fazla 100000 Karekter Yazılabilir");
            RuleFor(x => x.ProductCompanyWebsite).NotEmpty().WithMessage("Şirket Web Sitesi Boş Bırakılamaz").MaximumLength(500).WithMessage("Şirket Web Sitesi En Fazla 150 Karekter Yazılabilir");
            RuleFor(x => x.ProductStyle).NotEmpty().WithMessage("Ürün Tipi Bırakılamaz").MaximumLength(150).WithMessage("Ürün Tipi En Fazla 150 Karekter Yazılabilir");
            RuleFor(x => x.ProductRealiseDate).NotEmpty().WithMessage("Üretim Tarihi Boş Bırakılamaz");
            RuleFor(x => x.ProductShortContent).NotEmpty().WithMessage("Kısa İçerik Boş Bırakılamaz").MaximumLength(400).WithMessage("İçerik kısa açıklama En Fazla 400 Karekter Yazılabilir").MinimumLength(160).WithMessage("İçerik kısa açıklama En Az 160 Karekter Yazılabilir");


        }
    }
}
