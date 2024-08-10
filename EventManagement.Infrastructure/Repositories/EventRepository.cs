using EventManagement.Domain.Entities;
using EventManagement.Domain.Interfaces;
using EventManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Repositories;
public class EventRepository : IEventRepository
{
    private readonly ApplicationDbContext _context;

    public EventRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Event>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Event> GetByIdAsync(int id)
    {
        return await _context.Events.FindAsync(id);
    }

    Task IEventRepository.AddAsync(Event eve)
    {
        throw new NotImplementedException();
    }

    Task IEventRepository.DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<Event>> IEventRepository.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    Task<Event> IEventRepository.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task IEventRepository.UpdateAsync(Event eve)
    {
        throw new NotImplementedException();
    }
}