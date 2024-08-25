using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Domain.Interfaces;
public interface ITransactionRecordRepository : IGenericRepository<TransactionRecord>
{
    Task<IEnumerable<TransactionRecord>> GetTransactionsByAddressAsync(string address);
}

