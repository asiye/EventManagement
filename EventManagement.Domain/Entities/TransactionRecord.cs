using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Domain.Entities;
public class TransactionRecord : BaseEntity
{
    public string TranasactionHash { get; set; }
    public string BlockHash { get; set; }
    public BigInteger BlockNumber { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public BigInteger Value { get; set; }
}
