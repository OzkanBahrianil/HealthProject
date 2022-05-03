using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
  public  class Presentation
    {
        [Key]
        public int PresentationID { get; set; }
        public string PresentationTitle { get; set; }
        public string PresentationShortDetails { get; set; }
        public string PresentationDetails { get; set; }
        public bool PresentationStatus { get; set; }
        public string PresentationImage { get; set; }
   
    }
}

