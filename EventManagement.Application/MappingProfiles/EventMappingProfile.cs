using AutoMapper;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.MappingProfiles;
public class EventMappingProfile : Profile
{
    public EventMappingProfile()
    {
        CreateMap<Event, EventDto>().ReverseMap();
    }
}
