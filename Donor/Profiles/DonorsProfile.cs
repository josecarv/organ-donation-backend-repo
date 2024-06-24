using AutoMapper;
using Donor.Models;
using Donor.Entities;
using System.Linq;
using Donor.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Drawing;


namespace Donor.Profiles
{
    public class DonorsProfile : Profile
    {
        public DonorsProfile()
        {

            CreateMap<DonorDto, Entities.Donor>()
             .ForMember(dest => dest.Organs, opt => opt.Ignore());
        


                 CreateMap<Entities.Donor, DonorDto>()
             .ForMember(dest => dest.Organs, opt => opt.MapFrom(src => src.Organs.Select(o => o.Id)));



            CreateMap<Organ, OrganDto>();
            CreateMap<OrganDto, Organ>();
        }
    }
}
