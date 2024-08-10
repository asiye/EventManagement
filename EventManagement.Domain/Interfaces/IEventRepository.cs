using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Domain.Interfaces;
public interface IEventRepository
{
    Task<Event> GetByIdAsync(int id);
    Task<IEnumerable<Event>> GetAllAsync();
    Task AddAsync(Event eve);
    Task UpdateAsync(Event eve);
    Task DeleteAsync(int id);
}