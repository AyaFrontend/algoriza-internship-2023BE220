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
    public class Doctor : BaseEntity
    {
       
        public virtual AppUser AppUser { set; get; }
        public string ImageUrl { set; get; }
        public Gender Gender { set; get; }
        public string SpecializationId { set; get; }
        public virtual Specialization Specialization{ set; get; }
        public virtual ICollection<Booking> Booking { set; get; } = new HashSet<Booking>();
        public virtual ICollection<Appoinment> Appoinments { set; get; } = new HashSet<Appoinment>();

}
}
