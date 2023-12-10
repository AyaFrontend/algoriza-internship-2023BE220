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
    public class SpecializationConfig : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasKey(P => P.Id);
            builder.HasIndex(P => P.SpecializeName).IsUnique(true);
        }
    }
}
