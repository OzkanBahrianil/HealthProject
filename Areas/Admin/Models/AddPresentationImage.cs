using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Areas.Admin.Models
{
    public class AddPresentationImage
    {

        public int PresentationID { get; set; }
        public string PresentationTitle { get; set; }
        public string PresentationDetails { get; set; }
        public string PresentationShortDetails { get; set; }
        public bool PresentationStatus { get; set; }
        public IFormFile PresentationImage { get; set; }
        public string PresentationImageString { get; set; }
    }
}
