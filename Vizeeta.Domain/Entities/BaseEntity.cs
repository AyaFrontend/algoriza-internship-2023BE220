using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizeeta.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public string Id { set; get; } = Guid.NewGuid().ToString();
    }
}
