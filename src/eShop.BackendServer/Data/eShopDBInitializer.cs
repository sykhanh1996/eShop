using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.BackendServer.Data.Entities;
using eShop.BackendServer.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace eShop.BackendServer.Data
{
    public class eShopDBInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly string AdminRoleName = "Admin";
        private readonly string UserRoleName = "Member";

        public eShopDBInitializer(ApplicationDbContext context,
            UserManager<User> userManager,
            RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            #region Quyền

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole
                {
                    Id = AdminRoleName,
                    Name = AdminRoleName,
                    NormalizedName = AdminRoleName.ToUpper(),
                });
                await _roleManager.CreateAsync(new AppRole
                {
                    Id = UserRoleName,
                    Name = UserRoleName,
                    NormalizedName = UserRoleName.ToUpper(),
                });
            }

            #endregion Quyền

            #region Người dùng

            if (!_userManager.Users.Any())
            {
                var result = await _userManager.CreateAsync(new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    FirstName = "Quản trị",
                    LastName = "Web",
                    FullName = "Nguyễn Sỹ Khánh",
                    Email = "sykhanh1996l@gmail.com",
                    LockoutEnabled = false
                }, "Admin@123");
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync("admin");
                    await _userManager.AddToRoleAsync(user, AdminRoleName);
                }
            }

            #endregion Người dùng

            #region Ngôn ngữ

            if (!_context.Functions.Any())
            {
                _context.Languages.AddRange(new List<Language>
                {
                    new Language
                        {Id = "EN", Name = "English", IsDefault = false, SortOrder = 1, Status = Status.Active},
                    new Language {Id = "VN", Name = "Việt Nam", IsDefault = true, SortOrder = 2, Status = Status.Active}
                });
                await _context.SaveChangesAsync();
            }


            #endregion


            #region Chức năng

            if (!_context.Functions.Any())
            {
                _context.Functions.AddRange(new List<Function>
                {
                    new Function {Id = "DASHBOARD",  ParentId = null,NameTemp = "Thống kê",SortOrder = 1,Url = "/dashboard" },

                    new Function {Id = "PRODUCT",ParentId = null,NameTemp = "Sản phẩm",Url = "/products" },

                    new Function {Id = "CATEGORY",ParentId = null,NameTemp = "Thể loại",Url = "/categories" },

                    new Function {Id = "BILL",ParentId = null,NameTemp = "Hóa đơn",Url = "/bills"  },

                    new Function {Id = "SYSTEM", ParentId = null,NameTemp = "Hệ thống", Url = "/systems"},

                    new Function {Id = "SYSTEM_USER", ParentId = "SYSTEM",NameTemp = "Người dùng", Url = "/system/users"},
                    new Function {Id = "SYSTEM_ROLE", ParentId = "SYSTEM",NameTemp = "Nhóm Quyền", Url = "/system/roles"},
                    new Function {Id = "SYSTEM_FUNCTION",ParentId = "SYSTEM",NameTemp = "Chức năng", Url = "/system/functions"},
                    new Function {Id = "SYSTEM_PERMISSION",ParentId = "SYSTEM",NameTemp = "Quyền hạn", Url = "/system/permissions"}
                });
                await _context.SaveChangesAsync();
            }

            if (!_context.Commands.Any())
            {
                _context.Commands.AddRange(new List<Command>
                {
                    new Command(){Id = "VIEW", Name = "Xem"},
                    new Command(){Id = "CREATE", Name = "Thêm"},
                    new Command(){Id = "UPDATE", Name = "Sửa"},
                    new Command(){Id = "DELETE", Name = "Xoá"},
                    new Command(){Id = "APPROVE", Name = "Duyệt"},
                });
            }

            #endregion Chức năng

            #region Tên chức năng

            if (!_context.FunctionTranslations.Any())
            {
                _context.FunctionTranslations.AddRange(new List<FunctionTranslation>
                {
                    new FunctionTranslation{FunctionId = "DASHBOARD", LanguageId = "EN", Name = "Dashboard", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                    new FunctionTranslation{FunctionId = "DASHBOARD", LanguageId = "VN", Name = "Bảng tin", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                    new FunctionTranslation{FunctionId = "PRODUCT", LanguageId = "EN", Name = "Products", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                    new FunctionTranslation{FunctionId = "PRODUCT", LanguageId = "VN", Name = "Sản phẩm", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                    new FunctionTranslation{FunctionId = "CATEGORY", LanguageId = "EN", Name = "Categories", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                    new FunctionTranslation{FunctionId = "CATEGORY", LanguageId = "VN", Name = "Loại hàng", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                    new FunctionTranslation{FunctionId = "BILL", LanguageId = "EN", Name = "Orders", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                    new FunctionTranslation{FunctionId = "BILL", LanguageId = "VN", Name = "Đơn hàng", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                    new FunctionTranslation{FunctionId = "SYSTEM", LanguageId = "EN", Name = "Systems", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                    new FunctionTranslation{FunctionId = "SYSTEM", LanguageId = "VN", Name = "Hệ thống", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                    new FunctionTranslation{FunctionId = "SYSTEM_USER", LanguageId = "EN", Name = "Users", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                    new FunctionTranslation{FunctionId = "SYSTEM_USER", LanguageId = "VN", Name = "Người dùng", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                    new FunctionTranslation{FunctionId = "SYSTEM_ROLE", LanguageId = "EN", Name = "Roles", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                    new FunctionTranslation{FunctionId = "SYSTEM_ROLE", LanguageId = "VN", Name = "Nhóm quyền", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                    new FunctionTranslation{FunctionId = "SYSTEM_FUNCTION", LanguageId = "EN", Name = "Functions", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                    new FunctionTranslation{FunctionId = "SYSTEM_FUNCTION", LanguageId = "VN", Name = "Chức năng", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                    new FunctionTranslation{FunctionId = "SYSTEM_PERMISSION", LanguageId = "EN", Name = "Permissions", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now},
                    new FunctionTranslation{FunctionId = "SYSTEM_PERMISSION", LanguageId = "VN", Name = "Quyền hạn", CreateDate = DateTime.Now, LastModifiedDate = DateTime.Now}
                });
                await _context.SaveChangesAsync();
            }

            #endregion
            var functions = _context.Functions;

            if (!_context.CommandInFunctions.Any())
            {
                foreach (var function in functions)
                {
                    var createAction = new CommandInFunction()
                    {
                        CommandId = "CREATE",
                        FunctionId = function.Id
                    };
                    _context.CommandInFunctions.Add(createAction);

                    var updateAction = new CommandInFunction()
                    {
                        CommandId = "UPDATE",
                        FunctionId = function.Id
                    };
                    _context.CommandInFunctions.Add(updateAction);
                    var deleteAction = new CommandInFunction()
                    {
                        CommandId = "DELETE",
                        FunctionId = function.Id
                    };
                    _context.CommandInFunctions.Add(deleteAction);

                    var viewAction = new CommandInFunction()
                    {
                        CommandId = "VIEW",
                        FunctionId = function.Id
                    };
                    _context.CommandInFunctions.Add(viewAction);
                }
            }

            if (!_context.Permissions.Any())
            {
                var adminRole = await _roleManager.FindByNameAsync(AdminRoleName);
                foreach (var function in functions)
                {
                    _context.Permissions.Add(new Permission(function.Id, adminRole.Id, "CREATE"));
                    _context.Permissions.Add(new Permission(function.Id, adminRole.Id, "UPDATE"));
                    _context.Permissions.Add(new Permission(function.Id, adminRole.Id, "DELETE"));
                    _context.Permissions.Add(new Permission(function.Id, adminRole.Id, "VIEW"));
                    _context.Permissions.Add(new Permission(function.Id, adminRole.Id, "APPROVE"));
                }
            }
            await _context.SaveChangesAsync();
        }

    }
}
