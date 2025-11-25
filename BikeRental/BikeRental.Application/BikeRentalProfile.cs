using AutoMapper;
using BikeRental.Domain.Models;
using BikeRental.Application.Contracts;

namespace BikeRental.Application;

/// <summary>
/// Профиль AutoMapper для приложения велопроката.
/// </summary>
public class BikeRentalProfile : Profile
{
    public BikeRentalProfile()
    {
        CreateMap<Model, ModelCreateUpdateDto>()
            .ForMember(dest => dest.ModelId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.TotalHours,
                opt => opt.MapFrom(src =>
                    src.Bikes.SelectMany(b => b.Rents).Sum(r => r.Duration)));
    }
}
