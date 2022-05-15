using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
   public class PageVisit
    {
        [Key]
        public int PageID { get; set; }
        public string PageUrl { get; set; }
        public int Visits { get; set; }
    }
}
