using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Vizeeta.Domain.Enums
{
    public enum  Gender
    {
        [EnumMember(Value = "Mail")]
        Mail,

        [EnumMember(Value = "Femail")]
        Femail
    }
}
