using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
   public class WriterApplication
    {
        [Key]
        public int ApplicationID { get; set; }
        public string ApplicationCV { get; set; }
        public string ApplicationCoverLetter { get; set; }
        public string ApplicationUniversity { get; set; }
        public string ApplicationUniversityDepartment { get; set; }
        public DateTime ApplicationDate { get; set; }
        public bool ApplicationStatus { get; set; }
        public int UserID { get; set; }
        public AppUser User { get; set; }
    }
}
