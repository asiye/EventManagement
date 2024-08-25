using AutoMapper;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Entities;
using EventManagement.Domain.Interfaces;
using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EventManagement.Application.Services;
public class EventService : IEventService
{
    private readonly IGenericRepository<Event> _repository;
    private readonly IMapper _mapper;

    public EventService(IGenericRepository<Event> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EventDto> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<EventDto>(entity); // Map Event to EventDto
    }

    public async Task<IEnumerable<EventDto>> GetAllAsync()
    {
        var events = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<EventDto>>(events); // Map list of Events to list of EventDtos
    }

    public async Task CreateAsync(EventDto eventDto)
    {
        var entity = _mapper.Map<Event>(eventDto); // Map EventDto to Event

        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(EventDto eventDto)
    {
        var entity = _mapper.Map<Event>(eventDto); // Map EventDto to Event
        _repository.Update(entity);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        _repository.Delete(entity);
        await _repository.SaveChangesAsync();

    }
}
