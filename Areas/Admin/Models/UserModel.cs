using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthProject.Areas.Admin.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string About { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }

    }
}
