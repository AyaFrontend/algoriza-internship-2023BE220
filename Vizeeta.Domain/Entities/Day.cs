using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizeeta.Domain.Enums;

namespace Vizeeta.Domain.Entities
{
    public class Day : BaseEntity
    {
        [Column(TypeName = "nvarchar(24)")]
        public Days DayName { set; get; }
        public string AppoimentId { set; get; }
        public virtual Appoinment Appoiment { set; get; } 
      public virtual ICollection<Times> Times { set; get; } = new HashSet<Times>();
    }
}
