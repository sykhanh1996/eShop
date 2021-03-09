using System;
using System.Collections.Generic;
using System.Text;
using eShop.BackendServer.Models.ViewModels.Systems;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace eShop.ViewModels.UnitTest.Systems
{
    public class FunctionCreateRequestValidatorTest
    {
        private readonly FunctionCreateRequestValidator _validator;
        private readonly FunctionCreateRequest _request;
        public FunctionCreateRequestValidatorTest()
        {
            _request = new FunctionCreateRequest
            {
                Id = "test6",
                ParentId = null,
                NameTemp = "test6",
                SortOrder = 6,
                Url = "/test6"
            };
            var mock = new Mock<IStringLocalizer<FunctionCreateRequest>>();
            var localizedString = new LocalizedString("", "");
            mock.Setup(_ => _[""]).Returns(localizedString);
            var localizer = mock.Object;
            _validator = new FunctionCreateRequestValidator(localizer);

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
        public void Should_Error_Result_When_Miss_Id(string data)
        {
            _request.Id = data;
            var result = _validator.Validate(_request);
            Assert.False(result.IsValid);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Error_Result_When_Miss_Name(string data)
        {
            _request.NameTemp = data;
            var result = _validator.Validate(_request);
            Assert.False(result.IsValid);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Error_Result_When_Miss_Url(string data)
        {
            _request.Url = data;
            var result = _validator.Validate(_request);
            Assert.False(result.IsValid);
        }
    }
}
