using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Vizeeta.Domain.Enums
{
    public enum Days
    {
        [EnumMember(Value = "Satrday")]
        Satrday,
        [EnumMember(Value = "Sunday")]
        Sunday,
        [EnumMember(Value = "Monday")]
        Monday,
        [EnumMember(Value = "Tuesday")]
        Tuesday,
        [EnumMember(Value = "Wensday")]
        Wensday,
        [EnumMember(Value = "Thrusday")]
        Thrusday,
        [EnumMember(Value = "Friday")]
        Friday
    }
}
