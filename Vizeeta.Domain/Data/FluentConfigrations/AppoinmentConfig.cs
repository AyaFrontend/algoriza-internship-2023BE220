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
    public class AppoinmentConfig : IEntityTypeConfiguration<Appoinment>
    {
        public void Configure(EntityTypeBuilder<Appoinment> builder)
        {
            builder.HasKey(P => P.Id);
           
            builder.HasOne(P => P.Doctor).WithMany(P => P.Appoinments).HasForeignKey(P => P.DoctorId);

        }
    }
}
