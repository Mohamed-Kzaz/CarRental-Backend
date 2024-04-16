using AutoMapper;
using CarRental.APIs.DTOs.Car;
using CarRental.Core.Entities;

namespace CarRental.APIs.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CarDto, Car>();
                //.ForMember(dest => dest.CarImageURL, opt => opt.MapFrom(src => src.CarImage));

        }
    }
}
