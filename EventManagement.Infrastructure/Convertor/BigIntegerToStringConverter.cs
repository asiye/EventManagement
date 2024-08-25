using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Convertor;
public class BigIntegerToStringConverter : ValueConverter<BigInteger, string>
{
    public BigIntegerToStringConverter() : base(
        v => v.ToString(),
        v => BigInteger.Parse(v))
    {
    }
}
