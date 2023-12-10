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
    public class AdminConfig : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(P => P.Id);
            builder.Property(P => P.UserName).IsRequired();
            builder.HasIndex(P => P.UserName).IsUnique(true);
        }
    }
}
