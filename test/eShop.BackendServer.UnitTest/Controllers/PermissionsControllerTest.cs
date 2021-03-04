using System;
using System.Collections.Generic;
using System.Text;
using eShop.BackendServer.Controllers;
using eShop.BackendServer.Data;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace eShop.BackendServer.UnitTest.Controllers
{
    public class PermissionsControllerTest
    {
        private ApplicationDbContext _context;
        private Mock<IConfigurationRoot> _mockConfigurationRoot;
        public PermissionsControllerTest()
        {
            _context = new InMemoryDbContextFactory().GetApplicationDbContext();
            _mockConfigurationRoot = new Mock<IConfigurationRoot>();
        }
        [Fact]
        public void ShouldCreateInstance_NotNull_Success()
        {
            _mockConfigurationRoot.SetupGet(x => x[It.IsAny<string>()]).Returns("the string you want to return");
            var controller = new PermissionsController(_mockConfigurationRoot.Object);
            Assert.NotNull(controller);
        }
    }
}
