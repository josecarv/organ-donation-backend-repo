using AutoMapper;
using Donor.Models;
using Donor.Entities;
using System.Linq;
using Donor.DbContexts;
using Microsoft.EntityFrameworkCore;


namespace Donor.Profiles
{
    public class DonorsProfile : Profile
    {
        public DonorsProfile()
        {
            CreateMap<DonorDto, Entities.Donor>();
         // .ForMember(dest => dest.Organs, opt => opt.Ignore());
            // .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy));

            CreateMap<Entities.Donor, DonorDto>();
                        //.ForMember(dest => dest.Organs, opt => opt.MapFrom(src => src.Organs.Select(o => o.Id)));



            CreateMap<Organ, OrganDto>();
            CreateMap<OrganDto, Organ>();
        }
    }
}
