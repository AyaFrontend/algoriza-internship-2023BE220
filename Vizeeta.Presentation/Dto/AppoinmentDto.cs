using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizeeta.Domain.Entities;

namespace Vizeeta.Presentation.Dto
{
    public class AppoinmentDto
    {
        public string DoctorId { set; get; }
        public float Price { set; get; }
        public bool IsBooked { set; get; } = false;
        public  ICollection<Day> Days { set; get; } 
    }
}
