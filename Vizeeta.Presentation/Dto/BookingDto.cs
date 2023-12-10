using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizeeta.Domain.Enums;
namespace Vizeeta.Presentation.Dto
{
    public class BookingDto
    {
        public string  BookingStatus { set; get; }
        public string CuponeId { set; get; }
        public string TimeId { set; get; }
        
        public string PatientId { set; get; }
     
        public string DoctorId { set; get; }
   
    }
}
