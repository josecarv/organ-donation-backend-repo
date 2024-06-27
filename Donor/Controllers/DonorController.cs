using AutoMapper;
using Donor.Repositories;
using Microsoft.AspNetCore.Mvc;
using Donor.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Donor.Enums;
using Donor.Entities;


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
			await _repo.SaveChangesAsync();
			return Ok("Donor registered successfully");
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
        /// 
        /// </summary>
        /// <returns></returns>
        [Tags("Donor")]
        [HttpGet("GetOrgans")]
        public async Task<IActionResult> GetAllOrgans()
        {
            var organs = await _repo.GetAllOrgansAsync();
            var organDtos = _mapper.Map<IEnumerable<OrganDto>>(organs);
            return Ok(organDtos);
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

            var existingDonor = await _repo.GetDonorByIdAsync(id);
            if (existingDonor == null)
            {
                return NotFound("Donor not found");
            }

            await _repo.UpdateDonorAsync(id, donorDto);

            await _repo.SaveChangesAsync();

         

            return Ok("Donor updated successfully");
        }
    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>

        [HttpGet("SearchDonors")]
        public async Task<IActionResult> SearchDonors([FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term cannot be empty");
            }

            var donors = await _repo.SearchDonorsAsync(searchTerm);
            var donorDtos = _mapper.Map<IEnumerable<DonorDto>>(donors);

            return Ok(donorDtos);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="statusDto"></param>
        /// <returns></returns>
        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateDonorStatus(int id, [FromBody] DonorStatusDto statusDto)
        {
            if (statusDto == null)
            {
                return BadRequest("Invalid status data");
            }

            var donor = await _repo.GetDonorByIdAsync(id);
            if (donor == null)
            {
                return NotFound("Donor not found");
            }

            donor.DonorStatus = (int)statusDto.Status;
            if (statusDto.Status == DonorStatus.OnHold)
            {
                donor.OnHoldReason = statusDto.OnHoldReason;
            }
            else
            {
                donor.OnHoldReason = "Not Applicable";
            }

            await _repo.SaveChangesAsync();
            return Ok("Donor status updated successfully");
        }
    }


}



