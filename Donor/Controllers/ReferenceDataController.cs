using System.Collections.Generic;
using System.Threading.Tasks;
using Donor.Repositories;
using Microsoft.AspNetCore.Mvc;
using Donor.Entities;

namespace Donor.Controllers
{
    [ApiController]
    [Route("api/donor")]
    public class ReferenceDataController : ControllerBase
    {
        private readonly IReferenceRepository _repo;

        public ReferenceDataController(IReferenceRepository repository)
        {
            _repo = repository;
        }

        [HttpGet("GetLocalities")]
        public async Task<ActionResult<IEnumerable<Locality>>> GetAllLocalities()
        {
            var localities = await _repo.GetAllLocalitiesAsync();
            return Ok(localities);
        }



        [HttpGet("GetBloodGroups")]
        public IActionResult GetBloodGroups()
        {
            var bloodGroups = Enum.GetValues(typeof(BloodGroup))
                                  .Cast<BloodGroup>()
                                  .Select(bg => new { Id = (int)bg, Name = bg.ToString() })
                                  .ToList();
            return Ok(bloodGroups);
        }

        [HttpGet("GetGenders")]
        public IActionResult GetGenders()
        {
            var genders = Enum.GetValues(typeof(Gender))
                              .Cast<Gender>()
                              .Select(g => new { Id = (int)g, Name = g.ToString() })
                              .ToList();
            return Ok(genders);
        }


        [HttpGet("GetNationalities")]
        public async Task<ActionResult<IEnumerable<Nationality>>> GetAllNationalities()
        {
            var nationalities = await _repo.GetAllNationalitiesAsync();
            return Ok(nationalities);
        }
    }
}
