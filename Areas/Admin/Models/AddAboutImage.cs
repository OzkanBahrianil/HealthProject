using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Areas.Admin.Models
{
    public class AddAboutImage
    {
        public int AboutID { get; set; }
        public string AboutDetails { get; set; }
        public IFormFile AboutImage { get; set; }
        public string AboutImageString { get; set; }
        public string AboutMapLocation { get; set; }
        public bool Status { get; set; }
    }
}
