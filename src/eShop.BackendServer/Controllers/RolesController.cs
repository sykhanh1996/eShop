﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using eShop.BackendServer.Data.Entities;
using eShop.ViewModels.Systems;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using eShop.BackendServer.Data;
using eShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace eShop.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public RolesController(RoleManager<UserRole> roleManager, IMapper mapper, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostRole(RoleCreateRequest request)
        {
            var role = _mapper.Map<UserRole>(request);
     
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
                return CreatedAtAction(nameof(GetById), new { id = role.Id }, request);
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = _roleManager.Roles;

            var rolevms = await roles.Select(r => new RoleVm()
            {
                Id = r.Id,
                Name = r.Name
            }).ToListAsync();
            return Ok(rolevms);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetRolesPaging(string filter, int pageIndex, int pageSize)
        {
            var query = _roleManager.Roles;
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Id.Contains(filter) || x.Name.Contains(filter));
            }
            var totalRecords = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new RoleVm()
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToListAsync();

            var pagination = new Pagination<RoleVm>
            {
                Items = items,
                TotalRecords = totalRecords,
            };
            return Ok(pagination);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            var roleVm = _mapper.Map<RoleVm>(role);
            return Ok(roleVm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(string id, [FromBody] RoleCreateRequest roleVm)
        {
            if (id != roleVm.Id)
                return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            role.Name = roleVm.Name;
            role.NormalizedName = roleVm.Name.ToUpper();

            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                var rolevm = _mapper.Map<RoleVm>(role);
                return Ok(rolevm);
            }
            return BadRequest();
        }

        [HttpGet("{roleId}/permissions")]
        public async Task<IActionResult> GetPermissionByRoleId(string roleId)
        {
            var permissions = from p in _context.Permissions

                              join a in _context.Commands
                              on p.CommandId equals a.Id
                              where p.RoleId == roleId
                              select new PermissionVm()
                              {
                                  FunctionId = p.FunctionId,
                                  CommandId = p.CommandId,
                                  RoleId = p.RoleId
                              };

            return Ok(await permissions.ToListAsync());
        }
    }
}