using Microsoft.EntityFrameworkCore;
using MovieApp.DataLayer;
using MovieApp.DataLayer.Models;
using MovieApp.DataLayer.Services;
using System.Threading.Tasks;
using Xunit;

namespace MovieApp.Tests.Services
{
    public class BookmarkServiceTests
    {
        private DbContextOptions<MovieContext> CreateInMemoryOptions()
        {
            return new DbContextOptionsBuilder<MovieContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }
        [Fact]
        public async Task GetBookmarksByUserId_ShouldReturnBookmarksForGivenUser()
        {
            // Arrange: Setup in-memory database med testdata
            var options = new DbContextOptionsBuilder<MovieContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new MovieContext(options))
            {
                // Tilføj testdata
                context.Bookmarks.AddRange(
                    new Bookmark { BookmarkId = 1, UserId = 1, ItemType = "m", ItemId = "tt1234567", Annotation = "Bookmark 1" },
                    new Bookmark { BookmarkId = 2, UserId = 1, ItemType = "m", ItemId = "tt7654321", Annotation = "Bookmark 2" },
                    new Bookmark { BookmarkId = 3, UserId = 2, ItemType = "m", ItemId = "tt1111111", Annotation = "Bookmark 3" }
                );
                await context.SaveChangesAsync();
            }

            using (var context = new MovieContext(options))
            {
                var service = new BookmarkService(context);

                // Act: Kald metoden for at hente bogmærker for bruger med ID = 1
                var result = await service.GetBookmarksByUserIdAsync(1);

                // Assert: Tjek at resultaterne er korrekte
                Assert.NotNull(result);
                Assert.Equal(2, result.Count); // Bruger 1 skal have præcis 2 bogmærker
                Assert.Contains(result, b => b.Annotation == "Bookmark 1");
                Assert.Contains(result, b => b.Annotation == "Bookmark 2");
            }
        }

        [Fact]
        public async Task GetBookmarksByUserId_ShouldReturnEmptyList_WhenNoBookmarksExist()
        {
            // Arrange: Setup in-memory database uden bogmærker
            var options = new DbContextOptionsBuilder<MovieContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_Empty")
                .Options;

            using (var context = new MovieContext(options))
            {
                var service = new BookmarkService(context);

                // Act: Kald metoden for en bruger uden bogmærker
                var result = await service.GetBookmarksByUserIdAsync(999); // Bruger 999 findes ikke

                // Assert: Resultatet skal være en tom liste
                Assert.NotNull(result);
                Assert.Empty(result);
            }
        }
        [Fact]
        public async Task AddBookmark_ShouldAddBookmarkToDatabase()
        {
            // Arrange: Set up in-memory database and clear it
            var options = CreateInMemoryOptions();
            using (var context = new MovieContext(options))
            {
                context.Bookmarks.RemoveRange(context.Bookmarks); // Clear existing bookmarks
                await context.SaveChangesAsync();
            }

            using (var context = new MovieContext(options))
            {
                var service = new BookmarkService(context);

                // Act: Add a new bookmark
                var result = await service.AddBookmarkAsync(1, "m", "tt1234567", "Test annotation");

                // Assert: Verify the bookmark was added
                Assert.True(result);

                var bookmarks = await context.Bookmarks.ToListAsync();
                Assert.Single(bookmarks); // Expect exactly one bookmark
                Assert.Equal("Test annotation", bookmarks[0].Annotation);
                Assert.Equal("tt1234567", bookmarks[0].ItemId);
                Assert.Equal("m", bookmarks[0].ItemType);
                Assert.Equal(1, bookmarks[0].UserId);
            }
        }
    }
}
