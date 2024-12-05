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
                if (_dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
                {
                    // Simuler funktionalitet for InMemory-database (til tests)
                    _dbContext.Bookmarks.Add(new Bookmark
                    {
                        UserId = userId,
                        ItemType = itemType,
                        ItemId = itemId,
                        Annotation = annotation
                    });
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    // Udfør direkte databasekald i produktionsmiljø
                    await _dbContext.Database.ExecuteSqlInterpolatedAsync(
                        $"SELECT add_bookmark({userId}, {itemType}, {itemId}, {annotation})"
                    );
                }
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
                _dbContext.Bookmarks.Add(new Bookmark
                {
                    UserId = userId,
                    ItemType = itemType,
                    ItemId = itemId,
                    Annotation = annotation
                });
                await _dbContext.SaveChangesAsync();
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
                return await _dbContext.Bookmarks
                    .Where(b => b.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching bookmarks for user ID {userId}: {ex.Message}");
                return new List<Bookmark>();
            }
        }

        // Update a bookmark
        public async Task<bool> UpdateBookmarkAsync(int bookmarkId, string newAnnotation)
        {
            try
            {
                var bookmark = await _dbContext.Bookmarks.FindAsync(bookmarkId);
                if (bookmark != null)
                {
                    bookmark.Annotation = newAnnotation;
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
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
                var bookmark = await _dbContext.Bookmarks.FindAsync(bookmarkId);
                if (bookmark != null)
                {
                    _dbContext.Bookmarks.Remove(bookmark);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting bookmark ID {bookmarkId}: {ex.Message}");
                return false;
            }
        }
    }
}
