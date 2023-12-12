using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;
using WhereToServices.DTOs;
using AutoMapper;

namespace WhereToServices.Mapping
{
    public class UserTourMapperProfile : Profile
    {
        public UserTourMapperProfile()
        {
            CreateMap<PayForTourDto, UserTour>()
           .ReverseMap();
        }       
    }
}
