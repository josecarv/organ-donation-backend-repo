using AutoMapper;

namespace Donor
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Donor.Models.DonorDto, Entities.Donor>();
        }
    }
}
