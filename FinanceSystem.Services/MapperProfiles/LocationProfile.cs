using AutoMapper;
using FinanceSystem.Abstractions.Models.Location;
using NetTopologySuite.Geometries;
using Location = FinanceSystem.Data.Entities.Location;

namespace FinanceSystem.Services.MapperProfiles;

public sealed class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<LocationDto, Location>()
            .ForMember(dest => dest.Coordinates,
                act => act.MapFrom(src => new Point(src.Coordinates.Longitude, src.Coordinates.Latitude)));
    }
}