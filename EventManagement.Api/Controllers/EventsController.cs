using EventManagement.Api.Hubs;
using EventManagement.Application.DTOs;
using EventManagement.Application.Services;
using EventManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EventManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly IHubContext<EventHub> _hubContext;

    public EventsController(IEventService eventService, IHubContext<EventHub> hubContext)
    {
        this._eventService = eventService;
        this._hubContext = hubContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents()
    {
        var data = await _eventService.GetAllAsync();

        if (data == null)
        {
            return NotFound();
        }

        return Ok(data);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<EventDto>> GetEvent(Guid id)
    {
        var eventDto = await _eventService.GetByIdAsync(id);
        if (eventDto == null)
        {
            return NotFound();
        }
        return Ok(eventDto);
    }

    [HttpPost]
    public async Task<ActionResult> CreateEvent(EventDto eventDto)
    {
        await _eventService.CreateAsync(eventDto);

        // Notify clients about the new event
        await _hubContext.Clients.All.SendAsync("ReceiveUpdate", "A new event has been created.");

        return CreatedAtAction(nameof(GetEvent), new { id = eventDto.Id }, eventDto);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateEvent(EventDto eventDto)
    {
        await _eventService.UpdateAsync(eventDto);

        await _hubContext.Clients.All.SendAsync("ReceiveUpdate", "An event has been updated.");

        return CreatedAtAction(nameof(GetEvent), new { id = eventDto.Id }, eventDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEvent(Guid id)
    {
        await _eventService.DeleteAsync(id);

        await _hubContext.Clients.All.SendAsync("ReceiveUpdate", "An event has been deleted.");
        return Ok();
    }
}

