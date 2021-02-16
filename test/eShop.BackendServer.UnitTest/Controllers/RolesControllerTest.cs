﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eShop.BackendServer.AutoMapper;
using eShop.BackendServer.Controllers;
using eShop.BackendServer.Data;
using eShop.BackendServer.Data.Entities;
using eShop.ViewModels;
using eShop.ViewModels.Systems;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace eShop.BackendServer.UnitTest.Controllers
{
    public class RolesControllerTest
    {
        private readonly Mock<RoleManager<UserRole>> _mockRoleManager;
        private readonly IMapper _mapper;
        private ApplicationDbContext _context;
        private List<UserRole> _roleSources = new List<UserRole>(){
            new UserRole("test1","test1"),
            new UserRole("test2","test2"),
            new UserRole("test3","test3"),
            new UserRole("test4","test4"),
        };
        public RolesControllerTest()
        {
            var roleStore = new Mock<IRoleStore<UserRole>>();
            _mockRoleManager = new Mock<RoleManager<UserRole>>(roleStore.Object, null, null, null, null);

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
        public void ShouldCreateInstance_NotNull_Success()
        {
            var rolesController = new RolesController(_mockRoleManager.Object, _mapper, _context);
            Assert.NotNull(rolesController);
        }

        [Fact]
        public async Task PostRole_ValidInput_Success()
        {
            _mockRoleManager.Setup(x => x.CreateAsync(It.IsAny<UserRole>()))
                .ReturnsAsync(IdentityResult.Success);
            var rolesController = new RolesController(_mockRoleManager.Object, _mapper, _context);
            var result = await rolesController.PostRole(new RoleCreateRequest()
            {
                Id = "test",
                Name = "test"
            });
          
            Assert.NotNull(result);
            Assert.IsType<CreatedAtActionResult>(result);
        }
        [Fact]
        public async Task PostRole_ValidInput_Failed()
        {
            _mockRoleManager.Setup(x => x.CreateAsync(It.IsAny<UserRole>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError[] { }));
            var rolesController = new RolesController(_mockRoleManager.Object,_mapper, _context);
            var result = await rolesController.PostRole(new RoleCreateRequest()
            {
                Id = "test",
                Name = "test"
            });
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task GetRoles_HasData_ReturnSuccess()
        {
            _mockRoleManager.Setup(x => x.Roles)
                .Returns(_roleSources.AsQueryable().BuildMock().Object);
            var rolesController = new RolesController(_mockRoleManager.Object,_mapper, _context);
            var result = await rolesController.GetRoles();
            var okResult = result as OkObjectResult;
            var roleVms = okResult.Value as IEnumerable<RoleVm>;
            Assert.True(roleVms.Count() > 0);
        }
        [Fact]
        public async Task GetRoles_ThrowException_Failed()
        {
            _mockRoleManager.Setup(x => x.Roles).Throws<Exception>();

            var rolesController = new RolesController(_mockRoleManager.Object,_mapper,  _context);

            await Assert.ThrowsAnyAsync<Exception>(async () => await rolesController.GetRoles());
        }

        [Fact]
        public async Task GetRolesPaging_NoFilter_ReturnSuccess()
        {
            _mockRoleManager.Setup(x => x.Roles)
                .Returns(_roleSources.AsQueryable().BuildMock().Object);

            var rolesController = new RolesController(_mockRoleManager.Object,_mapper,  _context);
            var result = await rolesController.GetRolesPaging(null, 1, 2);
            var okResult = result as OkObjectResult;
            var roleVms = okResult.Value as Pagination<RoleVm>;
            Assert.Equal(4, roleVms.TotalRecords);
            Assert.Equal(2, roleVms.Items.Count);
        }

        [Fact]
        public async Task GetRolesPaging_HasFilter_ReturnSuccess()
        {
            _mockRoleManager.Setup(x => x.Roles)
                .Returns(_roleSources.AsQueryable().BuildMock().Object);

            var rolesController = new RolesController(_mockRoleManager.Object,_mapper,  _context);
            var result = await rolesController.GetRolesPaging("test3", 1, 2);
            var okResult = result as OkObjectResult;
            var roleVms = okResult.Value as Pagination<RoleVm>;
            Assert.Equal(1, roleVms.TotalRecords);
            Assert.Single(roleVms.Items);
        }

        [Fact]
        public async Task GetRolesPaging_ThrowException_Failed()
        {
            _mockRoleManager.Setup(x => x.Roles).Throws<Exception>();

            var rolesController = new RolesController(_mockRoleManager.Object,_mapper,  _context);

            await Assert.ThrowsAnyAsync<Exception>(async () => await rolesController.GetRolesPaging(null, 1, 1));
        }

        [Fact]
        public async Task GetById_HasData_ReturnSuccess()
        {
            _mockRoleManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new UserRole
                {
                    Id = "test1",
                    Name = "test1"
                });
            var rolesController = new RolesController(_mockRoleManager.Object,_mapper,  _context);
            var result = await rolesController.GetById("test1");
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var roleVm = okResult.Value as RoleVm;

            Assert.Equal("test1", roleVm.Name);
        }

        [Fact]
        public async Task GetById_ThrowException_Failed()
        {
            _mockRoleManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).Throws<Exception>();

            var rolesController = new RolesController(_mockRoleManager.Object,_mapper,  _context);

            await Assert.ThrowsAnyAsync<Exception>(async () => await rolesController.GetById("test1"));
        }

        [Fact]
        public async Task PutRole_ValidInput_Success()
        {
            _mockRoleManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
               .ReturnsAsync(new UserRole()
               {
                   Id = "test",
                   Name = "test"
               });

            _mockRoleManager.Setup(x => x.UpdateAsync(It.IsAny<UserRole>()))
                .ReturnsAsync(IdentityResult.Success);
            var rolesController = new RolesController(_mockRoleManager.Object,_mapper,  _context);
            var result = await rolesController.PutRole("test", new RoleCreateRequest()
            {
                Id = "test",
                Name = "test"
            });

            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutRole_ValidInput_Failed()
        {
            _mockRoleManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
             .ReturnsAsync(new UserRole()
             {
                 Id = "test",
                 Name = "test"
             });

            _mockRoleManager.Setup(x => x.UpdateAsync(It.IsAny<UserRole>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError[] { }));

            var rolesController = new RolesController(_mockRoleManager.Object,_mapper,  _context);
            var result = await rolesController.PutRole("test", new RoleCreateRequest()
            {
                Id = "test",
                Name = "test"
            });

            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteRole_ValidInput_Success()
        {
            _mockRoleManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
               .ReturnsAsync(new UserRole()
               {
                   Id = "test",
                   Name = "test"
               });

            _mockRoleManager.Setup(x => x.DeleteAsync(It.IsAny<UserRole>()))
                .ReturnsAsync(IdentityResult.Success);
            var rolesController = new RolesController(_mockRoleManager.Object,_mapper,  _context);
            var result = await rolesController.DeleteRole("test");
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteRole_ValidInput_Failed()
        {
            _mockRoleManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
             .ReturnsAsync(new UserRole()
             {
                 Id = "test",
                 Name = "test"
             });

            _mockRoleManager.Setup(x => x.DeleteAsync(It.IsAny<UserRole>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError[] { }));

            var rolesController = new RolesController(_mockRoleManager.Object,_mapper,  _context);
            var result = await rolesController.DeleteRole("test");
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
