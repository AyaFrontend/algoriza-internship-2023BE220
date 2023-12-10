using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizeeta.Domain.Entities
{
    public class Times : BaseEntity
    {
     
        public string DayId { set; get; }
        public virtual Day Day { set; get; }
        public DateTime Time {set;get;}
    }
}
