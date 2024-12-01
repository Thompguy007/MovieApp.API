using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StructuredStringSearchController : ControllerBase
{
    private readonly StructuredStringSearchBusinessService _businessService;

    public StructuredStringSearchController(StructuredStringSearchBusinessService businessService)
    {
        _businessService = businessService;
    }

    [HttpGet("Search")]
    public async Task<IActionResult> Search(
        [FromQuery] string titleOfMovie,
        [FromQuery] string plotDesc,
        [FromQuery] string characterName,
        [FromQuery] string actorName)
    {
        var results = await _businessService.SearchAsync(titleOfMovie, plotDesc, characterName, actorName);
        return Ok(results);
    }
}
