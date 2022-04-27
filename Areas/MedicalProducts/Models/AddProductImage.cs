using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Areas.MedicalProducts.Models
{
    public class AddProductImage
    {
        public int ProductID { get; set; }
        public string ProductTitle { get; set; }
        public string ProductContent { get; set; }
        public string ProductShortContent { get; set; }
        public string ProductStyle { get; set; }
        public DateTime ProductRealiseDate { get; set; }
        public IFormFile ProductThumbnailImage { get; set; }
        public string ProductThumbnailImageString { get; set; }
        public IFormFile ProductImage { get; set; }
        public string ProductImageString { get; set; }
        public string ProductCompanyWebsite { get; set; }
        public bool ProductStatus { get; set; }
        public int ProductCategoryID { get; set; }
        public int CompanyID { get; set; }


    }
}
