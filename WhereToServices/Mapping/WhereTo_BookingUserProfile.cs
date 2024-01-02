using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.WhereTo_BookingEntities;
using WhereToServices.DTOs;

namespace WhereToServices.Mapping
{
    public class WhereTo_BookingUserProfile : Profile
    {
        public WhereTo_BookingUserProfile()
        {
            CreateMap<WhereToBookingMessage, User>()
           .ReverseMap();
        }
    }
}
