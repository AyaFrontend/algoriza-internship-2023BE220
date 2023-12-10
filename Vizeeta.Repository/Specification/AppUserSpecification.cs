using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizeeta.Domain.Entities;

namespace Vizeeta.Repository.Specification
{
    public class AppUserSpecification : BaseSpecification<AppUser>
    {
        public AppUserSpecification(string doctorId): base(P=> P.DoctorId == doctorId)
        {

        }
    }
}
