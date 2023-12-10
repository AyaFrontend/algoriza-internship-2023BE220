using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizeeta.Domain.Enums;

namespace Vizeeta.Domain.Entities
{
    public class Appoinment : BaseEntity
    {
        public float Price { set; get; }
      
        public bool IsBooked { set; get; } = false;
        public virtual ICollection<Day> Days { set; get; } = new HashSet<Day>();
        public string DoctorId { set; get; }
        public virtual Doctor Doctor { set; get; }

    }
}
