using HMS.Business.ViewModels;
using HMS.Core.Entities;

namespace HMS.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task LoginAsync(UserLoginVM userLoginVm);
        Task LogOutAsync();
        Task<AppUser> GetUserByIdAsync(string id);
        Task CreateRollAsync();
        Task CreateAsync(UserRegisterVM userRegisterVm);
    }
}