using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
   public class About
    {   [Key]
        public int AboutID { get; set; }
        public string AboutDetails { get; set; }
        public string AboutImage { get; set; }
        public string AboutMapLocation { get; set; }
        public bool Status { get; set; }
    }
}
