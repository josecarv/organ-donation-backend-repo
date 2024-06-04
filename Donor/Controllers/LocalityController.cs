using System.Collections.Generic;
using System.Threading.Tasks;
using Donor.Repositories;
using Microsoft.AspNetCore.Mvc;
using Donor.Entities;

namespace Donor.Controllers
{
    [ApiController]
    [Route("api/locality")]
    public class LocalityController : ControllerBase
    {
        private readonly ILocalityRepository _repo;

        public LocalityController(ILocalityRepository repository)
        {
            _repo = repository;
        }

        [HttpGet("GetLocalities")]
        public async Task<ActionResult<IEnumerable<Locality>>> GetAllLocalities()
        {
            var localities = await _repo.GetAllLocalitiesAsync();
            return Ok(localities);
        }
    }
}
