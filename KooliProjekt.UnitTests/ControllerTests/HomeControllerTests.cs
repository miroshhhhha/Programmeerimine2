using KooliProjekt.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_should_return_index_view()
        {
            // Arrange
            var logger = new Microsoft.Extensions.Logging.Abstractions.NullLogger<HomeController>();
            var controller = new HomeController(logger);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Index" ||
                        string.IsNullOrEmpty(result.ViewName));
        }
    }
}