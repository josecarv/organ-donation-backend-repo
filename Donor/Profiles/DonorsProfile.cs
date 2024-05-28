using AutoMapper;
using Donor.Models;


namespace Donor.Profiles
{
    public class DonorsProfile : Profile
    {
        public DonorsProfile()
        {
          CreateMap<DonorDto, Entities.Donor>()
                .ForMember(dest => dest.DonationPreferences, opt => opt.Ignore());

            CreateMap<Entities.Donor, DonorDto>()
                .ForMember(dest => dest.DonationPreferences, opt => opt.MapFrom(src => src.DonationPreferences.Select(dp => dp.OrganId)));
        }
           
        }
    }

