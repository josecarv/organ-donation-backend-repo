using AutoMapper;
using Donor.Models;
using Donor.Entities;
using System.Linq;

namespace Donor.Profiles
{
    public class DonorsProfile : Profile
    {
        public DonorsProfile()
        {
            CreateMap<DonorDto,Entities.Donor>()
                .ForMember(dest => dest.Organs, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Organs.Clear();
                    if (src.Organs != null)
                    {
                        dest.Organs = src.Organs.Select(id => new Organ { Id = id }).ToList();
                    }
                });

            CreateMap<Entities.Donor, DonorDto>()
                .ForMember(dest => dest.Organs, opt => opt.MapFrom(src => src.Organs.Select(o => o.Id)));
        }
    }
}
