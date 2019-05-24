using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using VidlyExercice1.Dto;
using VidlyExercice1.ViewModels;

namespace VidlyExercice1.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerDto, Customer>().ForMember(c => c.Id, opt => opt.UseDestinationValue());
            CreateMap<Customer, CustomerDto>();

            CreateMap<MovieDto, Movie>().ForMember(c => c.Id, opt => opt.UseDestinationValue());
            CreateMap<Movie, MovieDto>();

            CreateMap<MembershipType, MembershipTypeDto>();
            CreateMap<MovieGenre, MovieGenreDto>();
        }      
    }
}