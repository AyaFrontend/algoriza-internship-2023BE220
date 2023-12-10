using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizeeta.Domain.Enums;

namespace Vizeeta.Domain.Entities
{
    public class Booking : BaseEntity
    {
        [Column(TypeName = "nvarchar(24)")]
        public BookingStatus BookingStatus { set; get; } = BookingStatus.Pending;
        [ForeignKey("Cupone")]
        public string CuponeId { set; get; }
        public virtual Cupone Cupon { set; get; }
        public string TimeId { set; get; }
        public virtual Times Time {set; get;}
        public string PatientId { set; get; }
        public virtual Patient Patient { set; get; }
        public string DoctorId { set; get; }
        public virtual Doctor Doctor { set; get; }

    }
}
