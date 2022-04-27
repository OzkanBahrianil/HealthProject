using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Models
{
    public class AddBlogImage
    {
        public int BlogID { get; set; }
        public string BlogTitle { get; set; }
        public string BlogShortContent { get; set; }
        public string BlogContent { get; set; }
        public DateTime BlogCreateDate { get; set; }
        public IFormFile BlogThumbnailImage { get; set; }
        public string BlogThumbnailImageString { get; set; }
        public IFormFile BlogImage { get; set; }
        public string BlogImageString { get; set; }
        public bool BlogStatus { get; set; }
        public int CategoryID { get; set; }
        public int WriterID { get; set; }
  
  
    }
}
