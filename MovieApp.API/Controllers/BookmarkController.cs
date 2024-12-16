using Microsoft.AspNetCore.Mvc;
using MovieApp.BusinessLayer.Services;

[ApiController]
[Route("api/[controller]")]
public class BookmarkController : ControllerBase
{
    private readonly BookmarkBusinessService _bookmarkBusinessService;

    public BookmarkController(BookmarkBusinessService bookmarkBusinessService)
    {
        _bookmarkBusinessService = bookmarkBusinessService;
    }

    // Brug databasefunktionen add_bookmark
    [HttpPost("AddUsingFunction")]
    public async Task<IActionResult> AddBookmarkUsingFunction(int userId, string itemType, string itemId, string annotation)
    {
        var success = await _bookmarkBusinessService.AddBookmarkUsingFunctionAsync(userId, itemType, itemId, annotation);
        if (success)
        {
            return Ok("Bookmark added successfully using database function.");
        }
        return StatusCode(500, "Failed to add bookmark using database function.");
    }

    // CRUD: Tilføj et bogmærke
    [HttpPost("Add")]
    public async Task<IActionResult> AddBookmark(int userId, string itemType, string itemId, string annotation)
    {
        var success = await _bookmarkBusinessService.AddBookmarkAsync(userId, itemType, itemId, annotation);
        if (success)
        {
            return Ok("Bookmark added successfully.");
        }
        return StatusCode(500, "Failed to add bookmark.");
    }

    // CRUD: Hent bogmærker efter bruger-ID
    [HttpGet("Get/{userId}")]
    public async Task<IActionResult> GetBookmarks(int userId, int page = 0, int pageSize = 10)
    {
        var bookmarks = await _bookmarkBusinessService.GetBookmarksByUserIdAsync(userId);

        if (bookmarks == null || !bookmarks.Any())
        {
            return NotFound("No bookmarks found for the specified user.");
        }

        var paginatedBookmarks = bookmarks
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();

        return Ok(new
        {
            items = paginatedBookmarks,
            totalPages = (int)Math.Ceiling((double)bookmarks.Count / pageSize)
        });
    }

    // CRUD: Opdater et bogmærke
    [HttpPatch("Update")]
    public async Task<IActionResult> UpdateBookmark(int bookmarkId, string newAnnotation)
    {
        var success = await _bookmarkBusinessService.UpdateBookmarkAsync(bookmarkId, newAnnotation);
        if (success)
        {
            return Ok("Bookmark updated successfully.");
        }
        return StatusCode(500, "Failed to update bookmark.");
    }

    // CRUD: Slet et bogmærke
    [HttpDelete("Delete/{bookmarkId}")]
    public async Task<IActionResult> DeleteBookmark(int bookmarkId)
    {
        var success = await _bookmarkBusinessService.DeleteBookmarkAsync(bookmarkId);
        if (success)
        {
            return Ok("Bookmark deleted successfully.");
        }
        return StatusCode(500, "Failed to delete bookmark.");
    }
}
