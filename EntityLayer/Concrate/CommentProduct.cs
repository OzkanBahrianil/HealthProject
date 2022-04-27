using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
   public class CommentProduct
    {
        [Key]
        public int CommentProductID { get; set; }
        public string CommentProductUserName { get; set; }
        public DateTime CommentProductDate { get; set; }
        public string CommentProductContent { get; set; }
        public string CommentProductTitle { get; set; }
        public bool CommentProductStatus { get; set; }
        public int ProductID { get; set; }
        public MedicalProduct Product { get; set; }
    }
}
