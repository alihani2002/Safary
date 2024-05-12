using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.DTOs;

namespace Safary.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Blog 

            CreateMap<Blog,BlogDTO>().ReverseMap();


            CreateMap<BlogPostDTO,Blog>().ReverseMap();
            CreateMap<Blog, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Title));

            //Tour


            CreateMap<TourDay, TourDayDTO>();
            CreateMap<TourDayPostDTO, TourDay>().ReverseMap();
            CreateMap<TourDay, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));
            CreateMap<TourHour, TourHourDTO>();
            CreateMap<TourHourPostDTO, TourHour>().ReverseMap();
            CreateMap<TourHour, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

            CreateMap<Tour, TourDTO>();
            CreateMap<TourPostDTO, Tour>().ReverseMap();
            CreateMap<Tour, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));
            // user 
            CreateMap<UserDTO, ApplicationUser>().ReverseMap();


            CreateMap<Tour, TourDTO>().ReverseMap();



        } 
    }
}
