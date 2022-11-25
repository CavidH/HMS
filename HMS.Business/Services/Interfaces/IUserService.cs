using HMS.Business.ViewModels;
using HMS.Core.Entities;

namespace HMS.Business.Services.Interfaces
{
    public interface IUserService
    {
        //Task<List<ApplicationUser>> GetAllAsync();
        //Task<Paginate<ApplicationUser>> GetAllPaginatedAsync(int page);
        Task LoginAsync(UserLoginVM userLoginVm);
        Task LogOutAsync();
        Task<AppUser> GetUserByIdAsync(string id);

        Task CreateRollAsync();

        //Task<ApplicationUser> GetAsync(int id);
        Task CreateAsync(UserRegisterVM userRegisterVm);
        //Task Update(int id, UserPostVM userPostVM);
        //Task Remove(int id);
        //Task<int> getPageCount(int take);
    }
}