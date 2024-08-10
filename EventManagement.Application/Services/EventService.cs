using AutoMapper;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Entities;
using EventManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Services;
public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public EventService(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<EventDto> GetEventByIdAsync(int id)
    {
        var eventEntity = await _eventRepository.GetByIdAsync(id);
        return _mapper.Map<EventDto>(eventEntity); // Map Event to EventDto
    }

    public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
    {
        var events = await _eventRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EventDto>>(events); // Map list of Events to list of EventDtos
    }

    public async Task CreateEventAsync(EventDto eventDto)
    {
        var eventEntity = _mapper.Map<Event>(eventDto); // Map EventDto to Event

        await _eventRepository.AddAsync(eventEntity);

    }

    public async Task UpdateEventAsync(EventDto eventDto)
    {
        var eventEntity = _mapper.Map<Event>(eventDto); // Map EventDto to Event
        await _eventRepository.UpdateAsync(eventEntity);
    }

    public async Task DeleteEventAsync(int id)
    {
        await _eventRepository.DeleteAsync(id);
    }
}
