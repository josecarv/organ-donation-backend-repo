using AutoMapper;
using Donor.Entities;
using Donor.Models;

namespace Donor.Profiles
{
	public class OrgansProfile : Profile
	{
        public OrgansProfile()
        {
			CreateMap<Organ, OrganDto>();
			CreateMap<OrganDto, Organ>();
		}
    }
}
