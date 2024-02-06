using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;
using WhereToServices.DTOs;

namespace WhereToServices.Mapping
{
    public class FeedbackMappingProfile : Profile
    {
        public FeedbackMappingProfile()
        {
            CreateMap<FeedbackDto, TourFeedback>()
           .ForMember(dest => dest.TourId, opt => opt.MapFrom(src => src.TourId))
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
           .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
           .ForMember(dest => dest.FeedbackPhotos, opt => opt.Ignore()); 
        }
    }
}
