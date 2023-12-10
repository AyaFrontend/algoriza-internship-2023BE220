using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizeeta.Domain.Entities;


namespace Vizeeta.Domain.Data.FluentConfigrations
{
    public class TimesConfig : IEntityTypeConfiguration<Times>
    {
        public void Configure(EntityTypeBuilder<Times> builder)
        {
            builder.HasKey(P => P.Id);
            builder.HasOne(P => P.Day).WithMany().HasForeignKey(P => P.DayId);
        }
    }
}
