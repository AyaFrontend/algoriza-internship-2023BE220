using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizeeta.Domain.Entities;
using Vizeeta.Presentation.Dto;

namespace Vizeeta.Presentation.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Doctor,DoctorDot>().ReverseMap();
            CreateMap<AppUser, DoctorDot>().ReverseMap();
            CreateMap<Patient, PatientDto>().ReverseMap();
            CreateMap<AppUser, PatientDto>().ReverseMap();
            CreateMap<Appoinment, AppoinmentDto>().ReverseMap();
            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<Cupone, CuponDto>().ReverseMap();
        }
    }
}
