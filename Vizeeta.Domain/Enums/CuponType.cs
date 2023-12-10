using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Vizeeta.Domain.Enums
{
    public enum CuponType
    {
        [EnumMember(Value = "Active")]
        Active,
        [EnumMember(Value = "Deactive")]
        Deactive
    }
}
