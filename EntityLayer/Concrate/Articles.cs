using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
   public class Articles
    {
        [Key]
        public int ArticlesID { get; set; }
        public string ArticlesTitle { get; set; }
        public string ArticlesContent { get; set; }
        public string ArticlesShortContent { get; set; }
        public string ArticlesType { get; set; }
        public string ArticlesPdf { get; set; }
        public DateTime ArticlesPublishDate { get; set; }
        public string ArticlesWritersName { get; set; }
        public bool ArticlesStatus { get; set; }
        public int ArticleCategoryID { get; set; }
        public ArticleCategory ArticleCategory { get; set; }

        public int UserID { get; set; }
        public AppUser User { get; set; }
    }
}
