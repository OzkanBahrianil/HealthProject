using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
   public class ProductCategory
    {
        [Key]
        public int ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductCategoryDescription { get; set; }
        public bool ProductCategoryStatus { get; set; }
        public List<MedicalProduct> MedicalProducts { get; set; }
    }

}
