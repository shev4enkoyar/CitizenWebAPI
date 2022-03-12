using CitizenWebAPI.Data;
using CitizenWebAPI.Models;
using CitizenWebAPI.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CitizenWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CitizenController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            new DatabaseFiller(dbContext).FillCitizensDb();
        }

        [HttpGet("get/{id}")]
        public IActionResult GetCitizen(string id)
        {
            var citizen = _dbContext.Citizens.Find(id);
            if (citizen != null)
                return Ok(new { citizen.Name, citizen.Sex, citizen.Age });
            return NotFound("Person with this id not found");
        }

        [HttpGet("get")]
        public IActionResult GetCitizens(string sex = null, int? ageX = null, int? ageY = null, int page = 1, int pageSize = 2)
        {
            BadRequestObjectResult pageSettingsBadRequest = CheckSettingsPage(sex, page, pageSize);
            if (pageSettingsBadRequest != null)
                return pageSettingsBadRequest;

            IEnumerable<Citizen> citizens = _dbContext.Citizens;
            if (citizens == null)
                return NotFound("Persons not found");

            citizens = Filtration.GetFiltration(citizens, sex, ageX, ageY);

            if (!citizens.Any())
                return NotFound("Persons with these parameters not found");

            var pagedCitizensShort = Pagination.Paginate(citizens, page, pageSize);
            if (!pagedCitizensShort.Any())
                return NotFound("Pages are over");
            return Ok(pagedCitizensShort);
        }

        [NonAction]
        private BadRequestObjectResult CheckSettingsPage(string sex, int page, int pageSize)
        {
            if (page < 1)
                return BadRequest("Page cannot be less than 1");
            else if (pageSize < 1)
                return BadRequest("Page size cannot be less than 1");
            else if (!(sex == "male" || sex == "female" || sex == null))
                return BadRequest("Sex can only be male or female");
            return null;
        }
    }
}
