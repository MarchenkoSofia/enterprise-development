using AutoMapper;
using BikeRental.Domain.Models;
using BikeRental.Application.Contracts.Model;
using BikeRental.Application.Contracts.Bike;
using BikeRental.Application.Contracts.Rent;
using BikeRental.Application.Contracts.Renter;

namespace BikeRental.Application;

/// <summary>
/// AutoMapper-профиль для преобразования между доменными сущностями и DTO.
/// </summary>
public class BikeRentalProfile : Profile
{
    public BikeRentalProfile()
    {
        //
        // МОДЕЛИ ВЕЛОСИПЕДОВ
        //
        CreateMap<Model, ModelDto>();
        CreateMap<ModelCreateUpdateDto, Model>();


        //
        // ВЕЛОСИПЕДЫ
        //
        CreateMap<Bike, BikeDto>();
        CreateMap<BikeCreateUpdateDto, Bike>();


        //
        // АРЕНДЫ
        //
        CreateMap<Rent, RentDto>();
        CreateMap<RentCreateUpdateDto, Rent>();


        //
        // АРЕНДАТОРЫ
        //
        CreateMap<Renter, RenterDto>();
        CreateMap<RenterCreateUpdateDto, Renter>();
    }
}
