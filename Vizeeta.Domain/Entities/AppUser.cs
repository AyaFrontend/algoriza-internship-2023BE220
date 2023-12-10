using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizeeta.Domain.Enums;

namespace Vizeeta.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Password { set; get; }
        public string FName { set; get; }
        public string LName { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string UserType { set; get; }

        [ForeignKey("Doctor")]
        public string DoctorId { set; get; }
        public virtual Doctor Doctor { set; get; }

        [ForeignKey("Patient")]
        public string PatientId { set; get; }
        public virtual Patient Patient { set; get; }

    }
}
