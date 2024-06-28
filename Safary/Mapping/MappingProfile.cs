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
            CreateMap<Tour, TourDTO>();
            CreateMap<TourPostDTO, Tour>().ReverseMap();
            CreateMap<Tour, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

            CreateMap<TourHourDTO, Tour>().ReverseMap()
			    .ForMember(dest => dest.TourImages, opt => opt.MapFrom(src => src.TourImages));

            CreateMap<Tour, TourHourPostDTO>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore()); 

            CreateMap<TourImage, TourImageDTO>().ReverseMap();

			CreateMap<TourImage, TourImagesDTO>().ReverseMap(); // Handle Images

            CreateMap<TourBlog, TourDTO>();
            CreateMap<TourPostDTO, TourBlog>().ReverseMap();
            CreateMap<TourBlog, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));

            // user 
            CreateMap<UserDTO, ApplicationUser>().ReverseMap();

            // tour guide
            CreateMap<RegisterTourGuideDTO, ApplicationUser>().ReverseMap();

            CreateMap<TourGuideDTO, ApplicationUser>().ReverseMap();
           

            CreateMap<TourBlog, TourDTO>().ReverseMap();

            CreateMap<RegisterDTO, ApplicationUser>().ReverseMap();


            CreateMap<AuthModel, ApplicationUser>().ReverseMap();

            // All Users 
            CreateMap<ApplicationUser, ApplicationUsersDTO>().ReverseMap();

            //Reveiws 
            CreateMap<TourGuideReview, TourGuideReviewDTO>();
            CreateMap<TourGuideReviewPostDto, TourGuideReview>().ReverseMap();

            CreateMap<TourReview, TourReviewDTO>();
            CreateMap<TourReviewPostDTO, TourReview>().ReverseMap();

             CreateMap<ApplicationUser, TourGuideDetailsDTO>();
            CreateMap<TourGuideReview, TourGuideReviewDetailsDTO>();

            //Tourist 
            CreateMap<TouristDto, ApplicationUser>().ReverseMap();


            //TourGiude
            CreateMap<TourgiudeDto, ApplicationUser>().ReverseMap();
            CreateMap<CardTourGuideDTO, ApplicationUser>().ReverseMap();
            CreateMap<TourGuideDetailsDTO, ApplicationUser>().ReverseMap()
                .ForPath(dest => dest.TourGuideSelectedDTO.TourGuideId, opt => opt.MapFrom(src => src.Id));

			CreateMap<SelectedTourGuide, TourGuideSelectedDTO>().ReverseMap();

			CreateMap<TourGuideSelectedTableDTO, SelectedTourGuide>().ReverseMap();

            //Tours
            CreateMap<Tour, TourDetailsDTO>()
               .ForMember(dest => dest.AverageRating, opt => opt.Ignore()) // Ignore during mapping from Tour to TourDetailsDTO
               .ForMember(dest => dest.Reviews, opt => opt.Ignore()); // Ignore during mapping from Tour to TourDetailsDTO

            CreateMap<TourReview, TourReviewDetailsDTO>();

            //admin
            CreateMap<ApplicationUser, AdminDTO>().ReverseMap();

        }
    }
}
