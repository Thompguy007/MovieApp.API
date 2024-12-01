using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer;
using System.Threading.Tasks;

namespace MovieApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetBookmarksDbController : ControllerBase
    {
        private readonly GetBookmarksDbBusinessService _getBookmarksDbBusinessService;

        public GetBookmarksDbController(GetBookmarksDbBusinessService getBookmarksDbBusinessService)
        {
            _getBookmarksDbBusinessService = getBookmarksDbBusinessService;
        }

        // API-endpoint til at hente bogmærker for en bruger baseret på user_id
        [HttpGet]
        public async Task<IActionResult> GetBookmarkedItems([FromQuery] int userId)
        {
            // Kald BusinessLayer for at hente bogmærker
            var bookmarks = await _getBookmarksDbBusinessService.GetBookmarksForUserAsync(userId);
            if (bookmarks == null || bookmarks.Count == 0)
            {
                return NotFound("No bookmarks found for the given user.");
            }
            return Ok(bookmarks);
        }
    }
}
