using AutoMapper;
using cerensoytuna.COMMON.AccountDTO;
using cerensoytuna.COMMON.DTOS.RoleDTO;
using cerensoytuna.editor.Models.RoleModel;
using cerensoytuna.editor.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cerensoytuna.editor.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, UserViewModel>();
            CreateMap<UserListItemDto, UserListViewModel>();
            CreateMap<UserCreateViewModel, UserDto>();
            CreateMap<UserDto, UserEditViewModel>();
            CreateMap<UserEditViewModel, UserDto>();

            CreateMap<RoleDto, RoleViewModel>();
            CreateMap<RoleListItemDto, RoleListViewModel>();
            CreateMap<RoleCreateViewModel, RoleDto>();
            CreateMap<RoleDto, RoleEditViewModel>();
            CreateMap<RoleEditViewModel, RoleDto>();

        }
    }
}
