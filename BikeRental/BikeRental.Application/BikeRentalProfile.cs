using AutoMapper;
using BikeRental.Application.Contracts.Bike;
using BikeRental.Application.Contracts.Model;
using BikeRental.Application.Contracts.Rent;
using BikeRental.Application.Contracts.Renter;
using BikeRental.Domain.Models;

namespace BikeRental.Application;

/// <summary>
/// AutoMapper profile for converting between domain entities and DTOs.
/// </summary>
public class BikeRentalProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BikeRentalProfile"/> class.
    /// Configures mappings for Models, Bikes, Rents, and Renters.
    /// </summary>
    public BikeRentalProfile()
    { 

        // Bike Models

        CreateMap<Model, ModelDto>();
        CreateMap<ModelCreateUpdateDto, Model>();


        // Bikes

        CreateMap<Bike, BikeDto>();
        CreateMap<BikeCreateUpdateDto, Bike>();


        // Rents

        CreateMap<Rent, RentDto>();
        CreateMap<RentCreateUpdateDto, Rent>();


        // Renters

        CreateMap<Renter, RenterDto>();
        CreateMap<RenterCreateUpdateDto, Renter>();
    }
}
