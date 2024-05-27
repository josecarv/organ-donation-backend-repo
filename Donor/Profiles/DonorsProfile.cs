using AutoMapper;

namespace Donor.Profiles
{
    public class DonorsProfile : Profile
    {
        public DonorsProfile()
        {
            CreateMap<Models.DonorDto, Entities.Donor>();
        }
    }
}
