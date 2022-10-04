using AutoMapper;
using SundownBoulevard.DTO;
using SundownBoulevard.Entities;
using SundownBoulevard.Models;

namespace SundownBoulevard.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<MealDTO, Meal>();
            CreateMap<Meal, MealDTO>();
            CreateMap<Booking, BookingDTO>();
            CreateMap<BookingDTO, Booking>();
        }
    }
}