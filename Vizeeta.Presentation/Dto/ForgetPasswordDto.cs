using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vizeeta.Presentation.Dto
{
    public class ForgetPasswordDto
    {
        [Required]
        public string Email { set; get; }
    }
}
