using AutoMapper;
using Domain.Entities;
using Domain.Models;
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

            //Places 
            CreateMap<Place, PlaceDTO>();
            CreateMap<PlacePostDTO, Place>().ReverseMap();
            CreateMap<Place, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));
            //Cities 
            CreateMap<City, CityDTO>();
            CreateMap<CityPostDTO, City>().ReverseMap();
            CreateMap<City, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));
            //Countries 
            CreateMap<Country, CountryDTO>();
            CreateMap<CountryPostDTO, Country>().ReverseMap();
            CreateMap<Country, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

            // user 
            CreateMap<UserDTO, ApplicationUser>().ReverseMap();

            // tour guide
            CreateMap<RegisterTourGuideDTO, ApplicationUser>().ReverseMap();

            CreateMap<TourGuideDTO, ApplicationUser>().ReverseMap();
           

            CreateMap<Tour, TourDTO>().ReverseMap();

            CreateMap<RegisterDTO, ApplicationUser>().ReverseMap();


            CreateMap<AuthModel, ApplicationUser>().ReverseMap();

            // All Users 
            CreateMap<ApplicationUser, ApplicationUsersDTO>().ReverseMap();

            //Reveiws 
            CreateMap<Review, ReviewDTO>();
            CreateMap<ReviewPostDto, Review>().ReverseMap();

            //Tourist 
            CreateMap<TouristDto, ApplicationUser>().ReverseMap();


            //TourGiude
            CreateMap<TourgiudeDto, ApplicationUser>().ReverseMap();
            CreateMap<CardTourGuideDTO, ApplicationUser>().ReverseMap();
            CreateMap<TourGuideDetailsDTO, ApplicationUser>().ReverseMap()

                .ForPath(dest => dest.TourGuideSelectedDTO.TourGuideId, opt => opt.MapFrom(src => src.Id));

			CreateMap<SelectedTourGuide, TourGuideSelectedDTO>().ReverseMap();
		}
    }
}
