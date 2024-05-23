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
            if (donorDto == null)
            {
                return BadRequest("Donor data is null");
            }

            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var donor = _mapper.Map<Donor.Models.Donor>(donorDto);

                await _repo.AddDonorAsync(donor);


                return Ok("Donor registered successfully");
            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
            {
                return Conflict("A donor with the same Identity Number or Email already exists.");
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error registering donor");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Get all donors
        /// </summary>
        /// <returns></returns>
        [Tags("Donor")]
        [HttpGet("GetDonors")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var donors = await _repo.GetAllDonorsAsync();
                return Ok(donors);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error getting all donors");
                return StatusCode(500, "Internal server error");
            }
        }

         private bool IsUniqueConstraintViolation(DbUpdateException ex)
        {
            if (ex.InnerException is SqlException sqlEx)
            {
                return sqlEx.Number == 2627 || sqlEx.Number == 2601;
            }
            return false;
        }
    }
 }


