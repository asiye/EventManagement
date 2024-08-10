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
        var data = await _eventService.GetAllEventsAsync();

        if (data == null)
        {
            return NotFound();
        }

        return Ok(data);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<EventDto>> GetEvent(int id)
    {
        var eventDto = await _eventService.GetEventByIdAsync(id);
        if (eventDto == null)
        {
            return NotFound();
        }
        return Ok(eventDto);
    }

    [HttpPost]
    public async Task<ActionResult> CreateEvent(EventDto eventDto)
    {
        await _eventService.CreateEventAsync(eventDto);

        // Notify clients about the new event
        await _hubContext.Clients.All.SendAsync("ReceiveUpdate", "A new event has been created.");

        return CreatedAtAction(nameof(GetEvent), new { id = eventDto.Id }, eventDto);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateEvent(EventDto eventDto)
    {
        await _eventService.UpdateEventAsync(eventDto);

        await _hubContext.Clients.All.SendAsync("ReceiveUpdate", "An event has been updated.");

        return CreatedAtAction(nameof(GetEvent), new { id = eventDto.Id }, eventDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEvent(int id)
    {
        await _eventService.DeleteEventAsync(id);

        await _hubContext.Clients.All.SendAsync("ReceiveUpdate", "An event has been deleted.");
        return Ok();
    }
}

