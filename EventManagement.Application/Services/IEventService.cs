using EventManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Services;
public interface IEventService
{
    public Task<EventDto> GetByIdAsync(Guid id);
    public Task<IEnumerable<EventDto>> GetAllAsync();
    public Task CreateAsync(EventDto eventDto);
    public Task UpdateAsync(EventDto eventDto);
    public Task DeleteAsync(Guid id);
}