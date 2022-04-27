using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Areas.Admin.Models
{
    public class MedicalProductsModel
    {
        public int ProductID { get; set; }
        public string ProductTitle { get; set; }
        public string ProductContent { get; set; }
        public string ProductShortContent { get; set; }
        public string ProductStyle { get; set; }
        public DateTime ProductRealiseDate { get; set; }
        public string ProductCompanyWebsite { get; set; }
        public bool ProductStatus { get; set; }
        public int ProductCategoryID { get; set; }
        public int UserID { get; set; }
    }
}
