using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
  public  class AppUser: IdentityUser<int>
    {
        public string NameSurname { get; set; }
        public string Image { get; set; }
        public string About { get; set; }
        public string VideoUrl { get; set; }
        public bool Status { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<WriterApplication> WriterApplications { get; set; }
        public List<MedicalProduct> MedicalProduct { get; set; }
        public List<Articles> Articles { get; set; }


        public virtual ICollection<Message> WriterSender { get; set; }
        public virtual ICollection<Message> WriterReceiver { get; set; }
    }
}
