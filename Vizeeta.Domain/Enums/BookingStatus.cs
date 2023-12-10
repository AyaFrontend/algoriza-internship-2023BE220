using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Vizeeta.Domain.Enums
{
    public enum BookingStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Complete")]
        Complete,
        [EnumMember(Value = "Cancelled")]
        Cancelled
    }
}
