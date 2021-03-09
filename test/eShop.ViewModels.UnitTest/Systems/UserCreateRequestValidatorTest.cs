using System;
using eShop.BackendServer.Controllers;
using eShop.BackendServer.Models.ViewModels.Systems;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace eShop.ViewModels.UnitTest.Systems
{
    public class UserCreateRequestValidatorTest
    {
        private readonly UserCreateRequestValidator _validator;
        private readonly UserCreateRequest _request;

        public UserCreateRequestValidatorTest()
        {
            _request = new UserCreateRequest()
            {
                Dob = DateTime.Now,
                Email = "sykhanh1996@gmail.com",
                FirstName = "Test",
                LastName = "test",
                Password = "Admin@123",
                PhoneNumber = "12345",
                UserName = "test"
            };
            var mock = new Mock<IStringLocalizer<UserCreateRequest>>();
            var localizedString = new LocalizedString("PhoneNumber", "Phone number is required");
            mock.Setup(_ => _["PhoneNumber"]).Returns(localizedString);
            var localizer = mock.Object;
            _validator = new UserCreateRequestValidator(localizer);

        }

        [Fact]
        public void Should_Valid_Result_When_Valid_Request()
        {
            var result = _validator.Validate(_request);
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Error_Result_When_Miss_UserName(string userName)
        {
            _request.UserName = userName;
            var result = _validator.Validate(_request);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Error_Result_When_Miss_LastName(string data)
        {
            _request.LastName = data;
            var result = _validator.Validate(_request);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Error_Result_When_Miss_FirstName(string data)
        {
            _request.FirstName = data;
            var result = _validator.Validate(_request);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Error_Result_When_Miss_PhoneNumber(string data)
        {
            _request.PhoneNumber = data;
            var result = _validator.Validate(_request);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("sdasfaf")]
        [InlineData("1234567")]
        [InlineData("Admin123")]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Error_Result_When_Password_Not_Match(string data)
        {
            _request.Password = data;
            var result = _validator.Validate(_request);
            Assert.False(result.IsValid);
        }
    }
}