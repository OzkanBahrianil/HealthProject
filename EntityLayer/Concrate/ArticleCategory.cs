using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
   public class ArticleCategory
    {
        [Key]
        public int ArticleCategoryID { get; set; }
        public string ArticleCategoryName { get; set; }
        public string ArticleCategoryDescription { get; set; }
        public bool ArticleCategoryStatus { get; set; }
        public List<Articles> Articles { get; set; }
    }
}
