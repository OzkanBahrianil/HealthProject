using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Areas.Article.Models
{
    public class AddArticlesPdf
    {
        public int ArticlesID { get; set; }
        public string ArticlesTitle { get; set; }
        public string ArticlesContent { get; set; }
        public string ArticlesShortContent { get; set; }
        public string ArticlesType { get; set; }
        public IFormFile ArticlesPdf { get; set; }
        public string ArticlesPdfString { get; set; }
        public DateTime ArticlesPublishDate { get; set; }
        public string ArticlesWritersName { get; set; }
        public bool ArticlesStatus { get; set; }
        public int ArticleCategoryID { get; set; }
        public int WriterID { get; set; }

    }
}
