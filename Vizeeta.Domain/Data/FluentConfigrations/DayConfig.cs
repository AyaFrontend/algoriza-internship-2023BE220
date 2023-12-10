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
    public class DayConfig : IEntityTypeConfiguration<Day>
    {
        public void Configure(EntityTypeBuilder<Day> builder)
        {
            builder.HasOne(P => P.AppoimentId).WithMany().HasForeignKey(P => P.AppoimentId);
            builder.Property(P => P.DayName).HasConversion(
          day => day.ToString(),
          day => (Days)Enum.Parse(typeof(Days), day));
         
        }
    }
}
