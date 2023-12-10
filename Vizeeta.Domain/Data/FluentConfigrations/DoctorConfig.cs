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
    public class DoctorConfig :  IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(P => P.Id);
            builder.Property(P => P.ImageUrl).IsRequired();
            builder.Property(P => P.Gender).HasConversion(
                gender => gender.ToString(),
                gender => (Gender)Enum.Parse(typeof(Gender), gender)
            );
            builder.HasOne(P => P.Specialization).WithMany(P => P.Doctors)
              .HasForeignKey(P => P.SpecializationId);
        }
    }
}
