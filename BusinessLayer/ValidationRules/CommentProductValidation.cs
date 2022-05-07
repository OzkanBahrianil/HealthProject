﻿using EntityLayer.Concrate;
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
            RuleFor(x => x.CommentProductUserName).NotEmpty().WithMessage("Kullanıcı Adı Boş Bırakılamaz").MinimumLength(3).WithMessage("Kullanıcı Adı En Az 3 Karekter Yazılabilir").MaximumLength(40).WithMessage("Kullanıcı Adı En Fazla 40 Karakter İle Yazılır"); ;
            RuleFor(x => x.CommentProductUserName).NotEmpty().WithMessage("Mesaj İçeriği Boş Bırakılamaz").MinimumLength(20).WithMessage("Mesaj İçeriği En Az 20 Karekter Yazılabilir").MaximumLength(500).WithMessage("Mesaj İçeriği En Fazla 500 Karakter İle Yazılır"); ;
            RuleFor(x => x.CommentProductTitle).NotEmpty().WithMessage("Başlık Başlığı Boş Bırakılamaz").MinimumLength(3).WithMessage("Başlık En Az 3 Karekter Yazılabilir").MaximumLength(40).WithMessage("Başlık En Fazla 40 Karakter İle Yazılır");
        }
    }
}
