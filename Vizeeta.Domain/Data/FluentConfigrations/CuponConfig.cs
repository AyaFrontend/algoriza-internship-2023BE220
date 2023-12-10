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
    public class CuponConfig : IEntityTypeConfiguration<Cupone>
    {
        public void Configure(EntityTypeBuilder<Cupone> builder)
        {
            builder.Property(P => P.DiscoundType).HasConversion(
              cupon => cupon.ToString(),
              cupon => (CuponType)Enum.Parse(typeof(CuponType), cupon));
        }
    }
}
