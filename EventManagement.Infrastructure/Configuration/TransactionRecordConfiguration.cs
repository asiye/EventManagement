using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using EventManagement.Domain.Entities;
using EventManagement.Infrastructure.Convertor;

namespace EventManagement.Infrastructure.Configuration;
internal class TransactionRecordConfiguration : BaseEntityConfiguration<TransactionRecord>
{
    public override void Configure(EntityTypeBuilder<TransactionRecord> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.TranasactionHash)
               .IsRequired();

        builder.Property(e => e.BlockHash)
               .IsRequired();

        var bigIntegerConverter = new BigIntegerToStringConverter();


        builder.Property(e => e.BlockNumber)
            .IsRequired().HasConversion(bigIntegerConverter);

        builder.Property(e => e.From)
            .IsRequired();

        builder.Property(e => e.To)
            .IsRequired();

        builder.Property(e => e.Value)
            .IsRequired().HasConversion(bigIntegerConverter);
    }
}

