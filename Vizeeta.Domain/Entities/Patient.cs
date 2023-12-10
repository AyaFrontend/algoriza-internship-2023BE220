using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizeeta.Domain.Enums;

namespace Vizeeta.Domain.Entities
{
    public class Patient : BaseEntity
    {
        [ForeignKey("AppUser")]
        public string AppUserId { set; get; }
        public virtual AppUser AppUser { set; get; }

 
        public string ImageUrl { set; get; }
        public Gender Gender { set; get; }
        public virtual ICollection<Booking> Booking { set; get; } = new HashSet<Booking>();
    }
}
