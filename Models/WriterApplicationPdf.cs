using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Models
{
    public class WriterApplicationPdf
    {
        public int ApplicationID { get; set; }
        public IFormFile ApplicationCV { get; set; }
        public string ApplicationCoverLetter { get; set; }
        public string ApplicationUniversity { get; set; }
        public string ApplicationUniversityDepartment { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime ApplicationDate { get; set; }
        public bool ApplicationStatus { get; set; }
        public int UserID { get; set; }

    }
}
