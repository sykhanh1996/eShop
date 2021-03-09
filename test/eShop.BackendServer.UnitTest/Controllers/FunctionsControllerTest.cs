using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eShop.BackendServer.AutoMapper;
using eShop.BackendServer.Controllers;
using eShop.BackendServer.Data;
using eShop.BackendServer.Data.Entities;
using eShop.BackendServer.Models.ViewModels;
using eShop.BackendServer.Models.ViewModels.Systems;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace eShop.BackendServer.UnitTest.Controllers
{
    public class FunctionsControllerTest
    {
        private readonly IMapper _mapper;
        private ApplicationDbContext _context;
        private readonly IStringLocalizer<FunctionsController> _localizer;
        private Mock<ILogger<FunctionsController>> _mockLogger;
        private List<Function> _functionSources = new List<Function>(){
            new Function("1","test1",1,"test1","icon1","name1"),
            new Function("2","test2",2,"test2","icon2","name2"),
            new Function("3","test3",3,"test3","icon3","name3")

        };
        public FunctionsControllerTest()
        {
            _context = new InMemoryDbContextFactory().GetApplicationDbContext("FunctionsControllerTest");
            _mockLogger = new Mock<ILogger<FunctionsController>>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new ViewModelToDomainMappingProfile());
                    mc.AddProfile(new DomainToViewModelMappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }
        [Fact]
        public void Should_Create_Instance_Not_Null_Success()
        {
            var controller = new FunctionsController(_context, _mockLogger.Object, _mapper, _localizer);
            Assert.NotNull(controller);
        }
        [Fact]
        public async Task PostFunction_ValidInput_Success()
        {
            IStringLocalizer<FunctionsController> localizer = MockLocalizer();

            var controller = new FunctionsController(_context, _mockLogger.Object, _mapper, localizer);
            var result = await controller.PostFunction(new FunctionCreateRequest
            {
                Id = "PostFunction_ValidInput_Success",
                ParentId = null,
                NameTemp = "PostFunction_ValidInput_Success",
                SortOrder = 5,
                Url = "/PostFunction_ValidInput_Success"
            });

            Assert.IsType<CreatedAtActionResult>(result);
        }



        [Fact]
        public async Task PostFunction_ValidInput_Failed()
        {
       
            _context.Functions.Add(
                new Function
                {
                    Id = "PostUser_ValidInput_Failed",
                    ParentId = null,
                    NameTemp = "PostUser_ValidInput_Failed",
                    SortOrder = 1,
                    Url = "/PostUser_ValidInput_Failed",
               
                }
            );
            await _context.SaveChangesAsync();
            IStringLocalizer<FunctionsController> localizer = MockLocalizer();
            var controller = new FunctionsController(_context, _mockLogger.Object, _mapper, localizer);

            var result = await controller.PostFunction(new FunctionCreateRequest()
            {
                Id = "PostUser_ValidInput_Failed",
                ParentId = null,
                NameTemp = "PostUser_ValidInput_Failed",
                SortOrder = 5,
                Url = "/PostUser_ValidInput_Failed",
                Icon = "test"
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task GetFunction_HasData_ReturnSuccess()
        {
            IStringLocalizer<FunctionsController> localizer = MockLocalizer();
            _context.Functions.AddRange(new List<Function>()
            {
                new Function(){
                    Id = "GetFunction_HasData_ReturnSuccess",
                    ParentId = null,
                    NameTemp = "GetFunction_HasData_ReturnSuccess",
                    SortOrder =1,
                    Url ="/GetFunction_HasData_ReturnSuccess"
                }
            });
            await _context.SaveChangesAsync();
            var controller = new FunctionsController(_context, _mockLogger.Object, _mapper, localizer);
            var result = await controller.GetFunctions();
            var okResult = result as OkObjectResult;
            var UserVms = okResult.Value as IEnumerable<FunctionVm>;
            Assert.True(UserVms.Count() > 0);
        }
        [Fact]
        public async Task GetFunctionsPaging_NoFilter_ReturnSuccess()
        {
            IStringLocalizer<FunctionsController> localizer = MockLocalizer();
            _context.Functions.AddRange(new List<Function>()
            {
                new Function(){
                    Id = "GetFunctionsPaging_NoFilter_ReturnSuccess1",
                    ParentId = null,
                    NameTemp = "GetFunctionsPaging_NoFilter_ReturnSuccess1",
                    SortOrder =1,
                    Url ="/test1"
                },
                new Function(){
                    Id = "GetFunctionsPaging_NoFilter_ReturnSuccess2",
                    ParentId = null,
                    NameTemp = "GetFunctionsPaging_NoFilter_ReturnSuccess2",
                    SortOrder =2,
                    Url ="/test2"
                },
                new Function(){
                    Id = "GetFunctionsPaging_NoFilter_ReturnSuccess3",
                    ParentId = null,
                    NameTemp = "GetFunctionsPaging_NoFilter_ReturnSuccess3",
                    SortOrder = 3,
                    Url ="/test3"
                },
                new Function(){
                    Id = "GetFunctionsPaging_NoFilter_ReturnSuccess4",
                    ParentId = null,
                    NameTemp = "GetFunctionsPaging_NoFilter_ReturnSuccess4",
                    SortOrder =4,
                    Url ="/test4"
                }
            });
            await _context.SaveChangesAsync();
            var controller = new FunctionsController(_context, _mockLogger.Object, _mapper, localizer);
            var result = await controller.GetFunctionsPaging(null, 1, 2);
            var okResult = result as OkObjectResult;
            var UserVms = okResult.Value as Pagination<FunctionVm>;
            Assert.Equal(4, UserVms.TotalRecords);
            Assert.Equal(2, UserVms.Items.Count);
        }
        [Fact]
        public async Task GetUsersPaging_HasFilter_ReturnSuccess()
        {
            IStringLocalizer<FunctionsController> localizer = MockLocalizer();
            _context.Functions.AddRange(new List<Function>()
            {
                new Function(){
                    Id = "GetUsersPaging_HasFilter_ReturnSuccess",
                    ParentId = null,
                    NameTemp = "GetUsersPaging_HasFilter_ReturnSuccess",
                    SortOrder = 3,
                    Url ="/GetUsersPaging_HasFilter_ReturnSuccess"
                }
            });
            await _context.SaveChangesAsync();

            var controller = new FunctionsController(_context, _mockLogger.Object, _mapper, localizer);
            var result = await controller.GetFunctionsPaging("GetUsersPaging_HasFilter_ReturnSuccess", 1, 2);
            var okResult = result as OkObjectResult;
            var UserVms = okResult.Value as Pagination<FunctionVm>;
            Assert.Equal(1, UserVms.TotalRecords);
            Assert.Single(UserVms.Items);
        }
        [Fact]
        public async Task GetById_HasData_ReturnSuccess()
        {
            IStringLocalizer<FunctionsController> localizer = MockLocalizer();
            _context.Functions.AddRange(new List<Function>()
            {
                new Function(){
                    Id = "GetById_HasData_ReturnSuccess",
                    ParentId = null,
                    NameTemp = "GetById_HasData_ReturnSuccess",
                    SortOrder =1,
                    Url ="/GetById_HasData_ReturnSuccess"
                }
            });
            await _context.SaveChangesAsync();
            var controller = new FunctionsController(_context, _mockLogger.Object, _mapper, localizer);
            var result = await controller.GetById("GetById_HasData_ReturnSuccess");
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var userVm = okResult.Value as FunctionVm;
            Assert.Equal("GetById_HasData_ReturnSuccess", userVm.Id);
        }
        [Fact]
        public async Task PutFunction_ValidInput_Success()
        {
            IStringLocalizer<FunctionsController> localizer = MockLocalizer();
            _context.Functions.AddRange(new List<Function>()
            {
                new Function(){
                    Id = "PutUser_ValidInput_Success",
                    ParentId = null,
                    NameTemp = "PutUser_ValidInput_Success",
                    SortOrder =1,
                    Url ="/PutUser_ValidInput_Success"
                }
            });
            await _context.SaveChangesAsync();
            var controller = new FunctionsController(_context, _mockLogger.Object, _mapper, localizer);
            var result = await controller.PutFunction("PutUser_ValidInput_Success", new FunctionCreateRequest()
            {
                ParentId = null,
                NameTemp = "PutUser_ValidInput_Success updated",
                SortOrder = 6,
                Url = "/PutUser_ValidInput_Success"
            });
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task PutFunction_ValidInput_Failed()
        {
            IStringLocalizer<FunctionsController> localizer = MockLocalizer();
            var controller = new FunctionsController(_context, _mockLogger.Object, _mapper, localizer);

            var result = await controller.PutFunction("PutUser_ValidInput_Failed", new FunctionCreateRequest()
            {
                ParentId = null,
                NameTemp = "PutUser_ValidInput_Failed update",
                SortOrder = 6,
                Url = "/PutUser_ValidInput_Failed"
            });
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async Task DeleteUser_ValidInput_Success()
        {
            IStringLocalizer<FunctionsController> localizer = MockLocalizer();
            _context.Functions.AddRange(new List<Function>()
            {
                new Function(){
                    Id = "DeleteUser_ValidInput_Success",
                    ParentId = null,
                    NameTemp = "DeleteUser_ValidInput_Success",
                    SortOrder =1,
                    Url ="/DeleteUser_ValidInput_Success"
                }
            });
            await _context.SaveChangesAsync();
            var controller = new FunctionsController(_context, _mockLogger.Object, _mapper, localizer);
            var result = await controller.DeleteFunction("DeleteUser_ValidInput_Success");
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DeleteUser_ValidInput_Failed()
        {
            IStringLocalizer<FunctionsController> localizer = MockLocalizer();
            var controller = new FunctionsController(_context, _mockLogger.Object, _mapper, localizer);
            var result = await controller.DeleteFunction("DeleteUser_ValidInput_Failed");
            Assert.IsType<NotFoundObjectResult>(result);
        }

        #region Service
        private static IStringLocalizer<FunctionsController> MockLocalizer()
        {
            var mock = new Mock<IStringLocalizer<FunctionsController>>();
            var localizedString = new LocalizedString("", "");
            mock.Setup(_ => _[""]).Returns(localizedString);
            var localizer = mock.Object;
            return localizer;
        }

        #endregion

    }
}
