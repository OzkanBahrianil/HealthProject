using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
  public  class MedicalProduct
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductTitle { get; set; }
        public string ProductContent { get; set; }
        public string ProductShortContent { get; set; }
        public string ProductStyle { get; set; }
        public DateTime ProductRealiseDate { get; set; }
        public string ProductThumbnailImage { get; set; }
        public string ProductImage { get; set; }
        public string ProductCompanyWebsite { get; set; }
        public bool ProductStatus { get; set; }


        public int ProductCategoryID { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public int UserID { get; set; }
        public AppUser User { get; set; }
        public List<CommentProduct> CommentProducts { get; set; }
    }
}
