using AutoMapper;
using Donor.Repositories;
using Microsoft.AspNetCore.Mvc;
using Donor.Models;
using Donor.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


namespace Donor.Controllers
{
    [ApiController]
    [Route("api/donor")]
    public class DonorController : ControllerBase
    {
        private readonly IDonorRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<DonorController> _log;


        public DonorController(IDonorRepository repo, IMapper mapper, ILogger<DonorController> log)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }


        /// <summary>
        /// Just for testing purpose
        /// </summary>
        /// <returns></returns>

        [Tags("Donor")]
        [HttpGet("Ping")]
        public string Ping()
        {

            return "Pong";
        }

        /// <summary>
        /// Register a new donor
        /// </summary>
        /// <param name="donorDto"></param>
        /// <returns></returns>
        [Tags("Donor")]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] DonorDto donorDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var donor = _mapper.Map<Entities.Donor>(donorDto);
            await _repo.AddDonorAsync(donor);

            return Ok("Donor registered successfully");
        }


        [HttpPost("AddOrgans/{donorId}")]
        public async Task<IActionResult> AddOrgans(int donorId, [FromBody] List<int> organIds)
        {
            var donor = await _repo.GetDonorByIdAsync(donorId);
            if (donor == null)
            {
                return NotFound("Donor not found");
            }

            await _repo.AddOrgansToDonorAsync(donor, organIds);

            return Ok("Organs added successfully");
        }


        /// <summary>
        /// Get all donors
        /// </summary>
        /// <returns></returns>
        [Tags("Donor")]
        [HttpGet("GetDonors")]
        public async Task<IActionResult> GetAll()
        {
            var donors = await _repo.GetAllDonorsAsync();
            var donorDtos = _mapper.Map<IEnumerable<DonorDto>>(donors);
            return Ok(donorDtos);
        }


        /// <summary>
        /// Update Edit
        /// </summary>
        /// <returns></returns>
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DonorDto donorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != donorDto.Id)
            {
                return BadRequest("Donor ID mismatch");
            }

            var donor = _mapper.Map<Entities.Donor>(donorDto);

            await _repo.UpdateDonorAsync(id, donor);

            return Ok("Donor updated successfully");
        }

        [HttpGet("GetDonor/{id}")]
        public async Task<IActionResult> GetDonorById(int id)
        {
            var donor = await _repo.GetDonorByIdAsync(id);
            if (donor == null)
            {
                return NotFound("Donor not found");
            }

            var donorDto = _mapper.Map<DonorDto>(donor);
            return Ok(donorDto);
        }




    }
}


