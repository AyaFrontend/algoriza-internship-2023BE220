using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizeeta.Domain.Entities
{
    public class Admin : BaseEntity
    {
        public string UserName { set; get; }
        public string Password { set; get; }
    }
}
