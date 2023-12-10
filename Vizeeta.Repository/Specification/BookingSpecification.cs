using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizeeta.Domain.Entities;
using Vizeeta.Domain.Enums;
namespace Vizeeta.Repository.Specification
{
    public class BookingSpecification : BaseSpecification<Booking>
    {
        public BookingSpecification(string TimeId):base(b=> b.TimeId == TimeId)
        {

        }
        public BookingSpecification(string DoctorId, int x) : base(b => b.DoctorId == DoctorId)
        {

        }

        public BookingSpecification(string PatientId,string bookingStatus) : base(b => b.PatientId == PatientId && b.BookingStatus.ToString() == bookingStatus)
        {

        }
    }
}
