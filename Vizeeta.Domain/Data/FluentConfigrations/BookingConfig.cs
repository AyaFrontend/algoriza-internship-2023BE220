using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizeeta.Domain.Entities;
using Vizeeta.Domain.Enums;

namespace Vizeeta.Domain.Data.FluentConfigrations
{
    public class BookingConfig : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(P => P.Id);
            builder.Property(P => P.BookingStatus).HasConversion(
                bookingstatus => bookingstatus.ToString(),
                bookingstatus => (BookingStatus)Enum.Parse(typeof(BookingStatus), bookingstatus));
            builder.HasOne(P => P.Doctor).WithMany(P => P.Booking).HasForeignKey(P => P.DoctorId);
            builder.HasOne(P => P.Patient).WithMany(P => P.Booking).HasForeignKey(P => P.PatientId);
        }
    }
}
