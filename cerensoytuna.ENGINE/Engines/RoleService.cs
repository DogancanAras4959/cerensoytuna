using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cerensoytuna.COMMON.DTOS.RoleDTO;
using cerensoytuna.COMMON.DTOS.RoleDTO.RoleUserDto;
using cerensoytuna.CORE.UnitOfWork;
using cerensoytuna.DAL;
using cerensoytuna.DAL.Models;
using cerensoytuna.ENGINES.Interface;

namespace cerensoytuna.ENGINES.Engines
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork<cerensoytunadbcontext> _unitOfWork;
        public RoleService(IUnitOfWork<cerensoytunadbcontext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool deleteRole(int id)
        {
            Task<int> result = _unitOfWork.GetRepository<Roles>().DeleteAsync(new Roles { Id = id });
            return Convert.ToBoolean(result.Result);
        }

        public RoleDto getRoleById(int id)
        {
            Roles role = _unitOfWork.GetRepository<Roles>().FindAsync(x => x.Id == id).Result;
            if (role == null)
            {
                return new RoleDto();
            }

            return new RoleDto
            {
                roleName = role.roleName,
                IsActive = role.IsActive,
                CreatedTime = role.CreatedTime,
                UpdatedTime = role.UpdatedTime,
                Id = role.Id
            };
        }

        public List<RoleUserListItemDto> ListRoleUserByUser(int accountId)
        {
            IEnumerable<RoleUsers> Roles = _unitOfWork.GetRepository<RoleUsers>().Where(x => x.userId == accountId, x => x.OrderBy(y => y.Id), "", 1, 30);

            return Roles.Select(x => new RoleUserListItemDto
            {
                Id = x.Id,
                userId = x.userId,
                roleId = x.roleId,

            }).ToList();
        }

        public async Task<bool> insertRole(RoleDto model)
        {
            Roles newRole = await _unitOfWork.GetRepository<Roles>().AddAsync(new Roles
            {
                roleName = model.roleName,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                IsActive = true,
            });

            return newRole != null && newRole.Id != 0;
        }

        public List<RoleListItemDto> roleList()
        {
            IEnumerable<Roles> Roles = _unitOfWork.GetRepository<Roles>().Where(null, x => x.OrderBy(y => y.Id), "", 1, 30);

            return Roles.Select(x => new RoleListItemDto
            {
                Id = x.Id,
                roleName = x.roleName,
                IsActive = x.IsActive,
                CreatedTime = x.CreatedTime,
                UpdatedTime = x.UpdatedTime

            }).ToList();
        }

        public async Task<bool> updateRole(RoleDto model)
        {
            Roles roleGet = await _unitOfWork.GetRepository<Roles>().FindAsync(x => x.Id == model.Id);

            Roles getRole = await _unitOfWork.GetRepository<Roles>().UpdateAsync(new Roles
            {
                Id = model.Id,
                roleName = model.roleName,
                UpdatedTime = DateTime.Now,
                CreatedTime = roleGet.CreatedTime,
                IsActive = roleGet.IsActive
            });

            return getRole != null;
        }

        public async Task<bool> addRoleToUser(int userId, int roleId)
        {
            RoleUsers role = await _unitOfWork.GetRepository<RoleUsers>().FindAsync(x => x.roleId == roleId && x.userId == userId);

            if (role == null)
            {
                RoleUsers newRole = await _unitOfWork.GetRepository<RoleUsers>().AddAsync(new RoleUsers
                {
                    roleId = roleId,
                    userId = userId,
                });

                return newRole != null && newRole.Id != 0;
            }
            return false;
        }

        public async Task<bool> removeRoleToUser(int userId, int roleId)
        {
            RoleUsers role = await _unitOfWork.GetRepository<RoleUsers>().FindAsync(x => x.roleId == roleId && x.userId == userId);

            if (role != null)
            {
                Task<int> result = _unitOfWork.GetRepository<RoleUsers>().DeleteAsync(new RoleUsers { Id = role.Id });
                return Convert.ToBoolean(result.Result);
            }
            else
            {
                return false;
            }

        }
    }
}
