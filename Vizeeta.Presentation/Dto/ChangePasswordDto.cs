﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vizeeta.Presentation.Dto
{
    public class ChangePasswordDto
    {
        [Required]
        public string Password { set; get; }

        [Required]
        public string Token { set; get; }

        [Required]
        public string Email { set; get; }
    }
}
