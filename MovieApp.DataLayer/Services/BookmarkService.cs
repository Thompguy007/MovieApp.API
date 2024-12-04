using Microsoft.EntityFrameworkCore;
using MovieApp.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.DataLayer.Services
{
    public class BookmarkService
    {
        private readonly MovieContext _dbContext;

        public BookmarkService(MovieContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Add bookmark using the add_bookmark database function
        public async Task<bool> AddBookmarkUsingFunctionAsync(int userId, string itemType, string itemId, string annotation)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync(
                    "SELECT add_bookmark({0}, {1}, {2}, {3})",
                    userId, itemType, itemId, annotation
                );
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding bookmark using function: {ex.Message}");
                return false;
            }
        }

        // Add a bookmark (CRUD operation)
        public async Task<bool> AddBookmarkAsync(int userId, string itemType, string itemId, string annotation)
        {
            try
            {
                var query = $"INSERT INTO bookmarks (user_id, item_type, item_id, annotation) VALUES ({userId}, '{itemType}', '{itemId}', '{annotation}')";
                await _dbContext.Database.ExecuteSqlRawAsync(query);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding bookmark: {ex.Message}");
                return false;
            }
        }

        // Get bookmarks by user ID
        public async Task<List<Bookmark>> GetBookmarksByUserIdAsync(int userId)
        {
            try
            {
                var bookmarks = await _dbContext.Bookmarks
                    .FromSqlInterpolated($"SELECT * FROM get_bookmarks({userId})")
                    .ToListAsync();
                return bookmarks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching bookmarks for user ID {userId}: {ex.Message}");
                return new List<Bookmark>(); // Return a blank list in case of an error
            }
        }

        // Update a bookmark
        public async Task<bool> UpdateBookmarkAsync(int bookmarkId, string newAnnotation)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlInterpolatedAsync(
                    $"UPDATE bookmarks SET annotation = {newAnnotation} WHERE bookmark_id = {bookmarkId}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating bookmark ID {bookmarkId}: {ex.Message}");
                return false;
            }
        }

        // Delete a bookmark
        public async Task<bool> DeleteBookmarkAsync(int bookmarkId)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlInterpolatedAsync(
                    $"DELETE FROM bookmarks WHERE bookmark_id = {bookmarkId}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting bookmark ID {bookmarkId}: {ex.Message}");
                return false;
            }
        }
    }
}
