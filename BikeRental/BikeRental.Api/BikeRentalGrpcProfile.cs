using AutoMapper;
using BikeRental.Domain.Enum;
using BikeRental.Domain.Models;
using Bikes.Contracts.Grpc;

namespace BikeRental.Api
{
    /// <summary>
    /// AutoMapper profile for converting gRPC generated messages into domain entities.
    /// </summary>
    public class BikeRentalGrpcProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BikeRentalGrpcProfile"/> class and defines the mapping rules.
        /// </summary>
        public BikeRentalGrpcProfile()
        {
            CreateMap<GrpcBikeType, BikeType>();

            CreateMap<AddModelRequest, Model>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())

                .ForMember(dest => dest.PricePerHour, opt => opt.MapFrom(src => (decimal)src.PricePerHour))

                .ForMember(dest => dest.BrakeType, opt => opt.MapFrom(src => src.BrakeType.ToString()))

                .ForMember(dest => dest.BikeType, opt => opt.MapFrom(src => src.BikeType));
        }
    }
}
