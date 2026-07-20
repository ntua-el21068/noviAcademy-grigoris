using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Domain.Entities;

namespace WorldRank.Infrastructure.Persistence.Configurations
{
    public class CurrencyRateConfiguration : IEntityTypeConfiguration<CurrencyRate>
    {
        public void Configure(EntityTypeBuilder<CurrencyRate> builder)
        {
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Currency).IsRequired().HasMaxLength(3);
            builder.Property(cr => cr.Rate).IsRequired().HasColumnType("decimal(18,6)");
            builder.Property(cr => cr.Date).IsRequired();
            builder.HasIndex(cr => new { cr.Currency, cr.Date }).IsUnique();

        }
    }
}
