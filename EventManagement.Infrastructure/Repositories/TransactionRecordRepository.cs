using EventManagement.Domain.Entities;
using EventManagement.Domain.Interfaces;
using EventManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Repositories;
public class TransactionRecordRepository : GenericRepository<TransactionRecord>, ITransactionRecordRepository
{
    public TransactionRecordRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<TransactionRecord>> GetTransactionsByAddressAsync(string address)
    {
        return await _dbSet
            .Where(t => t.From.Equals(address, StringComparison.OrdinalIgnoreCase) ||
                        t.To.Equals(address, StringComparison.OrdinalIgnoreCase))
            .ToListAsync();
    }
}
