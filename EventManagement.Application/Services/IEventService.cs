using EventManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Services;
public interface IEventService
{
    public Task<EventDto> GetEventByIdAsync(int id);
    public Task<IEnumerable<EventDto>> GetAllEventsAsync();
    public Task CreateEventAsync(EventDto eventDto);
    public Task UpdateEventAsync(EventDto eventDto);
    public Task DeleteEventAsync(int id);
}