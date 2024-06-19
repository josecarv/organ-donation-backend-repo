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
            CreateMap<DonorDto, Entities.Donor>()
				.ForMember(dest => dest.MailingAddress, opt => opt.MapFrom(src => src.MailingAddress))
				.ForMember(dest => dest.ResidentialAddress, opt => opt.MapFrom(src => src.ResidentialAddress))
				.ForMember(dest => dest.PreferredContact, opt => opt.MapFrom(src => (int)src.PreferredContact))
				.ForMember(dest => dest.Organs, opt => opt.MapFrom(src => src.Organs));


			CreateMap<Entities.Donor, DonorDto>();
                        //.ForMember(dest => dest.Organs, opt => opt.MapFrom(src => src.Organs.Select(o => o.Id)));



            CreateMap<Organ, OrganDto>();
            CreateMap<OrganDto, Organ>();
        }
    }
}
