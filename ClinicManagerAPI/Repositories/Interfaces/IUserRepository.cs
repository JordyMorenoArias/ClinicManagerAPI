using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Models.Entities;
using EcommerceAPI.Models.DTOs.User;

namespace ClinicManagerAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> AddUser(UserEntity user);
        Task<UserEntity?> GetUserByEmail(string email);
        Task<UserEntity?> GetUserById(int id);
        Task<UserEntity?> GetUserByUsername(string username);
        Task<PagedResult<UserEntity>> GetUsers(QueryUserParameters parameters);
        Task<UserEntity> UpdateUser(UserEntity user);
    }
}