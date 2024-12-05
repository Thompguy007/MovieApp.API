using Xunit;
using Moq;
using MovieApp.DataLayer.Services;
using MovieApp.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieApp.DataLayer;

namespace MovieApp.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task GetUserById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange: Configure the in-memory database options for testing
            var options = new DbContextOptionsBuilder<MovieContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Arrange: Seed the database with a user for testing
            using (var context = new MovieContext(options))
            {
                context.users.Add(new User
                {
                    UserId = 1, // Unique ID for the user
                    Username = "testuser", // Username for the user
                    Email = "test@example.com", // Email for the user
                    Password = "securepassword" // Required property to avoid exceptions
                });
                context.SaveChanges(); // Save the changes to the in-memory database
            }

            // Act: Retrieve the user by ID using the UserService
            using (var context = new MovieContext(options))
            {
                var service = new UserService(context);

                // Act: Call the GetUserByIdAsync method
                var result = await service.GetUserByIdAsync(1);

                // Assert: Verify the retrieved user is correct
                Assert.NotNull(result); // Ensure the user is not null
                Assert.Equal("testuser", result.Username); // Check the username matches
                Assert.Equal("test@example.com", result.Email); // Check the email matches
            }
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange: Configure the in-memory database options for testing
            var options = new DbContextOptionsBuilder<MovieContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Arrange: Create a context with no users added
            using (var context = new MovieContext(options))
            {
                var service = new UserService(context);

                // Act: Attempt to retrieve a user that does not exist
                var result = await service.GetUserByIdAsync(999);

                // Assert: Verify that null is returned
                Assert.Null(result); // Ensure the result is null
            }
        }
    }
}
