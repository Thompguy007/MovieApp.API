using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer;
using MovieApp.DataLayer.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonWordsDbController : ControllerBase
    {
        private readonly PersonWordsDbBusinessService _personWordsDbBusinessService;

        public PersonWordsDbController(PersonWordsDbBusinessService personWordsDbBusinessService)
        {
            _personWordsDbBusinessService = personWordsDbBusinessService;
        }

        // API-endpoint til at hente ord og hyppighed
        [HttpGet]
        public async Task<ActionResult<List<PersonWordsResult>>> GetPersonWords([FromQuery] string nconst, [FromQuery] int maxLength)
        {
            var result = await _personWordsDbBusinessService.GetPersonWordsAsync(nconst, maxLength);
            return Ok(result);
        }
    }
}
