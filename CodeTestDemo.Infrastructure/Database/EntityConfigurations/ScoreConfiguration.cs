using CodeTestDemo.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTestDemo.Infrastructure.Database.EntityConfigurations
{
    public class ScoreConfiguration : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            builder.Property(x => x.TeamB).HasMaxLength(50);
            builder.Property(x => x.TeamA).HasMaxLength(50);
        }
    }
}