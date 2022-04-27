﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Models
{
    public class UpdatePasswordViewModel
    {
       
            [Display(Name = "Yeni Şifre")]
            [Required(ErrorMessage = "Lütfen şifreyi boş geçmeyiniz.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        
    }
}
