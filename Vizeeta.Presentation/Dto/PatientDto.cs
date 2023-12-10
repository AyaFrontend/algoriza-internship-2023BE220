using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vizeeta.Presentation.Dto
{
    public class PatientDto
    {
        public IFormFile Image { set; get; }
        public string FName { set; get; }
        public string LName { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
   
        public string Password { set; get; }
        public string Gender { set; get; }
    }
}
