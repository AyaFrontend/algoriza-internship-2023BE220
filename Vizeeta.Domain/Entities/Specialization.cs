using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizeeta.Domain.Entities
{
    public class Specialization : BaseEntity
    {
        public string SpecializeName { set; get; }
        public virtual ICollection<Doctor> Doctors { set; get; } = new HashSet<Doctor>();
    }
}
