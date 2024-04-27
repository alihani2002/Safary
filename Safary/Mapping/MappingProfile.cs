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
            CreateMap<Blog,BlogDTO>();
            CreateMap<BlogPostDTO,Blog>().ReverseMap();
            CreateMap<Blog, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Title));

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
        } 
    }
}
