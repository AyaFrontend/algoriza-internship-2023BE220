using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizeeta.Domain.Enums;

namespace Vizeeta.Domain.Entities
{
    public class Cupone : BaseEntity
    {

        [Column(TypeName = "nvarchar(24)")]
        public CuponType DiscoundType { set; get; } = CuponType.Active;
        public string DicoundCode { set; get; }
        public float Value { set; get; }

        public virtual ICollection<Booking> Bookings { set; get; } = new HashSet<Booking>();
    }
}
