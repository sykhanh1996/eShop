using eShop.BackendServer.Models.ViewModels.Systems;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace eShop.ViewModels.UnitTest.Systems
{
    public class RoleCreateRequestValidatorTest
    {
        private readonly RoleCreateRequestValidator _validator;
        private readonly RoleCreateRequest _request;

        public RoleCreateRequestValidatorTest()
        {
            _request = new RoleCreateRequest
            {
                Id = "admin",
                Name = "admin"
            };
            var mock = new Mock<IStringLocalizer<RoleCreateRequest>>();
            var localizedString = new LocalizedString("", "");
            mock.Setup(_ => _[""]).Returns(localizedString);
            var localizer = mock.Object;
            _validator = new RoleCreateRequestValidator(localizer);

        }

        [Fact]
        public void Should_Valid_Result_When_Valid_Request()
        {
            var result = _validator.Validate(_request);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Should_Error_Result_When_Request_Miss_RoleId()
        {
            _request.Id = string.Empty;
            var result = _validator.Validate(_request);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Error_Result_When_Request_Miss_RoleName()
        {
            _request.Name = string.Empty;
            var result = _validator.Validate(_request);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Error_Result_When_Request_Role_Empty()
        {
            _request.Id = string.Empty;
            _request.Name = string.Empty;
            var result = _validator.Validate(_request);
            Assert.False(result.IsValid);
        }
    }
}