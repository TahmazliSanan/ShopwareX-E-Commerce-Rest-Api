using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopwareX.Dtos.Role;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Services.Concretes
{
    public class RoleService(IMapper mapper, IRoleRepository roleRepository) : IRoleService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task<RoleResponseDto> AddRoleAsync(RoleCreateDto dto)
        {
            var role = _mapper.Map<Role>(dto);
            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveAsync();
            return _mapper.Map<RoleResponseDto>(role);
        }

        public async Task<RoleResponseDto> DeleteRoleByIdAsync(long id)
        {
            var existRole = await _roleRepository.GetRoleByIdAsync(id);

            if (existRole is not null)
            {
                existRole.IsDeleted = true;
                existRole.UpdatedAt = DateTime.Now;

                existRole.Users
                    .ToList()
                    .ForEach(u =>
                    {
                        u.IsDeleted = true;
                        u.UpdatedAt = DateTime.Now;
                    });

                await _roleRepository.SaveAsync();
            }

            return _mapper.Map<RoleResponseDto>(existRole);
        }

        public async Task<IEnumerable<RoleResponseDto>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllRoles().ToListAsync();
            return _mapper.Map<IEnumerable<RoleResponseDto>>(roles);
        }

        public async Task<RoleResponseDto> GetRoleByIdAsync(long id)
        {
            var existRole = await _roleRepository.GetRoleByIdAsync(id);
            return _mapper.Map<RoleResponseDto>(existRole);
        }

        public async Task<RoleResponseDto> GetRoleByNameAsync(string name, long? id = null)
        {
            Role? existRoleByName;

            if (id is not null)
                existRoleByName = await _roleRepository.GetRoleByNameAsync(name, id);
            else
                existRoleByName = await _roleRepository.GetRoleByNameAsync(name);
            
            return _mapper.Map<RoleResponseDto>(existRoleByName);
        }

        public async Task<RoleResponseDto> UpdateRoleAsync(long id, RoleUpdateDto dto)
        {
            var existRole = await _roleRepository.GetByIdAsync(id);

            if (existRole is not null)
            {
                existRole.Name = dto.Name;

                await _roleRepository.UpdateAsync(existRole);
                await _roleRepository.SaveAsync();
            }

            return _mapper.Map<RoleResponseDto>(existRole);
        }
    }
}
